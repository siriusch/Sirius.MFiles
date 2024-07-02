using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class CondsFlagsElement: FlagsElementBase<CondsFlags> {
		public static readonly XName ElementName = "flags";

		public CondsFlagsElement(): base(ElementName) { }

		public CondsFlagsElement(XElement node): base(node) { }
	}
}
