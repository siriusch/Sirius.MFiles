using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public class MultiSelectLookupElement: LookupElementBase {
		public static readonly XName ElementName = "MultiSelectLookup";

		public MultiSelectLookupElement(): base(ElementName) { }

		public MultiSelectLookupElement(XElement node): base(node) { }

		public override MFDataType Type => MFDataType.MFDatatypeMultiSelectLookup;
	}
}
