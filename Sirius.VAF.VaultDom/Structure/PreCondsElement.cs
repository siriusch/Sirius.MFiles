using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class PreCondsElement: CondsElementBase {
		public static readonly XName ElementName = "preconds";

		public PreCondsElement(): base(ElementName) { }

		public PreCondsElement(XElement node): base(node) { }
	}
}
