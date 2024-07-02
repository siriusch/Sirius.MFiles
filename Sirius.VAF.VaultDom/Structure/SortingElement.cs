using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class SortingElement: XElement {
		public static readonly XName ElementName = "sorting";
		private static readonly XName AscendingName = "ascending";
		private static readonly XName TypeName = "type";

		public SortingElement(): base(ElementName) { }

		public SortingElement(XElement node): base(node) { }

		public bool Ascending {
			get => ElementAttribute<bool>.Get(this, AscendingName);
			set => ElementAttribute<bool>.Set(this, AscendingName, value);
		}

		public string Type {
			get => ElementAttribute<string>.Get(this, TypeName);
			set => ElementAttribute<string>.Set(this, TypeName, value);
		}
	}
}
