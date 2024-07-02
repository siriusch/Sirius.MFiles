using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class AssignmentRejectedElement: ActivableElementBase<MFAutoStateTransitionMode> {
		public static readonly XName ElementName = "AssignmentRejected";

		public AssignmentRejectedElement(): base(ElementName) { }

		public AssignmentRejectedElement(XElement node): base(node) { }

		public override MFAutoStateTransitionMode Type => MFAutoStateTransitionMode.MFASTModeAssignmentRejected;
	}
}
