using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class WorkflowElement: DefinitionElementBase {
		public static readonly XName ElementName = "workflow.2";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName FullName = "full";
		private static readonly XName DescriptionName = "description";
		internal static readonly XName WithClassName = "withclass";
		internal static readonly XName StatesName = "states";
		internal static readonly XName TransitionsName = "transitions";

		public WorkflowElement(): base(ElementName) { }

		public WorkflowElement(XElement node): base(node) { }

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public XElement Description {
			get => Element(DescriptionName);
			set => this.SetElement(DescriptionName, value);
		}

		public ClassReferenceElement WithClass {
			get => (ClassReferenceElement)Element(WithClassName);
			set => this.SetElement(WithClassName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public LayoutElement Layout {
			get => (LayoutElement)Element(LayoutElement.ElementName);
			set => this.SetElement(LayoutElement.ElementName, value);
		}

		public CollectionElement<StateReferenceElement> States {
			get => (CollectionElement<StateReferenceElement>)Element(StatesName);
			set => this.SetElement(StatesName, value);
		}

		public CollectionElement<TransitionReferenceElement> Transitions {
			get => (CollectionElement<TransitionReferenceElement>)Element(TransitionsName);
			set => this.SetElement(TransitionsName, value);
		}
	}
}
