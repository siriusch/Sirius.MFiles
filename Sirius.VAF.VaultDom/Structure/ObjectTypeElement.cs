using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ObjectTypeElement: XElement {
		public static readonly XName ElementName = "objecttype";
		private static readonly XName AllName = "all";
		private static readonly XName NameName = "name";
		private static readonly XName OtIdName = "otid";

		public ObjectTypeElement(): base(ElementName) { }

		public ObjectTypeElement(XElement node): base(node) { }

		public bool All {
			get => ElementAttribute<bool>.Get(this, AllName);
			set => ElementAttribute<bool>.Set(this, AllName, value);
		}

		public new string Name {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}

		public int? OtId {
			get => ElementAttribute<int?>.Get(this, OtIdName);
			set => ElementAttribute<int?>.Set(this, OtIdName, value);
		}
	}
}
