using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AssignmentInfoElement: XElement {
		public static readonly XName ElementName = "assignmentinfo";
		internal static readonly XName SignaturesName = "signatures";
		private static readonly XName ApprovedByAnyName = "approvedbyany";
		private static readonly XName TypeName = "type";

		public AssignmentInfoElement(): base(ElementName) { }

		public AssignmentInfoElement(XElement node): base(node) { }

		public bool ApprovedByAny {
			get => ElementAttribute<bool>.Get(this, ApprovedByAnyName);
			set => ElementAttribute<bool>.Set(this, ApprovedByAnyName, value);
		}

		public AssignmentType Type {
			get => ElementAttribute<AssignmentType>.Get(this, TypeName);
			set => ElementAttribute<AssignmentType>.Set(this, TypeName, value);
		}

		public CollectionElement<SignatureElement> Signatures {
			get => (CollectionElement<SignatureElement>)Element(SignaturesName);
			set => this.SetElement(SignaturesName, value);
		}
	}
}
