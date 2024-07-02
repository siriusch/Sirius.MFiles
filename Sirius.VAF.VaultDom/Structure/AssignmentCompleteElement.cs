using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class AssignmentCompleteElement: ActivableElementBase<MFAutoStateTransitionMode> {
		public static readonly XName ElementName = "AssignmentComplete";

		public AssignmentCompleteElement(): base(ElementName) { }

		public AssignmentCompleteElement(XElement node): base(node) { }

		public override MFAutoStateTransitionMode Type => MFAutoStateTransitionMode.MFASTModeAssignmentComplete;
	}
}
