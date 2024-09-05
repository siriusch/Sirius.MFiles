using System;

using MFiles.VAF.Common;

namespace Sirius.VAF.AspNetCore {
	public abstract class HttpGatewayRequest<TConfiguration> {
		public EventHandlerEnvironment Env {
			get;
		}

		public TConfiguration Configuration {
			get;
		}

		protected HttpGatewayRequest(EventHandlerEnvironment env, TConfiguration configuration) {
			Env = env;
			Configuration = configuration;
		}
	}
}
