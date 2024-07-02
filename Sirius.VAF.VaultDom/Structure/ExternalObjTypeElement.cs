using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ExternalObjTypeElement: XElement {
		public static readonly XName ElementName = "external";
		private static readonly XName ValueName = "value";

		public ExternalObjTypeElement(): base(ElementName) { }

		public ExternalObjTypeElement(XElement node): base(node) { }

		public new bool Value {
			get => ElementAttribute<bool>.Get(this, ValueName);
			set => ElementAttribute<bool>.Set(this, ValueName, value);
		}
	}
}
