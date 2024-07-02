using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class WorkflowReferenceElement: XElement {
		public static readonly XName ElementName = "workflow";
		private static readonly XName ForcedName = "forced";
		private static readonly XName IdName = "id";

		public WorkflowReferenceElement(): base(ElementName) { }

		public WorkflowReferenceElement(XElement node): base(node) { }

		public bool Forced {
			get => ElementAttribute<bool>.Get(this, ForcedName);
			set => ElementAttribute<bool>.Set(this, ForcedName, value);
		}

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}
	}
}
