using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure.AutomaticValue {
	public class CalcWithVBScriptElement: CalcVBScriptElementBase {
		public static readonly XName ElementName = "CalcWithVBScript";

		public CalcWithVBScriptElement(): base(ElementName) { }

		public CalcWithVBScriptElement(XElement node): base(node) { }

		public override AutomaticValueType Type => AutomaticValueType.CalcWithVBScript;
	}
}
