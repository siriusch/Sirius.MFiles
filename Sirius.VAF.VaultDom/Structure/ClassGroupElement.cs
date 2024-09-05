using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ClassGroupElement: DefinitionElementBase, IElementWithAcl {
		public static readonly XName ElementName = "classgroup";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName FullName = "full";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";
		private static readonly XName OtIdName = "otid";
		internal static readonly XName MembersName = "members";

		public ClassGroupElement(): base(ElementName) { }

		public ClassGroupElement(XElement node): base(node) { }

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public int OtId {
			get => ElementAttribute<int>.Get(this, OtIdName);
			set => ElementAttribute<int>.Set(this, OtIdName, value);
		}

		public CollectionElement<ClassReferenceElement> Members {
			get => (CollectionElement<ClassReferenceElement>)Element(MembersName);
			set => this.SetElement(MembersName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}
	}
}
