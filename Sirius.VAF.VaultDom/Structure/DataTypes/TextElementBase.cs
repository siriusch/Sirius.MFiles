using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public abstract class TextElementBase: DataTypeElementBase {
		private static readonly XName ContentTypeName = "contenttype";

		protected TextElementBase(XName name): base(name) { }

		protected TextElementBase(XElement node): base(node) { }

		public TextContentType ContentType {
			get => ElementAttribute<TextContentType>.GetOrDefault(this, ContentTypeName);
			set => ElementAttribute<TextContentType>.SetOrRemove(this, ContentTypeName, value);
		}
	}
}
