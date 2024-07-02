using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class PostCondsElement: CondsElementBase {
		public static readonly XName ElementName = "postconds";

		public PostCondsElement(): base(ElementName) { }

		public PostCondsElement(XElement node): base(node) { }
	}
}
