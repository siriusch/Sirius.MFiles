using MFiles.VAF.Common;

namespace Sirius.VAF.AspNetCore {
	public interface IEventHandlerEnvironmentFeature {
		EventHandlerEnvironment Env {
			get;
		}
	}
}
