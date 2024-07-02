using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public abstract class SubjectReferenceElementBase: XElement {
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";

		protected SubjectReferenceElementBase(XName name): base(name) { }

		protected SubjectReferenceElementBase(XElement node): base(node) { }

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
