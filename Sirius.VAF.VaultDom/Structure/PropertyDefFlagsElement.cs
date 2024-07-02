using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class PropertyDefFlagsElement: FlagsElementBase<PropertyDefFlags> {
		public static readonly XName ElementName = "flags";

		public PropertyDefFlagsElement(): base(ElementName) { }

		public PropertyDefFlagsElement(XElement node): base(node) { }
	}
}
