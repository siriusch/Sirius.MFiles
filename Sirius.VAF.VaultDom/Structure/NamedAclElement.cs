using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class NamedAclElement: DefinitionElementBase {
		public static readonly XName ElementName = "namedacl";
		private static readonly XName BuiltinName = "builtin";
		private static readonly XName FullName = "full";
		private static readonly XName TypeName = "type";

		public NamedAclElement(): base(ElementName) { }

		public NamedAclElement(XElement node): base(node) { }

		public bool Builtin {
			get => ElementAttribute<bool>.Get(this, BuiltinName);
			set => ElementAttribute<bool>.Set(this, BuiltinName, value);
		}

		public bool Full {
			get => ElementAttribute<bool>.Get(this, FullName);
			set => ElementAttribute<bool>.Set(this, FullName, value);
		}

		public string Type {
			get => ElementAttribute<string>.Get(this, TypeName);
			set => ElementAttribute<string>.Set(this, TypeName, value);
		}

		public AclForNaclElement AclForNacl {
			get => (AclForNaclElement)Element(AclForNaclElement.ElementName);
			set => this.SetElement(AclForNaclElement.ElementName, value);
		}

		public AclContentElement Content {
			get => (AclContentElement)Element(AclContentElement.ElementName);
			set => this.SetElement(AclContentElement.ElementName, value);
		}
	}
}
