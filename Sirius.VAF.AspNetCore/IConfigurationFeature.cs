namespace Sirius.VAF.AspNetCore {
	public interface IConfigurationFeature<TConfiguration> where TConfiguration: class {
		TConfiguration Configuration {
			get;
		}
	}
}
