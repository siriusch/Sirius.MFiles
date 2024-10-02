using System.Collections.Generic;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;

namespace Sirius.VAF {
	public static class MFIdentifierExtensions {
		public static EnumerableExtensions.TryFunc<ObjVerEx, KeyValuePair<TKey, ObjVerEx>> TryGetPropertyValue<TKey>(this MFIdentifier that) {
			return (ObjVerEx value, out KeyValuePair<TKey, ObjVerEx> result) => {
				if (value.TryGetPropertyWithValue(that, out var val)) {
					result = new KeyValuePair<TKey, ObjVerEx>((TKey)val.TypedValue.Value, value);
					return true;
				}
				result = new KeyValuePair<TKey, ObjVerEx>(default, value);
				return false;
			};
		}
	}
}