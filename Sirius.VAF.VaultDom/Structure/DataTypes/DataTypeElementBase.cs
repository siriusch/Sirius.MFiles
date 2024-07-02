using System;
using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public abstract class DataTypeElementBase: XElement {
		protected DataTypeElementBase(XName name): base(name) { }

		protected DataTypeElementBase(XElement node): base(node) { }

		public abstract MFDataType Type {
			get;
		}
	}
}
