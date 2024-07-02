using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ObjTypeElement: DefinitionElementBase {
		public static readonly XName ElementName = "objtype";
		internal static readonly XName AclForNewObjsName = "aclfornewobjs";
		internal static readonly XName BrowsingTargetsName = "browsingtargets";
		private static readonly XName DefaultPdIdName = "defaultpdid";
		private static readonly XName ExternalName = "external";
		private static readonly XName FullName = "full";
		private static readonly XName NamePluralName = "nameplural";
		private static readonly XName NameSingularName = "namesingular";
		private static readonly XName OwnerPdIdName = "ownerpdid";
		private static readonly XName RealObjName = "realobj";
		private static readonly XName TranslatableName = "translatable";
		private static readonly XName BuiltinName = "builtin";
		private static readonly XName CustomDataName = "customdata";

		public ObjTypeElement(): base(ElementName) { }

		public ObjTypeElement(XElement node): base(node) { }

		public bool Builtin {
			get => ElementAttribute<bool>.Get(this, BuiltinName);
			set => ElementAttribute<bool>.Set(this, BuiltinName, value);
		}

		public int DefaultPdId {
			get => ElementAttribute<int>.Get(this, DefaultPdIdName);
			set => ElementAttribute<int>.Set(this, DefaultPdIdName, value);
		}

		public bool IsExternal {
			get => ElementAttribute<bool>.Get(this, ExternalName);
			set => ElementAttribute<bool>.Set(this, ExternalName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public string Nameplural {
			get => ElementAttribute<string>.Get(this, NamePluralName);
			set => ElementAttribute<string>.Set(this, NamePluralName, value);
		}

		public string Namesingular {
			get => ElementAttribute<string>.Get(this, NameSingularName);
			set => ElementAttribute<string>.Set(this, NameSingularName, value);
		}

		public int OwnerPdId {
			get => ElementAttribute<int>.Get(this, OwnerPdIdName);
			set => ElementAttribute<int>.Set(this, OwnerPdIdName, value);
		}

		public bool RealObj {
			get => ElementAttribute<bool>.Get(this, RealObjName);
			set => ElementAttribute<bool>.Set(this, RealObjName, value);
		}

		public bool Translatable {
			get => ElementAttribute<bool>.Get(this, TranslatableName);
			set => ElementAttribute<bool>.Set(this, TranslatableName, value);
		}

		public ObjTypeFlagsElement Flags {
			get => (ObjTypeFlagsElement)Element(ObjTypeFlagsElement.ElementName);
			set => this.SetElement(ObjTypeFlagsElement.ElementName, value);
		}

		public IconElement Icon {
			get => (IconElement)Element(IconElement.ElementName);
			set => this.SetElement(IconElement.ElementName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public AclReferenceElement AclForNewObjs {
			get => (AclReferenceElement)Element(AclForNewObjsName);
			set => this.SetElement(AclForNewObjsName, value);
		}

		public CollectionElement<TargetElement> BrowsingTargets {
			get => (CollectionElement<TargetElement>)Element(BrowsingTargetsName);
			set => this.SetElement(BrowsingTargetsName, value);
		}

		public SortingElement Sorting {
			get => (SortingElement)Element(SortingElement.ElementName);
			set => this.SetElement(SortingElement.ElementName, value);
		}

		public ExternalObjTypeElement External {
			get => (ExternalObjTypeElement)Element(ExternalObjTypeElement.ElementName);
			set => this.SetElement(ExternalObjTypeElement.ElementName, value);
		}

		public XElement Customdata {
			get => Element(CustomDataName);
			set => this.SetElement(CustomDataName, value);
		}
	}
}
