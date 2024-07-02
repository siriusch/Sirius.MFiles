using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ExternalGroupElement: XElement {
		private static readonly Mapping<MFLicenseType> LicenseTypeMapping = new() {
				{ "None", MFLicenseType.MFLicenseTypeNone },
				{ "NamedUser", MFLicenseType.MFLicenseTypeNamedUserLicense },
				{ "ConcurrentUser", MFLicenseType.MFLicenseTypeConcurrentUserLicense },
				{ "ReadOnly", MFLicenseType.MFLicenseTypeReadOnlyLicense }
		};

		public static readonly XName ElementName = "external";
		private static readonly XName ExternalGroupImportModeName = "externalgroupimportmode";
		private static readonly XName LicenseTypeForNewAccountsName = "licensetypefornewaccounts";
		private static readonly XName MembershipSyncName = "membershipsync";
		private static readonly XName ValueName = "value";
		private static readonly XName ExternalNameElementName = "externalname";
		private static readonly XName OrganizationalUnitElementName = "organizationalunit";

		public ExternalGroupElement(): base(ElementName) { }

		public ExternalGroupElement(XElement node): base(node) { }

		public ExternalGroupImportMode ExternalGroupImportMode {
			get => ElementAttribute<ExternalGroupImportMode>.Get(this, ExternalGroupImportModeName);
			set => ElementAttribute<ExternalGroupImportMode>.Set(this, ExternalGroupImportModeName, value);
		}

		public MFLicenseType LicenseTypeForNewAccounts {
			get => ElementAttribute<MFLicenseType>.Get(this, LicenseTypeForNewAccountsName, LicenseTypeMapping.Parse);
			set => ElementAttribute<MFLicenseType>.Set(this, LicenseTypeForNewAccountsName, value, LicenseTypeMapping.Stringify);
		}

		public bool MembershipSync {
			get => ElementAttribute<bool>.Get(this, MembershipSyncName);
			set => ElementAttribute<bool>.Set(this, MembershipSyncName, value);
		}

		public new bool Value {
			get => ElementAttribute<bool>.Get(this, ValueName);
			set => ElementAttribute<bool>.Set(this, ValueName, value);
		}

		public XElement ExternalName {
			get => Element(ExternalNameElementName);
			set => this.SetElement(ExternalNameElementName, value);
		}

		public XElement OrganizationalUnit {
			get => Element(OrganizationalUnitElementName);
			set => this.SetElement(OrganizationalUnitElementName, value);
		}
	}
}
