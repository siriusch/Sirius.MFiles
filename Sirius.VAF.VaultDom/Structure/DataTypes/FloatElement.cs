using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class FloatElement: DataTypeElementBase {
		public static readonly XName ElementName = "Float";

		public FloatElement(): base(ElementName) { }

		public FloatElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeFloating;
	}
}
