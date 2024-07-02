using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AclForNaclElement: XElement {
		public static readonly XName ElementName = "aclfornacl";
		private static readonly XName CachedElementName = "cached";

		public AclForNaclElement(): base(ElementName) { }

		public AclForNaclElement(XElement node): base(node) { }

		public CachedElement Cached {
			get => (CachedElement)Element(CachedElementName);
			set => this.SetElement(CachedElementName, value);
		}
	}
}
