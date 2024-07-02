using System.Xml.Linq;

using Sirius.VAF.VaultDom.Structure.Expressions;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ExpressionElement: XElement {
		public static readonly XName ElementName = "expression";
		internal static readonly XName IndirectionName = "indirection";

		public ExpressionElement(): base(ElementName) { }

		public ExpressionElement(XElement node): base(node) { }

		public PropertyValueExprElement PropertyValueExpr {
			get => (PropertyValueExprElement)Element(PropertyValueExprElement.ElementName);
			set => this.SetElement(PropertyValueExprElement.ElementName, value);
		}

		public TypedValueExprElement TypedValueExpr {
			get => (TypedValueExprElement)Element(TypedValueExprElement.ElementName);
			set => this.SetElement(TypedValueExprElement.ElementName, value);
		}

		public StatusValueExprElement StatusValueExpr {
			get => (StatusValueExprElement)Element(StatusValueExprElement.ElementName);
			set => this.SetElement(StatusValueExprElement.ElementName, value);
		}

		public CollectionElement<LevelElement> Indirection {
			get => (CollectionElement<LevelElement>)Element(IndirectionName);
			set => this.SetElement(IndirectionName, value);
		}
	}
}
