using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class SetPropertiesElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "setproperties";
		private static readonly XName DefaultPropertyElementName = "defaultproperty";

		public SetPropertiesElement(): base(ElementName) { }

		public SetPropertiesElement(XElement node): base(node) { }

		public DefaultPropertyElement DefaultProperty {
			get => (DefaultPropertyElement)Element(DefaultPropertyElementName);
			set => this.SetElement(DefaultPropertyElementName, value);
		}

		public override StateAction Type => StateAction.SetProperties;
	}
}
