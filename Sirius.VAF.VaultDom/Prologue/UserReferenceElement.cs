using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Prologue {
	public class UserReferenceElement: SubjectReferenceElementBase {
		public static readonly XName ElementName = "user";

		public UserReferenceElement(): base(ElementName) { }

		public UserReferenceElement(XElement node): base(node) { }
	}
}
