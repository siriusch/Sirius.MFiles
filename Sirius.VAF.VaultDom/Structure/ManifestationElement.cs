using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ManifestationElement: XElement {
		public static readonly XName ElementName = "manifestation";
		private static readonly XName PdIdName = "pdid";

		public ManifestationElement(): base(ElementName) { }

		public ManifestationElement(XElement node): base(node) { }

		public int PdId {
			get => ElementAttribute<int>.Get(this, PdIdName);
			set => ElementAttribute<int>.Set(this, PdIdName, value);
		}
	}
}
