using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ConditionElement: XElement {
		public static readonly XName ElementName = "condition";

		public ConditionElement(): base(ElementName) { }

		public ConditionElement(XElement node): base(node) { }

		public string Type {
			get => ElementAttribute<string>.Get(this, "type");
			set => ElementAttribute<string>.Set(this, "type", value);
		}
	}
}
