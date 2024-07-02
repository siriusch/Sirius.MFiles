using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure.AutomaticValue {
	public class AutoNumberWithVBScriptElement: CalcVBScriptElementBase {
		public static readonly XName ElementName = "AutoNumberWithVBScript";

		public AutoNumberWithVBScriptElement(): base(ElementName) { }

		public AutoNumberWithVBScriptElement(XElement node): base(node) { }

		public override AutomaticValueType Type => AutomaticValueType.AutoNumberWithVBScript;
	}
}
