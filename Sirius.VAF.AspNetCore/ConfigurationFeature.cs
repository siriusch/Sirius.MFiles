namespace Sirius.VAF.AspNetCore {
	public class ConfigurationFeature<TConfiguration>: IConfigurationFeature<TConfiguration> where TConfiguration: class {
		public ConfigurationFeature(TConfiguration configuration) {
			Configuration = configuration;
		}

		public TConfiguration Configuration {
			get;
		}
	}
}
