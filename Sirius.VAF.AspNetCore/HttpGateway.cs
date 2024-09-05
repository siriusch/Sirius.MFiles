using System;
using System.IO;

using bsn.Har;
using bsn.Har.AspNetCore.Server;

using MFiles.VAF.Common;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace Sirius.VAF.AspNetCore {
	public class HttpGateway: IDisposable {
		public static HttpGateway Create<TConfiguration>(Action<IServiceCollection> configureServices, Action<IApplicationBuilder> configure) where TConfiguration: class {
			var host = new WebHostBuilder()
					.UseHarServer(o => { })
					.ConfigureServices(s => {
						s.AddHttpGateway<TConfiguration>();
						configureServices?.Invoke(s);
					})
					.Configure(configure)
					.Build();
			host.Start();
			return new HttpGateway(host);
		}

		public HttpGateway(IWebHost host) {
			if ((host ?? throw new ArgumentNullException(nameof(host))).Services.GetService<IServer>() is not HarServer) {
				throw new ArgumentException("The IServer service must be a HarServer", nameof(host));
			}
			Host = host;
		}

		private IWebHost Host {
			get;
		}

		private HarServer Server => (HarServer)Host.Services.GetService<IServer>();

		public string HttpGatewayExtensionMethod<TConfiguration>(EventHandlerEnvironment env, TConfiguration configuration) where TConfiguration: class {
			// Deserialize HAR request
			using var reader = new JsonTextReader(new StringReader(env.Input));
			var request = HarDocument.Serializer.Deserialize<HarRequest>(reader);
			// Process request in ASP.NET Core application and wait for result
			var processTask = Server.ProcessAsync(request, f => {
				f.Set<IEventHandlerEnvironmentFeature>(new EventHandlerEnvironmentFeature(env));
				f.Set<IConfigurationFeature<TConfiguration>>(new ConfigurationFeature<TConfiguration>(configuration));
			}).AsTask();
			var response = processTask.GetAwaiter().GetResult();
			// Send HAR response back
			using var writer = new StringWriter();
			HarDocument.Serializer.Serialize(writer, response);
			return writer.ToString();
		}

		public void Dispose() {
			Host.Dispose();
		}
	}
}
