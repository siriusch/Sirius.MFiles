using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class LanguageElement: XElement {
		public static readonly XName ElementName = "language";
		private static readonly XName OwnerName = "code";
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";

		public LanguageElement(): base(ElementName) { }

		public LanguageElement(XElement node): base(node) { }

		public string Code {
			get => ElementAttribute<string>.GetOrDefault(this, OwnerName, "");
			set => ElementAttribute<string>.Set(this, OwnerName, value);
		}

		public int Id {
			get => ElementAttribute<int>.GetOrDefault(this, IdName, 0);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public string LanguageName {
			get => ElementAttribute<string>.GetOrDefault(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}
	}
}
