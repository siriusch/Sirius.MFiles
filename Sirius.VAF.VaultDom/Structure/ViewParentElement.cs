using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewParentElement: XElement {
		public static readonly XName ElementName = "parent";
		private static readonly XName ValueName = "value";
		private static readonly XName ViewLocationElementName = "viewlocation";

		public ViewParentElement(): base(ElementName) { }

		public ViewParentElement(XElement node): base(node) { }

		public new bool Value {
			get => ElementAttribute<bool>.Get(this, ValueName);
			set => ElementAttribute<bool>.Set(this, ValueName, value);
		}

		public ViewLocationElement ViewLocation {
			get => (ViewLocationElement)Element(ViewLocationElementName);
			set => this.SetElement(ViewLocationElementName, value);
		}
	}
}
