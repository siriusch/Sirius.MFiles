using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class RegExElement: XElement {
		public static readonly XName ElementName = "regex";

		public RegExElement(): base(ElementName) { }

		public RegExElement(XElement node): base(node) { }

		public string Pattern {
			get => ElementAttribute<string>.Get(this, "pattern");
			set => ElementAttribute<string>.Set(this, "pattern", value);
		}

		public string Options {
			get => ElementAttribute<string>.Get(this, "options");
			set => ElementAttribute<string>.Set(this, "options", value);
		}
	}
}
