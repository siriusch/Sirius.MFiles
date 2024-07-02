using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class DateElement: DataTypeElementBase {
		public static readonly XName ElementName = "Date";

		public DateElement(): base(ElementName) { }

		public DateElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeDate;
	}
}
