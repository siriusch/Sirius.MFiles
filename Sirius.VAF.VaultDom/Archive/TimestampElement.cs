using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Archive {
	public class TimestampElement: XElement {
		public static readonly XName ElementName = "timestamp";
		private static readonly XName ValueName = "value";

		public TimestampElement(): base(ElementName) { }

		public TimestampElement(XElement other): base(other) { }

		public new DateTime Value {
			get => ElementAttribute<DateTime>.Get(this, ValueName);
			set => ElementAttribute<DateTime>.Set(this, ValueName, value);
		}
	}
}
