using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class LoginElement: XElement {
		public static readonly XName ElementName = "login";
		private static readonly XName AccountTypeName = "accounttype";
		private static readonly XName LicenseTypeName = "licensetype";
		private static readonly XName DomainName = "domain";
		private static readonly XName UsernameName = "username";
		private static readonly XName FullnameName = "fullname";
		private static readonly XName EmailName = "email";

		public LoginElement(): base(ElementName) { }

		public LoginElement(XElement node): base(node) { }

		public string AccountType {
			get => ElementAttribute<string>.Get(this, AccountTypeName);
			set => ElementAttribute<string>.Set(this, AccountTypeName, value);
		}

		public string LicenseType {
			get => ElementAttribute<string>.Get(this, LicenseTypeName);
			set => ElementAttribute<string>.Set(this, LicenseTypeName, value);
		}

		public XElement Domain {
			get => Element(DomainName);
			set => this.SetElement(DomainName, value);
		}

		public XElement Username {
			get => Element(UsernameName);
			set => this.SetElement(UsernameName, value);
		}

		public XElement Fullname {
			get => Element(FullnameName);
			set => this.SetElement(FullnameName, value);
		}

		public XElement Email {
			get => Element(EmailName);
			set => this.SetElement(EmailName, value);
		}

		public ServerRolesElement ServerRoles {
			get => (ServerRolesElement)Element(ServerRolesElement.ElementName);
			set => this.SetElement(ServerRolesElement.ElementName, value);
		}
	}
}
