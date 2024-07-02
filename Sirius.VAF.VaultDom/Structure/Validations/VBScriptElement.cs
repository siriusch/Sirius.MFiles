using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure.Validations {
	public class VBScriptElement: ActivableElementBase<ValidationType> {
		internal static readonly XName ElementName = "VBScript";

		public VBScriptElement(): base(ElementName) { }

		public VBScriptElement(XElement node): base(node) { }

		public override ValidationType Type => ValidationType.VBScript;
	}
}
