using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class LookupElement: LookupElementBase {
		public static readonly XName ElementName = "Lookup";

		public LookupElement(): base(ElementName) { }

		public LookupElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeLookup;
	}
}
