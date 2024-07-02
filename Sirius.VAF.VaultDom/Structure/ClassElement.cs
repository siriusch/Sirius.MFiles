using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ClassElement: DefinitionElementBase {
		public static readonly XName ElementName = "class";
		internal static readonly XName AssociatedPropertiesName = "associatedproperties";
		private static readonly XName BuiltinName = "builtin";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName FullName = "full";
		private static readonly XName OtIdName = "otid";
		private static readonly XName OtNameName = "otname";

		public ClassElement(): base(ElementName) { }

		public ClassElement(XElement node): base(node) { }

		public bool Builtin {
			get => ElementAttribute<bool>.Get(this, BuiltinName);
			set => ElementAttribute<bool>.Set(this, BuiltinName, value);
		}

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public int OtId {
			get => ElementAttribute<int>.Get(this, OtIdName);
			set => ElementAttribute<int>.Set(this, OtIdName, value);
		}

		public string OtName {
			get => ElementAttribute<string>.Get(this, OtNameName);
			set => ElementAttribute<string>.Set(this, OtNameName, value);
		}

		public CollectionElement<PropertyElement> AssociatedProperties {
			get => (CollectionElement<PropertyElement>)Element(AssociatedPropertiesName);
			set => this.SetElement(AssociatedPropertiesName, value);
		}

		public NamePropertyElement NameProperty {
			get => (NamePropertyElement)Element(NamePropertyElement.ElementName);
			set => this.SetElement(NamePropertyElement.ElementName, value);
		}

		public WorkflowReferenceElement Workflow {
			get => (WorkflowReferenceElement)Element(WorkflowReferenceElement.ElementName);
			set => this.SetElement(WorkflowReferenceElement.ElementName, value);
		}

		public AssignmentInfoElement AssignmentInfo {
			get => (AssignmentInfoElement)Element(AssignmentInfoElement.ElementName);
			set => this.SetElement(AssignmentInfoElement.ElementName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public AutomaticAclElement AutomaticAcl {
			get => (AutomaticAclElement)Element(AutomaticAclElement.ElementName);
			set => this.SetElement(AutomaticAclElement.ElementName, value);
		}
	}
}
