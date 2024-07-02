using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class ConfigurationRuleElement: XElement {
		public static readonly XName ElementName = "configurationrule";

		public ConfigurationRuleElement(): base(ElementName) { }

		public ConfigurationRuleElement(XElement node): base(node) { }
	}
}
