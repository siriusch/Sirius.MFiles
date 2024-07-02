using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Archive;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Content {
	public class OriginalIdentityElement: XElement {
		public static readonly XName ElementName = "originalidentity";
		private static readonly XName IdName = "id";
		private static readonly XName OtidName = "otid";
		private static readonly XName VaultGuidName = "vaultguid";

		public OriginalIdentityElement(): base(ElementName) { }

		public OriginalIdentityElement(XElement node): base(node) { }

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public int Otid {
			get => ElementAttribute<int>.Get(this, OtidName);
			set => ElementAttribute<int>.Set(this, OtidName, value);
		}

		public Guid VaultGuid {
			get => ElementAttribute<Guid>.Get(this, VaultGuidName);
			set => ElementAttribute<Guid>.Set(this, VaultGuidName, value, ArchiveDocument.StringifyGuid);
		}
	}
}
