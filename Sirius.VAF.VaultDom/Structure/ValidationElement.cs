using System.Xml.Linq;

using Sirius.VAF.VaultDom.Structure.Validations;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ValidationElement: SelectOneElementBase<ValidationType> {
		public static readonly XName ElementName = "validation";
		private static readonly XName StringName = "string";

		public ValidationElement(): base(ElementName) { }

		public ValidationElement(XElement node): base(node) { }

		public XElement String {
			get => Element(StringName);
			set => this.SetElement(StringName, value);
		}

		public RegEx RegEx {
			get => (RegEx)Element(RegExElement.ElementName);
			set => this.SetElement(RegExElement.ElementName, value);
		}

		public VBScriptElement VBScript {
			get => (VBScriptElement)Element(Validations.VBScriptElement.ElementName);
			set => this.SetElement(Validations.VBScriptElement.ElementName, value);
		}
	}
}
