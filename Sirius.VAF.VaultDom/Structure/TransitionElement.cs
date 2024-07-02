using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class TransitionElement: XElement {
		public static readonly XName ElementName = "transition";

		public TransitionElement(): base(ElementName) { }

		public TransitionElement(XElement node): base(node) { }
	}
}
