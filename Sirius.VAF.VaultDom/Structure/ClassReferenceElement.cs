using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ClassReferenceElement: XElement {
		public static readonly XName ElementName = "class";
		private static readonly XName IdName = "id";

		public ClassReferenceElement(): this(ElementName) { }

		public ClassReferenceElement(XName name): base(name) { }

		public ClassReferenceElement(XElement node): base(node) { }

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}
	}
}
