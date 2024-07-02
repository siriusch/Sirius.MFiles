using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class FreeformElement: XElement {
		public static readonly XName ElementName = "freeform";
		private static readonly XName PdIdName = "pdid";
		private static readonly XName ValueName = "value";

		public FreeformElement(): base(ElementName) { }

		public FreeformElement(XElement node): base(node) { }

		public int PdId {
			get => ElementAttribute<int>.Get(this, PdIdName);
			set => ElementAttribute<int>.Set(this, PdIdName, value);
		}

		public new bool Value {
			get => ElementAttribute<bool>.Get(this, ValueName);
			set => ElementAttribute<bool>.Set(this, ValueName, value);
		}
	}
}
