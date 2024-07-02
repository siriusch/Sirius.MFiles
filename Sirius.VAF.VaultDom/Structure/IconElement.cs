using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class IconElement: XElement, IFileReference {
		public static readonly XName ElementName = "icon";
		private static readonly XName BytesName = "bytes";
		private static readonly XName PathFromBaseName = "pathfrombase";
		private static readonly XName ContentTypeName = "contenttype";

		public IconElement(): base(ElementName) { }

		public IconElement(XElement node): base(node) { }

		public int Bytes {
			get => ElementAttribute<int>.Get(this, BytesName);
			set => ElementAttribute<int>.Set(this, BytesName, value);
		}

		public string ContentType {
			get => ElementAttribute<string>.Get(this, ContentTypeName);
			set => ElementAttribute<string>.Set(this, ContentTypeName, value);
		}

		public string PathFromBase {
			get => ElementAttribute<string>.Get(this, PathFromBaseName);
			set => ElementAttribute<string>.Set(this, PathFromBaseName, value);
		}
	}
}
