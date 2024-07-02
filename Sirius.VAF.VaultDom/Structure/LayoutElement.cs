using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class LayoutElement: XElement, IFileReference {
		public static readonly XName ElementName = "layout";
		private static readonly XName PathFromBaseName = "pathfrombase";
		private static readonly XName ContentTypeName = "contenttype";

		public LayoutElement(): base(ElementName) { }

		public LayoutElement(XElement node): base(node) { }

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
