using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AclReferenceElement: XElement {
		public static readonly XName ElementName = "acl";

		public AclReferenceElement(): this(ElementName) { }

		public AclReferenceElement(XName name): base(name) { }

		public AclReferenceElement(XElement node): base(node) { }

		public CachedElement Cached {
			get => (CachedElement)Element(CachedElement.ElementName);
			set => this.SetElement(CachedElement.ElementName, value);
		}
	}
}
