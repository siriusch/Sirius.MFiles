using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Archive {
	public class ArchiveFlagsElement: XElement, ICollection<string> {
		public static readonly XName ElementName = "flags";

		public ArchiveFlagsElement(): base(ElementName) { }

		public ArchiveFlagsElement(XElement other): base(other) { }

		public IEnumerator<string> GetEnumerator() {
			return Elements().Select(e => e.Name.LocalName).GetEnumerator();
		}

		public bool Add(string item) {
			if (Contains(item)) {
				return false;
			}
			Add(new XElement(item));
			return true;
		}

		void ICollection<string>.Add(string item) {
			Add(item);
		}

		public void Clear() {
			RemoveNodes();
		}

		public bool Contains(string item) {
			return Element(item) != null;
		}

		void ICollection<string>.CopyTo(string[] array, int arrayIndex) {
			foreach (var flag in this) {
				array[arrayIndex++] = flag;
			}
		}

		public bool Remove(string item) {
			var elem = Element(item);
			if (elem == null) {
				return false;
			}
			elem.Remove();
			return true;
		}

		int ICollection<string>.Count => Elements().Count();

		bool ICollection<string>.IsReadOnly => false;

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
