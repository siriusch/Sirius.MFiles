using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class VaultRolesElement: FlagsElementBase<MFUserAccountVaultRole> {
		public static readonly XName ElementName = "vaultroles";

		public VaultRolesElement(XName name): base(name) { }

		public VaultRolesElement(XElement node): base(node) { }

		protected override string Prefix => nameof(MFUserAccountVaultRole);
	}
}
