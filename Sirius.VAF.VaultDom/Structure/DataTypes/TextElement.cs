using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class TextElement: TextElementBase {
		public static readonly XName ElementName = "Text";

		public TextElement(): base(ElementName) { }

		public TextElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeText;
	}
}
