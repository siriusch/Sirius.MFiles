using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sirius.XML {
	public class CollectionElement<TElement>: XElement, IEnumerable<TElement> where TElement: XElement {
		public CollectionElement(XName name): base(name) { }

		public CollectionElement(XElement other): base(other) { }

		public IEnumerator<TElement> GetEnumerator() {
			return Elements().OfType<TElement>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
