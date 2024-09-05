using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Prologue;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class GroupElement: DefinitionElementBase, IElementWithAcl {
		public static readonly XName ElementName = "group";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName ExternalNameName = "externalname";
		private static readonly XName FullName = "full";
		internal static readonly XName MembersName = "members";

		public GroupElement(): base(ElementName) { }

		public GroupElement(XElement node): base(node) { }

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public string ExternalName {
			get => ElementAttribute<string>.Get(this, ExternalNameName);
			set => ElementAttribute<string>.Set(this, ExternalNameName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public CollectionElement<SubjectReferenceElementBase> Members {
			get => (CollectionElement<SubjectReferenceElementBase>)Element(MembersName);
			set => this.SetElement(MembersName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public ExternalGroupElement External {
			get => (ExternalGroupElement)Element(ExternalGroupElement.ElementName);
			set => this.SetElement(ExternalGroupElement.ElementName, value);
		}
	}
}
