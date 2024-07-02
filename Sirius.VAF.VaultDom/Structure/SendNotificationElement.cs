using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class SendNotificationElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "sendnotification";
		private static readonly XName SubjectName = "subject";
		private static readonly XName MessageName = "message";

		public SendNotificationElement(): base(ElementName) { }

		public SendNotificationElement(XElement node): base(node) { }

		public XElement Subject {
			get => Element(SubjectName);
			set => this.SetElement(SubjectName, value);
		}

		public XElement Message {
			get => Element(MessageName);
			set => this.SetElement(MessageName, value);
		}

		public RecipientsElement Recipients {
			get => (RecipientsElement)Element(RecipientsElement.ElementName);
			set => this.SetElement(RecipientsElement.ElementName, value);
		}

		public override StateAction Type => StateAction.SendNotification;
	}
}
