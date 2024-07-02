using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewElement: DefinitionElementBase {
		public static readonly XName ElementName = "view";
		private static readonly XName CategoryName = "category";
		private static readonly XName ChangedAtName = "changedat";
		private static readonly XName CommonName = "common";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName FullName = "full";
		private static readonly XName TypeName = "type";
		private static readonly XName VisibleName = "visible";
		internal static readonly XName UiSettingsName = "uisettings";

		public ViewElement(): base(ElementName) { }

		public ViewElement(XElement node): base(node) { }

		public string Category {
			get => ElementAttribute<string>.Get(this, CategoryName);
			set => ElementAttribute<string>.Set(this, CategoryName, value);
		}

		public DateTime ChangedAt {
			get => ElementAttribute<DateTime>.Get(this, ChangedAtName);
			set => ElementAttribute<DateTime>.Set(this, ChangedAtName, value);
		}

		public bool Common {
			get => ElementAttribute<bool>.Get(this, CommonName);
			set => ElementAttribute<bool>.Set(this, CommonName, value);
		}

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public string Type {
			get => ElementAttribute<string>.Get(this, TypeName);
			set => ElementAttribute<string>.Set(this, TypeName, value);
		}

		public bool Visible {
			get => ElementAttribute<bool>.Get(this, VisibleName);
			set => ElementAttribute<bool>.Set(this, VisibleName, value);
		}

		public ViewFlagsElement Flags {
			get => (ViewFlagsElement)Element(ViewFlagsElement.ElementName);
			set => this.SetElement(ViewFlagsElement.ElementName, value);
		}

		public ViewAllowedOperationsElement AllowedOperations {
			get => (ViewAllowedOperationsElement)Element(ViewAllowedOperationsElement.ElementName);
			set => this.SetElement(ViewAllowedOperationsElement.ElementName, value);
		}

		public SearchDefElement SearchDef {
			get => (SearchDefElement)Element(SearchDefElement.ElementName);
			set => this.SetElement(SearchDefElement.ElementName, value);
		}

		public ViewParentElement ParentView {
			get => (ViewParentElement)Element(ViewParentElement.ElementName);
			set => this.SetElement(ViewParentElement.ElementName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public CollectionElement<ViewUiSettingElement> UiSettings {
			get => (CollectionElement<ViewUiSettingElement>)Element(UiSettingsName);
			set => this.SetElement(UiSettingsName, value);
		}
	}
}
