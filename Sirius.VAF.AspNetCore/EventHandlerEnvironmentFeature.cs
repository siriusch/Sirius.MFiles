using MFiles.VAF.Common;

namespace Sirius.VAF.AspNetCore {
	public class EventHandlerEnvironmentFeature: IEventHandlerEnvironmentFeature {
		public EventHandlerEnvironmentFeature(EventHandlerEnvironment env) {
			Env = env;
		}

		public EventHandlerEnvironment Env {
			get;
		}
	}
}
