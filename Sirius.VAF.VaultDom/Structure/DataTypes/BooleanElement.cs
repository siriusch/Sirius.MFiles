using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class BooleanElement: DataTypeElementBase {
		public static readonly XName ElementName = "Boolean";

		public BooleanElement(): base(ElementName) { }

		public BooleanElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeBoolean;
	}
}
