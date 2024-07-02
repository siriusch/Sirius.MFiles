using System;
using System.Collections;
using System.Collections.Generic;

namespace Sirius.XML {
	public class Mapping<T>: IEnumerable<KeyValuePair<string, T>> {
		private readonly Dictionary<T, string> stringify;
		private readonly Dictionary<string, T> parse;

		public Mapping(): this(null, null) { }

		public Mapping(IEqualityComparer<T> valueComparer): this(valueComparer, null) { }

		public Mapping(IEqualityComparer<string> stringComparer): this(null, stringComparer) { }

		public Mapping(IEqualityComparer<T> valueComparer, IEqualityComparer<string> stringComparer) {
			parse = new Dictionary<string, T>(stringComparer ?? StringComparer.Ordinal);
			stringify = new Dictionary<T, string>(valueComparer ?? EqualityComparer<T>.Default);
		}

		public void Add(string key, T value) {
			stringify.Add(value, key);
			try {
				parse.Add(key, value);
			} catch {
				stringify.Remove(value);
				throw;
			}
		}

		public T Parse(string value) {
			return parse[value];
		}

		public string Stringify(T value) {
			return stringify[value];
		}

		public IEnumerator<KeyValuePair<string, T>> GetEnumerator() {
			return parse.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
