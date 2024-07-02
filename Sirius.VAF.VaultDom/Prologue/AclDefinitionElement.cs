using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public class AclDefinitionElement: XElement {
		public static readonly XName ElementName = "acl";
		internal static readonly XName AclComponentName = "aclcomponent";
		private static readonly XName IdName = "id";
		private static readonly XName IgnoreLciName = "ignorelci";

		public AclDefinitionElement(): base(ElementName) { }

		public AclDefinitionElement(XElement node): base(node) { }

		public string Id {
			get => ElementAttribute<string>.Get(this, IdName);
			set => ElementAttribute<string>.Set(this, IdName, value);
		}

		public bool IgnoreLci {
			get => ElementAttribute<bool>.Get(this, IgnoreLciName);
			set => ElementAttribute<bool>.Set(this, IgnoreLciName, value);
		}

		public CollectionElement<AclComponentElementBase> AclComponent {
			get => (CollectionElement<AclComponentElementBase>)Element(AclComponentName);
			set => this.SetElement(AclComponentName, value);
		}
	}
}
