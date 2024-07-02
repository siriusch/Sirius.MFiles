using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class PropertyElement: XElement {
		public static readonly XName ElementName = "property";
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";
		private static readonly XName RequiredName = "required";

		public PropertyElement(): base(ElementName) { }

		public PropertyElement(XElement node): base(node) { }

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public new string Name {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}

		public bool Required {
			get => ElementAttribute<bool>.Get(this, RequiredName);
			set => ElementAttribute<bool>.Set(this, RequiredName, value);
		}
	}
}
