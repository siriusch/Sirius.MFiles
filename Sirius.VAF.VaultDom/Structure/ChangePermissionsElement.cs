using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ChangePermissionsElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "changepermissions";
		private static readonly XName DiscardAutomaticAclsName = "discardautomaticacls";

		public ChangePermissionsElement(): base(ElementName) { }

		public ChangePermissionsElement(XElement node): base(node) { }

		public bool DiscardAutomaticAcls {
			get => ElementAttribute<bool>.Get(this, DiscardAutomaticAclsName);
			set => ElementAttribute<bool>.Set(this, DiscardAutomaticAclsName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public override StateAction Type => StateAction.ChangePermissions;
	}
}
