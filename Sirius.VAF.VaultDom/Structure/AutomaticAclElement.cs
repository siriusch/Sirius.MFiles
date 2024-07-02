using System.Xml.Linq;

using Sirius.VAF.VaultDom.Prologue;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AutomaticAclElement: CollectionElement<NamedAclReferenceElement> {
		public static readonly XName ElementName = "automaticacl";
		private static readonly XName CanDeactivateName = "candeactivate";
		private static readonly XName UsesOwnAclName = "usesownacl";

		public AutomaticAclElement(): base(ElementName) { }

		public AutomaticAclElement(XElement other): base(other) { }

		public bool CanDeactivate {
			get => ElementAttribute<bool>.Get(this, CanDeactivateName);
			set => ElementAttribute<bool>.Set(this, CanDeactivateName, value);
		}

		public bool UsesOwnAcl {
			get => ElementAttribute<bool>.Get(this, UsesOwnAclName);
			set => ElementAttribute<bool>.Set(this, UsesOwnAclName, value);
		}
	}
}
