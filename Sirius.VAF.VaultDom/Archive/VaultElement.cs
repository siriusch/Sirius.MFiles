using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Archive {
	public class VaultElement: XElement {
		public static readonly XName ElementName = "vault";

		public VaultElement(): base(ElementName) { }

		public VaultElement(XElement other): base(other) { }

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, "guid");
			set => ElementAttribute<Guid>.Set(this, "guid", value, ArchiveDocument.StringifyGuid);
		}

		public bool ExtendedMetadataDrivenPermissions {
			get => ElementAttribute<bool>.Get(this, "extendedmetadatadrivenpermissions");
			set => ElementAttribute<bool>.Set(this, "extendedmetadatadrivenpermissions", value);
		}
	}
}
