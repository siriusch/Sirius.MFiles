using System;
using System.Collections.Generic;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;

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
	}
}
