using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class CriteriaElement: XElement {
		public static readonly XName ElementName = "criteria";
		private static readonly XName ActiveName = "active";

		public CriteriaElement(): base(ElementName) { }

		public CriteriaElement(XElement node): base(node) { }

		public bool Active {
			get => ElementAttribute<bool>.Get(this, ActiveName);
			set => ElementAttribute<bool>.Set(this, ActiveName, value);
		}

		public SearchCondElement SearchCond {
			get => (SearchCondElement)Element(SearchCondElement.ElementName);
			set => this.SetElement(SearchCondElement.ElementName, value);
		}
	}
}
