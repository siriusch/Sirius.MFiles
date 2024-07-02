using System;
using System.Xml.Linq;

namespace Sirius.XML {
	public static class Extensions {
		public static XElement GetOrCreateElement(this XContainer container, XName name) {
			var result = container.Element(name);
			if (result == null) {
				container.Add(new XElement(name));
				// When adding, the node may get replaced for typing, so we need to get the last node from the container
				return (XElement)container.LastNode;
			}
			return result;
		}

		public static void SetElement(this XContainer container, XName name, XElement element) {
			if (element != null) {
				if (element.Parent == container) {
					if (element.Name != name) {
						element.Name = name;
					}
					return;
				}
				element.Remove();
				if (element.Name != name) {
					element.Name = name;
				}
			}
			var existing = container.Element(name);
			if (existing == null) {
				if (element != null) {
					container.Add(element);
				}
				return;
			}
			if (element == null) {
				existing.Remove();
				return;
			}
			existing.ReplaceWith(element);
		}
	}
}
