using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class TriggerElement: SelectOneElementBase<MFAutoStateTransitionMode> {
		private static readonly Mapping<MFAutoStateTransitionMode> TypeMapping = new() {
				{ "None", MFAutoStateTransitionMode.MFASTModeNone },
				{ "RelativeTime", MFAutoStateTransitionMode.MFASTModeRelativeTime },
				{ "CriteriaFulfilled", MFAutoStateTransitionMode.MFASTModeCriteriaFulfilled },
				{ "AllowedByScript", MFAutoStateTransitionMode.MFASTModeAllowedByScript },
				{ "AssignmentComplete", MFAutoStateTransitionMode.MFASTModeAssignmentComplete },
				{ "AssignmentRejected", MFAutoStateTransitionMode.MFASTModeAssignmentRejected }
		};

		public static readonly XName ElementName = "trigger";

		public TriggerElement(): base(ElementName) { }

		public TriggerElement(XElement node): base(node) { }

		protected override MFAutoStateTransitionMode ParseType(string str) {
			return TypeMapping.Parse(str);
		}

		protected override string StringifyType(MFAutoStateTransitionMode value) {
			return TypeMapping.Stringify(value);
		}

		public RelativeTimeElement RelativeTime {
			get => (RelativeTimeElement)Element(RelativeTimeElement.ElementName);
			set => this.SetElement(RelativeTimeElement.ElementName, value);
		}

		public CriteriaFulfilledElement CriteriaFulfilled {
			get => (CriteriaFulfilledElement)Element(CriteriaFulfilledElement.ElementName);
			set => this.SetElement(CriteriaFulfilledElement.ElementName, value);
		}

		public AllowedByScriptElement AllowedByScript {
			get => (AllowedByScriptElement)Element(AllowedByScriptElement.ElementName);
			set => this.SetElement(AllowedByScriptElement.ElementName, value);
		}

		public AssignmentCompleteElement AssignmentComplete {
			get => (AssignmentCompleteElement)Element(AssignmentCompleteElement.ElementName);
			set => this.SetElement(AssignmentCompleteElement.ElementName, value);
		}

		public AssignmentRejectedElement AssignmentRejected {
			get => (AssignmentRejectedElement)Element(AssignmentRejectedElement.ElementName);
			set => this.SetElement(AssignmentRejectedElement.ElementName, value);
		}
	}
}
