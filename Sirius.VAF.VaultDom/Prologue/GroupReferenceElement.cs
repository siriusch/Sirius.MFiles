using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Prologue {
	public class GroupReferenceElement: SubjectReferenceElementBase {
		public static readonly XName ElementName = "group";

		public GroupReferenceElement(): base(ElementName) { }

		public GroupReferenceElement(XElement node): base(node) { }
	}
}
