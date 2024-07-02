using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public class AnyFieldExprElement: ExpressionElementBase {
		public static readonly XName ElementName = "AnyFieldExpr";

		public AnyFieldExprElement(): base(ElementName) { }

		public AnyFieldExprElement(XElement node): base(node) { }

		public override MFExpressionType ExpressionType => MFExpressionType.MFExpressionTypeAnyField;
	}
}
