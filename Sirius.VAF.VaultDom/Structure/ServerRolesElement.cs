using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class ServerRolesElement: FlagsElementBase<MFLoginServerRole> {
		public static readonly XName ElementName = "vaultroles";

		public ServerRolesElement(XName name): base(name) { }

		public ServerRolesElement(XElement node): base(node) { }

		protected override string Prefix => nameof(MFLoginServerRole);
	}
}
