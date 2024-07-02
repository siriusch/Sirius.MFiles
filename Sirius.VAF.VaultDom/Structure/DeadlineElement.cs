using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class DeadlineElement: XElement {
		public static readonly XName ElementName = "deadline";
		private static readonly XName DaysName = "days";
		private static readonly XName SpecifiedName = "specified";

		public DeadlineElement(): base(ElementName) { }

		public DeadlineElement(XElement node): base(node) { }

		public int Days {
			get => ElementAttribute<int>.Get(this, DaysName);
			set => ElementAttribute<int>.Set(this, DaysName, value);
		}

		public bool Specified {
			get => ElementAttribute<bool>.Get(this, SpecifiedName);
			set => ElementAttribute<bool>.Set(this, SpecifiedName, value);
		}
	}
}
