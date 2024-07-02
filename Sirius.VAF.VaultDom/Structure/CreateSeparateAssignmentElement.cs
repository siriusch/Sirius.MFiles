using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class CreateSeparateAssignmentElement: XElement {
		public static readonly XName ElementName = "CreateSeparateAssignment";
		private static readonly XName ActiveName = "active";
		private static readonly XName SeparateName = "separate";
		private static readonly XName TitleElementName = "title";
		private static readonly XName DescriptionElementName = "description";
		private static readonly XName DeadlineElementName = "deadline";
		private static readonly XName AssignedToName = "assignedto";
		private static readonly XName MonitoredByName = "monitoredby";

		public CreateSeparateAssignmentElement(): base(ElementName) { }

		public CreateSeparateAssignmentElement(XElement node): base(node) { }

		public bool Active {
			get => ElementAttribute<bool>.Get(this, ActiveName);
			set => ElementAttribute<bool>.Set(this, ActiveName, value);
		}

		public bool Separate {
			get => ElementAttribute<bool>.Get(this, SeparateName);
			set => ElementAttribute<bool>.Set(this, SeparateName, value);
		}

		public XElement Title {
			get => Element(TitleElementName);
			set => this.SetElement(TitleElementName, value);
		}

		public XElement Description {
			get => Element(DescriptionElementName);
			set => this.SetElement(DescriptionElementName, value);
		}

		public DeadlineElement Deadline {
			get => (DeadlineElement)Element(DeadlineElement.ElementName);
			set => this.SetElement(DeadlineElement.ElementName, value);
		}

		public AssignedToElement AssignedTo {
			get => (AssignedToElement)Element(AssignedToElement.ElementName);
			set => this.SetElement(AssignedToElement.ElementName, value);
		}

		public MonitoredByElement MonitoredBy {
			get => (MonitoredByElement)Element(MonitoredByElement.ElementName);
			set => this.SetElement(MonitoredByElement.ElementName, value);
		}
	}
}
