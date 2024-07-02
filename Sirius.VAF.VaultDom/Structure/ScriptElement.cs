using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ScriptElement: XElement, IFileReference {
		public static readonly XName ElementName = "script";
		private static readonly XName ActiveName = "active";
		private static readonly XName ContentTypeName = "contenttype";
		private static readonly XName EmptyName = "empty";
		private static readonly XName PathFromBaseName = "pathfrombase";

		public ScriptElement(): base(ElementName) { }

		public ScriptElement(XElement node): base(node) { }

		public bool Active {
			get => ElementAttribute<bool>.Get(this, ActiveName);
			set => ElementAttribute<bool>.Set(this, ActiveName, value);
		}

		public string ContentType {
			get => ElementAttribute<string>.Get(this, ContentTypeName);
			set => ElementAttribute<string>.SetOrRemove(this, ContentTypeName, string.IsNullOrEmpty(value) ? null : value);
		}

		public bool Empty {
			get => ElementAttribute<bool>.Get(this, EmptyName);
			set {
				if (ElementAttribute<bool>.Set(this, EmptyName, value) && value) {
					PathFromBase = null;
					ContentType = null;
				}
			}
		}

		public string PathFromBase {
			get => ElementAttribute<string>.Get(this, PathFromBaseName);
			set {
				if (ElementAttribute<string>.SetOrRemove(this, PathFromBaseName, string.IsNullOrEmpty(value) ? null : value)) {
					Empty = string.IsNullOrEmpty(value);
				}
			}
		}
	}
}
