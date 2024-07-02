using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public abstract class CondsElementBase: XElement {
		protected CondsElementBase(XName name): base(name) { }

		protected CondsElementBase(XElement node): base(node) { }

		public CondsFlagsElement Flags {
			get => (CondsFlagsElement)Element(CondsFlagsElement.ElementName);
			set => this.SetElement(CondsFlagsElement.ElementName, value);
		}

		public CriteriaElement Criteria {
			get => (CriteriaElement)Element(CriteriaElement.ElementName);
			set => this.SetElement(CriteriaElement.ElementName, value);
		}

		public ScriptElement Script {
			get => (ScriptElement)Element(ScriptElement.ElementName);
			set => this.SetElement(ScriptElement.ElementName, value);
		}
	}
}
