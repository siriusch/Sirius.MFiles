using JetBrains.Annotations;

using MFiles.VAF.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sirius.VAF.AspNetCore {
	public static class HttpGatewayExtensions {
		public static EventHandlerEnvironment GetEnv(this HttpContext that) {
			return that.Features.Get<IEventHandlerEnvironmentFeature>()?.Env;
		}

		public static TConfiguration GetConfiguration<TConfiguration>(this HttpContext that) where TConfiguration: class {
			return that.Features.Get<IConfigurationFeature<TConfiguration>>()?.Configuration;
		}

		public static IServiceCollection AddHttpGateway<TConfiguration>([NotNull] this IServiceCollection that) where TConfiguration: class {
			that.AddHttpContextAccessor();
			that.TryAddTransient(sp => sp.GetService<IHttpContextAccessor>().HttpContext.GetEnv());
			that.TryAddTransient(sp => sp.GetService<IHttpContextAccessor>().HttpContext.GetConfiguration<TConfiguration>());
			that.TryAddSingleton<IActionResultExecutor<CalendarResult>, CalendarResultExecutor>();
			that.TryAddSingleton<IActionResultExecutor<ObjVerExFileResult>, ObjVerExFileResultExecutor>();
			return that;
		}
	}
}
