using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public abstract class ExpressionElementBase: XElement {
		protected ExpressionElementBase(XName name): base(name) { }

		protected ExpressionElementBase(XElement other): base(other) { }

		public DfCallElement DfCall {
			get => (DfCallElement)Element(DfCallElement.ElementName);
			set => this.SetElement(DfCallElement.ElementName, value);
		}

		public abstract MFExpressionType ExpressionType {
			get;
		}
	}
}
