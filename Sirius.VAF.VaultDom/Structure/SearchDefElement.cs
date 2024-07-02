using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class SearchDefElement: XElement {
		public static readonly XName ElementName = "searchdef";
		internal static readonly XName SearchCondsName = "searchconds";
		internal static readonly XName LevelsName = "levels";

		public SearchDefElement(): base(ElementName) { }

		public SearchDefElement(XElement node): base(node) { }

		public SearchFlagsElement Flags {
			get => (SearchFlagsElement)Element(SearchFlagsElement.ElementName);
			set => this.SetElement(SearchFlagsElement.ElementName, value);
		}

		public CollectionElement<SearchCondElement> SearchConds {
			get => (CollectionElement<SearchCondElement>)Element(SearchCondsName);
			set => this.SetElement(SearchCondsName, value);
		}

		public CollectionElement<LevelElement> Levels {
			get => (CollectionElement<LevelElement>)Element(LevelsName);
			set => this.SetElement(LevelsName, value);
		}
	}
}
