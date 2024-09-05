using Sirius.VAF.VaultDom.Structure;

namespace Sirius.VAF.VaultDom {
	public interface IElementWithAcl {
		AclReferenceElement Acl {
			get;
			set;
		}
	}
}