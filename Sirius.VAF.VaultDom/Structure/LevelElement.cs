using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class LevelElement: XElement {
		public static readonly XName ElementName = "level";
		private static readonly XName IdName = "id";
		private static readonly XName PosName = "pos";
		private static readonly XName TypeName = "type";

		public LevelElement(): base(ElementName) { }

		public LevelElement(XElement node): base(node) { }

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public IndirectionType Type {
			get => ElementAttribute<IndirectionType>.Get(this, TypeName);
			set => ElementAttribute<IndirectionType>.Set(this, TypeName, value);
		}
	}
}
