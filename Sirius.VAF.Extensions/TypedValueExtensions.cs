using System;
using System.Collections.Generic;

using MFilesAPI;

namespace Sirius.VAF {
	public static class TypedValueExtensions {
		public static IEnumerable<int> GetValueAsLookupIDs(this TypedValue that) {
			return that.GetValueAsLookups().GetLookupIDs();
		}

		internal static void SetValueOrNULL(this TypedValue that, MFDataType type, object value) {
			if (value == null) {
				that.SetValueToNULL(type);
			} else {
				that.SetValue(type, value);
			}
		}
	}
}
