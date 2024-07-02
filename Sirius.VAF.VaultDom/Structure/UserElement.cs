using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class UserElement: XElement {
		public static readonly XName ElementName = "user";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName FullName = "full";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName LoginNameName = "loginname";
		private static readonly XName NameName = "name";
		private static readonly XName PseudoName = "pseudo";
		private static readonly XName VaultLanguageName = "vaultlanguage";

		public UserElement(): base(ElementName) { }

		public UserElement(XElement node): base(node) { }

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, GuidName);
			set => ElementAttribute<Guid>.Set(this, GuidName, value);
		}

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public string LoginName {
			get => ElementAttribute<string>.Get(this, LoginNameName);
			set => ElementAttribute<string>.Set(this, LoginNameName, value);
		}

		public string UserName {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}

		public bool Pseudo {
			get => ElementAttribute<bool>.Get(this, PseudoName);
			set => ElementAttribute<bool>.Set(this, PseudoName, value);
		}

		public int VaultLanguage {
			get => ElementAttribute<int>.Get(this, VaultLanguageName);
			set => ElementAttribute<int>.Set(this, VaultLanguageName, value);
		}

		public VaultRolesElement VaultRoles {
			get => (VaultRolesElement)Element(VaultRolesElement.ElementName);
			set => this.SetElement(VaultRolesElement.ElementName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public LoginElement Login {
			get => (LoginElement)Element(LoginElement.ElementName);
			set => this.SetElement(LoginElement.ElementName, value);
		}
	}
}
