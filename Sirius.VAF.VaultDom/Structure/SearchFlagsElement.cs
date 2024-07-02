using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class SearchFlagsElement: FlagsElementBase<MFSearchFlags> {
		public static readonly XName ElementName = "flags";

		public SearchFlagsElement(): base(ElementName) { }

		public SearchFlagsElement(XElement node): base(node) { }

		protected override string Prefix => "MFSearchFlag";
	}
}
