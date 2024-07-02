using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class StateElement: DefinitionElementBase {
		public static readonly XName ElementName = "state.2";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName FullName = "full";
		private static readonly XName WfIdName = "wfid";
		private static readonly XName DescriptionName = "description";

		public StateElement(): base(ElementName) { }

		public StateElement(XElement node): base(node) { }

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public int WfId {
			get => ElementAttribute<int>.Get(this, WfIdName);
			set => ElementAttribute<int>.Set(this, WfIdName, value);
		}

		public XElement Description {
			get => Element(DescriptionName);
			set => this.SetElement(DescriptionName, value);
		}

		public IconElement Icon {
			get => (IconElement)Element(IconElement.ElementName);
			set => this.SetElement(IconElement.ElementName, value);
		}

		public InOutElement InOut {
			get => (InOutElement)Element(InOutElement.ElementName);
			set => this.SetElement(InOutElement.ElementName, value);
		}

		public ActionsElement Actions {
			get => (ActionsElement)Element(ActionsElement.ElementName);
			set => this.SetElement(ActionsElement.ElementName, value);
		}
	}
}
