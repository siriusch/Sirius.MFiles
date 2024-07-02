using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public class AceElement: AclComponentElementBase {
		public static readonly XName ElementName = "ace";
		internal static readonly XName PseudoUserElementsName = "pseudouserelements";
		internal static readonly XName UsersAndGroupsName = "usersandgroups";
		internal static readonly XName RightsName = "rights";
		private static readonly XName IdName = "id";

		public AceElement(): base(ElementName) { }

		public AceElement(XElement node): base(node) { }

		public string Id {
			get => ElementAttribute<string>.Get(this, IdName);
			set => ElementAttribute<string>.Set(this, IdName, value);
		}

		public CollectionElement<PseudoUserElement> PseudoUserElements {
			get => (CollectionElement<PseudoUserElement>)Element(PseudoUserElementsName);
			set => this.SetElement(PseudoUserElementsName, value);
		}

		public CollectionElement<SubjectReferenceElementBase> UsersAndGroups {
			get => (CollectionElement<SubjectReferenceElementBase>)Element(UsersAndGroupsName);
			set => this.SetElement(UsersAndGroupsName, value);
		}

		public CollectionElement<AceRightElement> Rights {
			get => (CollectionElement<AceRightElement>)Element(RightsName);
			set => this.SetElement(RightsName, value);
		}
	}
}
