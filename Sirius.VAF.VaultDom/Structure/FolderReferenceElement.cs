using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class FolderReferenceElement: XElement {
		public static readonly XName ElementName = "folder";
		private static readonly XName PosName = "pos";
		private static readonly XName TypeName = "type";
		private static readonly XName ViewIdName = "viewid";

		public FolderReferenceElement(): base(ElementName) { }

		public FolderReferenceElement(XElement node): base(node) { }

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public string Type {
			get => ElementAttribute<string>.Get(this, TypeName);
			set => ElementAttribute<string>.Set(this, TypeName, value);
		}

		public int ViewId {
			get => ElementAttribute<int>.Get(this, ViewIdName);
			set => ElementAttribute<int>.Set(this, ViewIdName, value);
		}
	}
}
