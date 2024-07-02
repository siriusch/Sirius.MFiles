using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Content {
	public class ContentElement: XElement {
		public static readonly XName ElementName = "content";
		private static readonly XName CapabilitiesElementName = "capabilities";

		public ContentElement(): base(ElementName) { }

		public ContentElement(XElement node): base(node) { }

		public CapabilitiesElement Capabilities {
			get => (CapabilitiesElement)Element(CapabilitiesElementName);
			set => this.SetElement(CapabilitiesElementName, value);
		}

		public IEnumerable<VlItemElement> VlItems => Elements(VlItemElement.ElementName).Cast<VlItemElement>();
	}
}
