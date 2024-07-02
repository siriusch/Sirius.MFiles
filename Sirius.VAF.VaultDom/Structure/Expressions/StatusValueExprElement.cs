using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public class StatusValueExprElement: ExpressionElementBase {
		private static readonly Mapping<MFStatusType> TypeMapping = new() {
				{ "CheckedOut", MFStatusType.MFStatusTypeCheckedOut },
				{ "CheckedOutTo", MFStatusType.MFStatusTypeCheckedOutTo },
				{ "CheckedOutAt", MFStatusType.MFStatusTypeCheckedOutAt },
				{ "Object", MFStatusType.MFStatusTypeObjectID },
				{ "ObjectVersion", MFStatusType.MFStatusTypeObjectVersionID },
				{ "Deleted", MFStatusType.MFStatusTypeDeleted },
				{ "ObjectType", MFStatusType.MFStatusTypeObjectTypeID },
				{ "IsLatestCheckedInVersion", MFStatusType.MFStatusTypeIsLatestCheckedInVersion },
				{ "Ext", MFStatusType.MFStatusTypeExtID },
				{ "LatestOrSpecific", MFStatusType.MFStatusTypeLatestOrSpecific }
		};

		public static readonly XName ElementName = "StatusValueExpr";
		private static readonly XName TypeName = "type";

		public StatusValueExprElement(): base(ElementName) { }

		public StatusValueExprElement(XElement other): base(other) { }

		public MFStatusType Type {
			get => ElementAttribute<MFStatusType>.Get(this, TypeName, TypeMapping.Parse);
			set => ElementAttribute<MFStatusType>.Set(this, TypeName, value, TypeMapping.Stringify);
		}

		public override MFExpressionType ExpressionType => MFExpressionType.MFExpressionTypeStatusValue;
	}
}
