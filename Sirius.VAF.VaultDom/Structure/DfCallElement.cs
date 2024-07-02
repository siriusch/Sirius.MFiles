using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class DfCallElement: XElement {
		public static readonly XName ElementName = "dfcall";

		public DfCallElement(): base(ElementName) { }

		public DfCallElement(XElement node): base(node) { }

		public string Type {
			get => ElementAttribute<string>.Get(this, "type");
			set => ElementAttribute<string>.Set(this, "type", value);
		}
	}
}
