using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Archive;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public class PseudoUserElement: XElement {
		public static readonly XName ElementName = "pseudouserelement";
		private static readonly XName IdName = "id";
		private static readonly XName GuidName = "guid";
		private static readonly XName NameName = "name";
		private static readonly XName PosName = "pos";
		private static readonly XName TypeName = "type";

		public PseudoUserElement(): base(ElementName) { }

		public PseudoUserElement(XElement node): base(node) { }

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public Guid? Guid {
			get => ElementAttribute<Guid?>.GetOrDefault(this, GuidName);
			set => ElementAttribute<Guid?>.SetOrRemove(this, GuidName, value, value => value.HasValue ? ArchiveDocument.StringifyGuid(value.Value) : null);
		}

		public new string Name {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public PseudoUserType Type {
			get => ElementAttribute<PseudoUserType>.Get(this, TypeName);
			set => ElementAttribute<PseudoUserType>.Set(this, TypeName, value);
		}
	}
}
