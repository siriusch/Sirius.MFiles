using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class NamePropertyElement: XElement {
		public static readonly XName ElementName = "nameproperty";
		private static readonly XName IdName = "id";

		public NamePropertyElement(): base(ElementName) { }

		public NamePropertyElement(XElement node): base(node) { }

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}
	}
}
