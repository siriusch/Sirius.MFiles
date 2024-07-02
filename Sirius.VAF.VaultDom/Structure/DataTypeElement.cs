using System.Linq;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Structure.DataTypes;

namespace Sirius.VAF.VaultDom.Structure {
	public class DataTypeElement: XElement {
		public static readonly XName ElementName = "datatype";

		public DataTypeElement(): base(ElementName) { }

		public DataTypeElement(XElement node): base(node) { }

		public new DataTypeElementBase Value {
			get => (DataTypeElementBase)Elements().SingleOrDefault();
			set {
				if (value?.Parent == this) {
					return;
				}
				RemoveNodes();
				Add(value);
			}
		}
	}
}
