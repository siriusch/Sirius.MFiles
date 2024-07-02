using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class StructureElement: XElement {
		public static readonly XName ElementName = "structure";
		internal static readonly XName LanguagesName = "languages";
		internal static readonly XName ObjTypesName = "objtypes";
		internal static readonly XName PropertyDefsName = "propertydefs";
		internal static readonly XName NamedAclsName = "namedacls";
		internal static readonly XName ClassesName = "classes";
		internal static readonly XName ClassGroupsName = "classgroups";
		internal static readonly XName WorkflowsName = "workflows";
		internal static readonly XName StatesName = "states";
		internal static readonly XName TransitionsName = "transitions";
		internal static readonly XName UserAccountsName = "useraccounts";
		internal static readonly XName UserGroupsName = "usergroups";
		internal static readonly XName ViewDefsName = "viewdefs";
		internal static readonly XName MetadataCardConfigurationsName = "metadatacardconfigurations";

		public StructureElement(): base(ElementName) { }

		public StructureElement(XElement node): base(node) { }

		public StructureCapabilitiesElement Capabilities {
			get => (StructureCapabilitiesElement)Element(StructureCapabilitiesElement.ElementName);
			set => this.SetElement(StructureCapabilitiesElement.ElementName, value);
		}

		public CollectionElement<LanguageElement> Languages {
			get => (CollectionElement<LanguageElement>)Element(LanguagesName);
			set => this.SetElement(LanguagesName, value);
		}

		public CollectionElement<ObjTypeElement> ObjTypes {
			get => (CollectionElement<ObjTypeElement>)Element(ObjTypesName);
			set => this.SetElement(ObjTypesName, value);
		}

		public CollectionElement<PropertyDefElement> PropertyDefs {
			get => (CollectionElement<PropertyDefElement>)Element(PropertyDefsName);
			set => this.SetElement(PropertyDefsName, value);
		}

		public CollectionElement<NamedAclElement> NamedAcls {
			get => (CollectionElement<NamedAclElement>)Element(NamedAclsName);
			set => this.SetElement(NamedAclsName, value);
		}

		public CollectionElement<ClassElement> Classes {
			get => (CollectionElement<ClassElement>)Element(ClassesName);
			set => this.SetElement(ClassesName, value);
		}

		public CollectionElement<ClassGroupElement> ClassGroups {
			get => (CollectionElement<ClassGroupElement>)Element(ClassGroupsName);
			set => this.SetElement(ClassGroupsName, value);
		}

		public CollectionElement<WorkflowReferenceElement> Workflows {
			get => (CollectionElement<WorkflowReferenceElement>)Element(WorkflowsName);
			set => this.SetElement(WorkflowsName, value);
		}

		public CollectionElement<StateElement> States {
			get => (CollectionElement<StateElement>)Element(StatesName);
			set => this.SetElement(StatesName, value);
		}

		public CollectionElement<TransitionElement> Transitions {
			get => (CollectionElement<TransitionElement>)Element(TransitionsName);
			set => this.SetElement(TransitionsName, value);
		}

		public CollectionElement<UserElement> UserAccounts {
			get => (CollectionElement<UserElement>)Element(UserAccountsName);
			set => this.SetElement(UserAccountsName, value);
		}

		public CollectionElement<GroupElement> UserGroups {
			get => (CollectionElement<GroupElement>)Element(UserGroupsName);
			set => this.SetElement(UserGroupsName, value);
		}

		public CollectionElement<ViewElement> ViewDefs {
			get => (CollectionElement<ViewElement>)Element(ViewDefsName);
			set => this.SetElement(ViewDefsName, value);
		}

		public CollectionElement<ConfigurationRuleElement> MetadataCardConfigurations {
			get => (CollectionElement<ConfigurationRuleElement>)Element(MetadataCardConfigurationsName);
			set => this.SetElement(MetadataCardConfigurationsName, value);
		}
	}
}
