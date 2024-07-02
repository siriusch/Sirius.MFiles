using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewOverlappingElement: XElement {
		public static readonly XName ElementName = "overlapping";
		private static readonly XName ValueName = "value";

		public ViewOverlappingElement(): base(ElementName) { }

		public ViewOverlappingElement(XElement node): base(node) { }

		public new bool Value {
			get => ElementAttribute<bool>.Get(this, ValueName);
			set => ElementAttribute<bool>.Set(this, ValueName, value);
		}
	}
}
