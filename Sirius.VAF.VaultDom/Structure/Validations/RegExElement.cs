using System;
using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure.Validations {
	public class RegExElement: ActivableElementBase<ValidationType> {
		internal static readonly XName ElementName = "RegEx";

		public RegExElement(): base(ElementName) { }

		public RegExElement(XElement node): base(node) { }

		public override ValidationType Type => ValidationType.RegEx;
	}
}
