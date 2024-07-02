using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public class PrologueElement: XElement {
		public static readonly XName ElementName = "prologue";
		internal static readonly XName AclCacheName = "aclcache";

		public PrologueElement(): base(ElementName) { }

		public PrologueElement(XElement node): base(node) { }

		public CollectionElement<AclDefinitionElement> AclCache {
			get => (CollectionElement<AclDefinitionElement>)Element(AclCacheName);
			set => this.SetElement(AclCacheName, value);
		}
	}
}
