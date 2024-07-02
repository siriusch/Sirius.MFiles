using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class OwnerElement: XElement {
		public static readonly XName ElementName = "owner";
		private static readonly XName NameName = "name";
		private static readonly XName IdName = "id";

		public OwnerElement(): base(ElementName) { }

		public OwnerElement(XElement node): base(node) { }

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
