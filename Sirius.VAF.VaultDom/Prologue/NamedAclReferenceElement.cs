using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Archive;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public class NamedAclReferenceElement: AclComponentElementBase {
		public static readonly XName ElementName = "namedacl";
		private static readonly XName AliasesName = "aliases";
		private static readonly XName BuiltinName = "builtin";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";
		private static readonly XName TypeName = "type";

		public NamedAclReferenceElement(): base(ElementName) { }

		public NamedAclReferenceElement(XElement other): base(other) { }

		public NamedAclType Type {
			get => ElementAttribute<NamedAclType>.GetOrDefault(this, TypeName);
			set => ElementAttribute<NamedAclType>.Set(this, TypeName, value);
		}

		public AliasString Aliases {
			get => ElementAttribute<AliasString>.Get(this, AliasesName, AliasString.Parse);
			set => ElementAttribute<AliasString>.Set(this, AliasesName, value, AliasString.Stringify);
		}

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, GuidName);
			set => ElementAttribute<Guid>.Set(this, GuidName, value, ArchiveDocument.StringifyGuid);
		}

		public bool Builtin {
			get => ElementAttribute<bool>.Get(this, BuiltinName);
			set => ElementAttribute<bool>.Set(this, BuiltinName, value);
		}

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public new string Name {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}
	}
}
