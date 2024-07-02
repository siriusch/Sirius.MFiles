using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Content {
	public class CapabilitiesElement: XElement {
		public static readonly XName ElementName = "capabilities";
		private static readonly XName ValueName = "value";

		public CapabilitiesElement(): base(ElementName) { }

		public CapabilitiesElement(XElement node): base(node) { }

		public new int Value {
			get => ElementAttribute<int>.Get(this, ValueName);
			set => ElementAttribute<int>.Set(this, ValueName, value);
		}
	}
}
