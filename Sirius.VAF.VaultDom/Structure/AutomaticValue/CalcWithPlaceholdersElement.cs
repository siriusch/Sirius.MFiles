using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure.AutomaticValue {
	public class CalcWithPlaceholdersElement: CalcSimpleElementBase {
		public static readonly XName ElementName = "CalcWithPlaceholders";

		public CalcWithPlaceholdersElement(): base(ElementName) { }

		public CalcWithPlaceholdersElement(XElement node): base(node) { }

		public override AutomaticValueType Type => AutomaticValueType.CalcWithPlaceholders;
	}
}
