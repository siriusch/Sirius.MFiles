using System.Xml.Linq;

using Sirius.VAF.VaultDom.Structure.Formatting;

namespace Sirius.VAF.VaultDom.Structure {
	public class FormattingElement: SelectOneElementBase<FormattingType> {
		public static readonly XName ElementName = "formatting";

		public FormattingElement(): base(ElementName) { }

		public FormattingElement(XElement node): base(node) { }
	}
}
