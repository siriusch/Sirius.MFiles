using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ActionsElement: XElement, INotifyActiveChange<StateAction> {
		public static readonly XName ElementName = "actions";
		private static readonly XName ValueName = "value";
		private static readonly XName DeleteElementName = "delete";
		private static readonly XName MarkForArchivingElementName = "markforarchiving";
		private static readonly XName ChangePermissionsElementName = "changepermissions";
		private static readonly XName CreateAssignmentElementName = "createassignment";
		private static readonly XName SendNotificationElementName = "sendnotification";
		private static readonly XName SetPropertiesElementName = "setproperties";
		private static readonly XName ConvertToPdfElementName = "converttopdf";
		private static readonly XName RunScriptElementName = "runscript";

		public ActionsElement(): base(ElementName) { }

		public ActionsElement(XElement node): base(node) { }

		public new int Value {
			get => ElementAttribute<int>.Get(this, ValueName);
			set => ElementAttribute<int>.Set(this, ValueName, value);
		}

		public DeleteElement Delete {
			get => (DeleteElement)Element(DeleteElementName);
			set => this.SetElement(DeleteElementName, value);
		}

		public MarkForArchivingElement MarkForArchiving {
			get => (MarkForArchivingElement)Element(MarkForArchivingElementName);
			set => this.SetElement(MarkForArchivingElementName, value);
		}

		public ChangePermissionsElement ChangePermissions {
			get => (ChangePermissionsElement)Element(ChangePermissionsElementName);
			set => this.SetElement(ChangePermissionsElementName, value);
		}

		public CreateAssignmentElement CreateAssignment {
			get => (CreateAssignmentElement)Element(CreateAssignmentElementName);
			set => this.SetElement(CreateAssignmentElementName, value);
		}

		public SendNotificationElement SendNotification {
			get => (SendNotificationElement)Element(SendNotificationElementName);
			set => this.SetElement(SendNotificationElementName, value);
		}

		public SetPropertiesElement SetProperties {
			get => (SetPropertiesElement)Element(SetPropertiesElementName);
			set => this.SetElement(SetPropertiesElementName, value);
		}

		public ConvertToPdfElement ConvertToPdf {
			get => (ConvertToPdfElement)Element(ConvertToPdfElementName);
			set => this.SetElement(ConvertToPdfElementName, value);
		}

		public RunScriptElement RunScript {
			get => (RunScriptElement)Element(RunScriptElementName);
			set => this.SetElement(RunScriptElementName, value);
		}

		public IEnumerable<CreateSeparateAssignmentElement> CreateSeparateAssignment =>
				Elements(ActionElement.ElementName)
						.Cast<ActionElement>()
						.Where(a => a.Active)
						.Select(a => a.CreateSeparateAssignment)
						.Where(csa => csa != null);

		void INotifyActiveChange<StateAction>.ActiveChanged(IActivable<StateAction> sender) {
			if (!sender.Active) {
				if (sender.Type != StateAction.CreateSeparateAssignment || !CreateSeparateAssignment.Any()) {
					Value &= ~(int)sender.Type;
				}
			} else {
				Value |= (int)sender.Type;
			}
		}
	}
}
