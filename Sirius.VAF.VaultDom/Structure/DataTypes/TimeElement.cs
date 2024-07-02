using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class TimeElement: DataTypeElementBase {
		public static readonly XName ElementName = "Time";

		public TimeElement(): base(ElementName) { }

		public TimeElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeTime;
	}
}
