using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class TimestampElement: DataTypeElementBase {
		public static readonly XName ElementName = "Timestamp";

		public TimestampElement(): base(ElementName) { }

		public TimestampElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeTimestamp;
	}
}
