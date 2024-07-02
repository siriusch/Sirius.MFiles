using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class OwnerPdElement: XElement {
		public static readonly XName ElementName = "ownerpd";

		public OwnerPdElement(): base(ElementName) { }

		public OwnerPdElement(XElement node): base(node) { }

		public string Id {
			get => ElementAttribute<string>.Get(this, "id");
			set => ElementAttribute<string>.Set(this, "id", value);
		}

		public string Type {
			get => ElementAttribute<string>.Get(this, "type");
			set => ElementAttribute<string>.Set(this, "type", value);
		}
	}
}
