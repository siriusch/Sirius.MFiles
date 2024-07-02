using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class TypedValueElement: XElement {
		public static readonly XName ElementName = "typedvalue";
		private static readonly XName DataTypeName = "datatype";
		private static readonly XName EmptyName = "empty";

		public TypedValueElement(): base(ElementName) { }

		public TypedValueElement(XElement node): base(node) { }

		public TypedValueDataType DataType {
			get => ElementAttribute<TypedValueDataType>.Get(this, DataTypeName);
			set => ElementAttribute<TypedValueDataType>.Set(this, DataTypeName, value);
		}

		public bool Empty {
			get => ElementAttribute<bool>.Get(this, EmptyName);
			set => ElementAttribute<bool>.Set(this, EmptyName, value);
		}
	}
}
