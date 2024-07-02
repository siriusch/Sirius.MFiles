using System.Xml.Linq;

using Sirius.VAF.VaultDom.Prologue;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AclContentElement: XElement {
		public static readonly XName ElementName = "content";
		internal static readonly XName AclComponentName = "aclcomponent";
		private static readonly XName IgnoreLciName = "ignorelci";

		public AclContentElement(): base(ElementName) { }

		public AclContentElement(XElement node): base(node) { }

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
