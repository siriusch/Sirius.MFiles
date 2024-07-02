using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.AutomaticValue {
	public abstract class CalcVBScriptElementBase: ActivableElementBase<AutomaticValueType>, IFileReference {
		private static readonly XName EmptyName = "empty";
		private static readonly XName PathFromBaseName = "pathfrombase";
		private static readonly XName ContentTypeName = "contenttype";

		protected CalcVBScriptElementBase(XName name): base(name) { }

		protected CalcVBScriptElementBase(XElement node): base(node) { }

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
