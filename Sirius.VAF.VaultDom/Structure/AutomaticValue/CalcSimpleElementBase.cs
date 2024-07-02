using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.AutomaticValue {
	public abstract class CalcSimpleElementBase: ActivableElementBase<AutomaticValueType> {
		private static readonly XName ValueName = "value";

		protected CalcSimpleElementBase(XName name): base(name) { }

		protected CalcSimpleElementBase(XElement node): base(node) { }

		public new string Value {
			get => ElementAttribute<string>.Get(this, ValueName);
			set => ElementAttribute<string>.Set(this, ValueName, value);
		}
	}
}
