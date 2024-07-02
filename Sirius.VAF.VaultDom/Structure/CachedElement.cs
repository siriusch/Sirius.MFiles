using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class CachedElement: XElement {
		public static readonly XName ElementName = "cached";
		private static readonly XName IdName = "id";

		public CachedElement(): base(ElementName) { }

		public CachedElement(XElement node): base(node) { }

		public string Id {
			get => ElementAttribute<string>.Get(this, IdName);
			set => ElementAttribute<string>.Set(this, IdName, value);
		}
	}
}
