using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class InOutElement: XElement {
		public static readonly XName ElementName = "inout";
		private static readonly XName CheckAccessName = "checkaccess";
		private static readonly XName EditAccessRequiredName = "editaccessrequired";
		private static readonly XName RestrictTransitionsName = "restricttransitions";

		public InOutElement(): base(ElementName) { }

		public InOutElement(XElement node): base(node) { }

		public bool CheckAccess {
			get => ElementAttribute<bool>.Get(this, CheckAccessName);
			set => ElementAttribute<bool>.Set(this, CheckAccessName, value);
		}

		public bool EditAccessRequired {
			get => ElementAttribute<bool>.Get(this, EditAccessRequiredName);
			set => ElementAttribute<bool>.Set(this, EditAccessRequiredName, value);
		}

		public bool RestrictTransitions {
			get => ElementAttribute<bool>.Get(this, RestrictTransitionsName);
			set => ElementAttribute<bool>.Set(this, RestrictTransitionsName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public PreCondsElement PreConds {
			get => (PreCondsElement)Element(PreCondsElement.ElementName);
			set => this.SetElement(PreCondsElement.ElementName, value);
		}

		public PostCondsElement PostConds {
			get => (PostCondsElement)Element(PostCondsElement.ElementName);
			set => this.SetElement(PostCondsElement.ElementName, value);
		}
	}
}
