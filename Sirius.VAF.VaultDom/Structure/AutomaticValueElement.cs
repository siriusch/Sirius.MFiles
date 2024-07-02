using System.Xml.Linq;

using Sirius.VAF.VaultDom.Structure.AutomaticValue;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AutomaticValueElement: SelectOneElementBase<AutomaticValueType> {
		public static readonly XName ElementName = "automaticvalue";
		private static readonly XName CalcOrderName = "calcorder";
		private static readonly XName CalcWithPlaceholdersName = "CalcWithPlaceholders";
		private static readonly XName CalcWithVBScriptName = "CalcWithVBScript";
		private static readonly XName AutoNumberSimpleName = "AutoNumberSimple";
		private static readonly XName AutoNumberWithVBScriptName = "AutoNumberWithVBScript";

		public AutomaticValueElement(): base(ElementName) { }

		public AutomaticValueElement(XElement node): base(node) { }

		public int CalcOrder {
			get => ElementAttribute<int>.Get(this, CalcOrderName);
			set => ElementAttribute<int>.Set(this, CalcOrderName, value);
		}

		public CalcWithPlaceholdersElement CalcWithPlaceholders {
			get => (CalcWithPlaceholdersElement)Element(CalcWithPlaceholdersName);
			set => this.SetElement(CalcWithPlaceholdersName, value);
		}

		public CalcWithVBScriptElement CalcWithVBScript {
			get => (CalcWithVBScriptElement)Element(CalcWithVBScriptName);
			set => this.SetElement(CalcWithVBScriptName, value);
		}

		public AutoNumberSimpleElement AutoNumberSimple {
			get => (AutoNumberSimpleElement)Element(AutoNumberSimpleName);
			set => this.SetElement(AutoNumberSimpleName, value);
		}

		public AutoNumberWithVBScriptElement AutoNumberWithVBScript {
			get => (AutoNumberWithVBScriptElement)Element(AutoNumberWithVBScriptName);
			set => this.SetElement(AutoNumberWithVBScriptName, value);
		}
	}
}
