using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public class ACLExprElement: ExpressionElementBase {
		private static readonly Mapping<MFPermissionsExpressionType> TypeMapping = new() {
				{ "ACL", MFPermissionsExpressionType.MFACL },
				{ "VisibleTo", MFPermissionsExpressionType.MFVisibleTo },
				{ "EditableBy", MFPermissionsExpressionType.MFEditableBy },
				{ "PermissionsChangeableBy", MFPermissionsExpressionType.MFPermissionsChangeableBy },
				{ "FullControlBy", MFPermissionsExpressionType.MFFullControlBy },
				{ "DeletableBy", MFPermissionsExpressionType.MFDeletableBy },
				{ "ObjectsAttachableToThisItemBy", MFPermissionsExpressionType.MFObjectsAttachableToThisItemBy }
		};

		public static readonly XName ElementName = "ACLExpr";
		private static readonly XName TypeName = "type";

		public ACLExprElement(): base(ElementName) { }

		public ACLExprElement(XElement node): base(node) { }

		public MFPermissionsExpressionType Type {
			get => ElementAttribute<MFPermissionsExpressionType>.Get(this, TypeName, TypeMapping.Parse);
			set => ElementAttribute<MFPermissionsExpressionType>.Set(this, TypeName, value, TypeMapping.Stringify);
		}

		public override MFExpressionType ExpressionType => MFExpressionType.MFExpressionTypePermissions;
	}
}
