using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class VBScriptElement: XElement {
		public static readonly XName ElementName = "vbscript";

		public VBScriptElement(): base(ElementName) { }

		public VBScriptElement(XElement node): base(node) { }

		public string Code {
			get => ElementAttribute<string>.Get(this, "code");
			set => ElementAttribute<string>.Set(this, "code", value);
		}
	}
}
