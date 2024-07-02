using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class DefaultPropertyElement: XElement {
		public static readonly XName ElementName = "defaultproperty";
		private static readonly XName AllowRetainPreviousUserSpecifiedValueName = "allowRetainPreviousUserSpecifiedValue";
		private static readonly XName PdidName = "pdid";
		private static readonly XName TypeName = "type";
		private static readonly XName ValueElementName = "value";

		public DefaultPropertyElement(): base(ElementName) { }

		public DefaultPropertyElement(XElement node): base(node) { }

		public bool AllowRetainPreviousUserSpecifiedValue {
			get => ElementAttribute<bool>.Get(this, AllowRetainPreviousUserSpecifiedValueName);
			set => ElementAttribute<bool>.Set(this, AllowRetainPreviousUserSpecifiedValueName, value);
		}

		public int Pdid {
			get => ElementAttribute<int>.Get(this, PdidName);
			set => ElementAttribute<int>.Set(this, PdidName, value);
		}

		public DefaultPropertyType Type {
			get => ElementAttribute<DefaultPropertyType>.Get(this, TypeName);
			set => ElementAttribute<DefaultPropertyType>.Set(this, TypeName, value);
		}

		public new ValueElement Value {
			get => (ValueElement)Element(ValueElementName);
			set => this.SetElement(ValueElementName, value);
		}
	}
}
