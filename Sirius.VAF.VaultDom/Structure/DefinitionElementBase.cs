using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Archive;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public abstract class DefinitionElementBase: XElement {
		private static readonly XName AliasesName = "aliases";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";

		protected DefinitionElementBase(XName name): base(name) { }

		protected DefinitionElementBase(XElement other): base(other) { }

		public AliasString Aliases {
			get => ElementAttribute<AliasString>.Get(this, AliasesName, AliasString.Parse);
			set => ElementAttribute<AliasString>.Set(this, AliasesName, value, AliasString.Stringify);
		}

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, GuidName);
			set => ElementAttribute<Guid>.Set(this, GuidName, value, ArchiveDocument.StringifyGuid);
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
