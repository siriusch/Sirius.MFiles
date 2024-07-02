using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class MultiLineTextElement: TextElementBase {
		public static readonly XName ElementName = "MultiLineText";

		public MultiLineTextElement(): base(ElementName) { }

		public MultiLineTextElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeMultiLineText;
	}
}
