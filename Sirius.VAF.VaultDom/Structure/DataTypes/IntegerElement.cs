using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class IntegerElement: DataTypeElementBase {
		public static readonly XName ElementName = "Integer";

		public IntegerElement(): base(ElementName) { }

		public IntegerElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeInteger;
	}
}
