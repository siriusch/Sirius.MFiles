using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class ObjTypeFlagsElement: FlagsElementBase<ObjTypeFlags> {
		public static readonly XName ElementName = "flags";

		public ObjTypeFlagsElement(): base(ElementName) { }

		public ObjTypeFlagsElement(XElement node): base(node) { }
	}
}
