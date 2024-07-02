using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure.AutomaticValue {
	public class AutoNumberSimpleElement: CalcSimpleElementBase {
		public static readonly XName ElementName = "AutoNumberSimple";

		public AutoNumberSimpleElement(): base(ElementName) { }

		public AutoNumberSimpleElement(XElement node): base(node) { }

		public override AutomaticValueType Type => AutomaticValueType.AutoNumberSimple;
	}
}
