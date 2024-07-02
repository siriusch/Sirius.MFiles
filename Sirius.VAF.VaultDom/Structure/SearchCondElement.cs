using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class SearchCondElement: XElement {
		public static readonly XName ElementName = "searchcond";
		private static readonly XName PosName = "pos";

		public SearchCondElement(): base(ElementName) { }

		public SearchCondElement(XElement node): base(node) { }

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public ExpressionElement Expression {
			get => (ExpressionElement)Element(ExpressionElement.ElementName);
			set => this.SetElement(ExpressionElement.ElementName, value);
		}

		public ConditionElement Condition {
			get => (ConditionElement)Element(ConditionElement.ElementName);
			set => this.SetElement(ConditionElement.ElementName, value);
		}

		public TypedValueElement TypedValue {
			get => (TypedValueElement)Element(TypedValueElement.ElementName);
			set => this.SetElement(TypedValueElement.ElementName, value);
		}
	}
}
