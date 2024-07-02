using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class PropertyDefElement: DefinitionElementBase {
		public static readonly XName ElementName = "propertydef";
		internal static readonly XName DefaultValueName = "defaultvalue";
		private static readonly XName BuiltinName = "builtin";
		private static readonly XName ControlTypeName = "controltype";
		private static readonly XName DisallowAsGroupingLevelName = "disallowasgroupinglevel";
		private static readonly XName FullName = "full";
		private static readonly XName TipName = "tip";
		private static readonly XName UpdateTypeName = "updatetype";

		public PropertyDefElement(): base(ElementName) { }

		public PropertyDefElement(XElement node): base(node) { }

		public bool Builtin {
			get => ElementAttribute<bool>.Get(this, BuiltinName);
			set => ElementAttribute<bool>.Set(this, BuiltinName, value);
		}

		public string ControlType {
			get => ElementAttribute<string>.Get(this, ControlTypeName);
			set => ElementAttribute<string>.Set(this, ControlTypeName, value);
		}

		public bool DisallowAsGroupingLevel {
			get => ElementAttribute<bool>.Get(this, DisallowAsGroupingLevelName);
			set => ElementAttribute<bool>.Set(this, DisallowAsGroupingLevelName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public string Tip {
			get => ElementAttribute<string>.Get(this, TipName);
			set => ElementAttribute<string>.Set(this, TipName, value);
		}

		public PropertyDefUpdateType UpdateType {
			get => ElementAttribute<PropertyDefUpdateType>.Get(this, UpdateTypeName);
			set => ElementAttribute<PropertyDefUpdateType>.Set(this, UpdateTypeName, value);
		}

		public PropertyDefFlagsElement Flags {
			get => (PropertyDefFlagsElement)Element(PropertyDefFlagsElement.ElementName);
			set => this.SetElement(PropertyDefFlagsElement.ElementName, value);
		}

		public DataTypeElement DataType {
			get => (DataTypeElement)Element(DataTypeElement.ElementName);
			set => this.SetElement(DataTypeElement.ElementName, value);
		}

		public IconElement Icon {
			get => (IconElement)Element(IconElement.ElementName);
			set => this.SetElement(IconElement.ElementName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public XElement DefaultValue {
			get => Element(DefaultValueName);
			set => this.SetElement(DefaultValueName, value);
		}

		public AutomaticValueElement AutomaticValue {
			get => (AutomaticValueElement)Element(AutomaticValueElement.ElementName);
			set => this.SetElement(AutomaticValueElement.ElementName, value);
		}

		public ObjectTypeElement ObjectType {
			get => (ObjectTypeElement)Element(ObjectTypeElement.ElementName);
			set => this.SetElement(ObjectTypeElement.ElementName, value);
		}

		public ValidationElement Validation {
			get => (ValidationElement)Element(ValidationElement.ElementName);
			set => this.SetElement(ValidationElement.ElementName, value);
		}

		public FormattingElement Formatting {
			get => (FormattingElement)Element(FormattingElement.ElementName);
			set => this.SetElement(FormattingElement.ElementName, value);
		}

		public XElement CustomData {
			get => Element(CustomData.Name);
			set => this.SetElement(CustomData.Name, value);
		}
	}
}
