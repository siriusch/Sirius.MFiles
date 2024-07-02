using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ActionElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "action";
		private static readonly XName PosName = "pos";
		private static readonly XName TypeName = "type";
		private static readonly XName CreateSeparateAssignmentElementName = "CreateSeparateAssignment";

		public ActionElement(): base(ElementName) { }

		public ActionElement(XElement node): base(node) { }

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public string ActionType {
			get => ElementAttribute<string>.Get(this, TypeName);
			set => ElementAttribute<string>.Set(this, TypeName, value);
		}

		public CreateSeparateAssignmentElement CreateSeparateAssignment {
			get => (CreateSeparateAssignmentElement)Element(CreateSeparateAssignmentElementName);
			set => this.SetElement(CreateSeparateAssignmentElementName, value);
		}

		public override StateAction Type => StateAction.CreateSeparateAssignment;
	}
}
