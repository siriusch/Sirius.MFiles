using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public class PropertyValueExprElement: ExpressionElementBase {
		public static readonly XName ElementName = "PropertyValueExpr";
		private static readonly XName PdIdName = "pdid";
		private static readonly XName ParentChildBehaviorName = "parentchildbehavior";

		public PropertyValueExprElement(): base(ElementName) { }

		public PropertyValueExprElement(XElement node): base(node) { }

		public int PdId {
			get => ElementAttribute<int>.Get(this, PdIdName);
			set => ElementAttribute<int>.Set(this, PdIdName, value);
		}

		public override MFExpressionType ExpressionType => MFExpressionType.MFExpressionTypePropertyValue;

		public ParentChildBehavior ParentChildBehavior {
			get => ElementAttribute<ParentChildBehavior>.Get(this, ParentChildBehaviorName);
			set => ElementAttribute<ParentChildBehavior>.Set(this, ParentChildBehaviorName, value);
		}
	}
}
