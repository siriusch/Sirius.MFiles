using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Archive;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Content {
	public class ParentReferenceElement: XElement {
		public static readonly XName ElementName = "parent";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName ValueName = "value";

		public ParentReferenceElement(): base(ElementName) { }

		public ParentReferenceElement(XElement node): base(node) { }

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, GuidName);
			set => ElementAttribute<Guid>.Set(this, GuidName, value, ArchiveDocument.StringifyGuid);
		}

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public new string Value {
			get => ElementAttribute<string>.Get(this, ValueName);
			set => ElementAttribute<string>.Set(this, ValueName, value);
		}
	}
}
