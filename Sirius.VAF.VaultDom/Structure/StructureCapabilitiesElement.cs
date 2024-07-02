using System;
using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class StructureCapabilitiesElement: FlagsElementBase<CapabilitiesFlags> {
		public static readonly XName ElementName = "capabilities";

		public StructureCapabilitiesElement(): base(ElementName) { }

		public StructureCapabilitiesElement(XElement node): base(node) { }
	}
}
