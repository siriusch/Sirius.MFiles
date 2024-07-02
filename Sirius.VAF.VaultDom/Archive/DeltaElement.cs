using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Archive {
	public class DeltaElement: XElement {
		public static readonly XName ElementName = "delta";

		public DeltaElement(): base(ElementName) { }

		public DeltaElement(XElement other): base(other) { }

		public bool Applied {
			get => ElementAttribute<bool>.Get(this, "applied");
			set => ElementAttribute<bool>.Set(this, "applied", value);
		}
	}
}
