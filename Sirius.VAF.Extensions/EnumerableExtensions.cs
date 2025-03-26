using System;
using System.Collections.Generic;
using System.Linq;

namespace Sirius.VAF {
	public static class EnumerableExtensions {
		public delegate bool TryFunc<in TIn, TOut>(TIn value, out TOut result);

		public delegate bool TryFunc<in TIn, in T, TOut>(TIn value, T arg, out TOut result);

		public delegate bool TryFunc<in TIn, in T1, in T2, TOut>(TIn value, T1 arg1, T2 arg2, out TOut result);

		public static IEnumerable<TOut> SelectWhere<TIn, TOut>(this IEnumerable<TIn> that, TryFunc<TIn, TOut> selectWhere) {
			if (that == null) {
				throw new ArgumentNullException(nameof(that));
			}
			if (selectWhere == null) {
				throw new ArgumentNullException(nameof(selectWhere));
			}
			return SelectWhereInternal(that, selectWhere);

			static IEnumerable<TOut> SelectWhereInternal(IEnumerable<TIn> that, TryFunc<TIn, TOut> selectWhere) {
				foreach (var value in that) {
					if (selectWhere(value, out var result)) {
						yield return result;
					}
				}
			}
		}

		public static IEnumerable<TOut> SelectWhere<TIn, T, TOut>(this IEnumerable<TIn> that, TryFunc<TIn, T, TOut> selectWhere, T arg) {
			if (that == null) {
				throw new ArgumentNullException(nameof(that));
			}
			if (selectWhere == null) {
				throw new ArgumentNullException(nameof(selectWhere));
			}
			return SelectWhereInternal(that, selectWhere, arg);

			static IEnumerable<TOut> SelectWhereInternal(IEnumerable<TIn> that, TryFunc<TIn, T, TOut> selectWhere, T arg) {
				foreach (var value in that) {
					if (selectWhere(value, arg, out var result)) {
						yield return result;
					}
				}
			}
		}

		public static IEnumerable<TOut> SelectWhere<TIn, T1, T2, TOut>(this IEnumerable<TIn> that, TryFunc<TIn, T1, T2, TOut> selectWhere, T1 arg1, T2 arg2) {
			if (that == null) {
				throw new ArgumentNullException(nameof(that));
			}
			if (selectWhere == null) {
				throw new ArgumentNullException(nameof(selectWhere));
			}
			return SelectWhereInternal(that, selectWhere, arg1, arg2);

			static IEnumerable<TOut> SelectWhereInternal(IEnumerable<TIn> that, TryFunc<TIn, T1, T2, TOut> selectWhere, T1 arg1, T2 arg2) {
				foreach (var value in that) {
					if (selectWhere(value, arg1, arg2, out var result)) {
						yield return result;
					}
				}
			}
		}

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> that, IEqualityComparer<TKey> comparer = null) {
			var result = new Dictionary<TKey, TValue>(comparer);
			result.AddRange(that);
			return result;
		}

		public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> that, IEnumerable<KeyValuePair<TKey, TValue>> values) {
			foreach (var pair in values) {
				that.Add(pair.Key, pair.Value);
			}
		}

		public static bool TrySingle<T>(this IEnumerable<T> that, out T item) {
			using var enumerator = that.GetEnumerator();
			if (enumerator.MoveNext()) {
				item = enumerator.Current;
				if (!enumerator.MoveNext()) {
					return true;
				}
			}
			item = default;
			return false;
		}

		public static bool TrySingle<T>(this IEnumerable<T> that, Func<T, bool> predicate, out T item) {
			return that.Where(predicate).TrySingle(out item);
		}

		public static bool TryFirst<T>(this IEnumerable<T> that, out T item) {
			using var enumerator = that.GetEnumerator();
			if (enumerator.MoveNext()) {
				item = enumerator.Current;
				return true;
			}
			item = default;
			return false;
		}

		public static bool TryFirst<T>(this IEnumerable<T> that, Func<T, bool> predicate, out T item) {
			return that.Where(predicate).TryFirst(out item);
		}

		public static bool TryLast<T>(this IEnumerable<T> that, out T item) {
			if (that is IReadOnlyList<T> list) {
				if (list.Count == 0) {
					item = default;
					return false;
				}
				item = list[list.Count-1];
				return true;
			}
			using var enumerator = that.GetEnumerator();
			if (!enumerator.MoveNext()) {
				item = default;
				return false;
			}
			do {
				item = enumerator.Current;
			} while (enumerator.MoveNext());
			return true;
		}

		public static bool TryLast<T>(this IEnumerable<T> that, Func<T, bool> predicate, out T item) {
			if (that is IReadOnlyList<T> list) {
				T result;
				for (var i = list.Count-1; i >= 0; i--) {
					result = list[i];
					if (predicate(result)) {
						item = result;
						return true;
					}
				}
				item = default;
				return false;
			}
			return that.Where(predicate).TryLast(out item);
		}
	}
}
