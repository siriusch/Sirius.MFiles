using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Archive {
	public class StatisticsElement: XElement {
		public static readonly XName ElementName = "statistics";

		public StatisticsElement(): base(ElementName) { }

		public StatisticsElement(XElement other): base(other) { }

		public int ObjectCount {
			get => ElementAttribute<int>.Get(this, "objectcount");
			set => ElementAttribute<int>.Set(this, "objectcount", value);
		}

		public int VlItemCount {
			get => ElementAttribute<int>.Get(this, "vlitemcount");
			set => ElementAttribute<int>.Set(this, "vlitemcount", value);
		}
	}
}
