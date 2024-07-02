using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class RegEx: XElement {
		public static readonly XName ElementName = "regex";

		public RegEx(): base(ElementName) { }

		public RegEx(XElement node): base(node) { }

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
