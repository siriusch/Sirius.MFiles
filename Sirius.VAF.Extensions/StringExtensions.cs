using System;

using MFilesAPI;

namespace Sirius.VAF {
	public static class StringExtensions {
		public static string NullIfEmpty(this string that) {
			return string.IsNullOrEmpty(that) ? null : that;
		}

		public static TypedValue ToTextTypedValue(this string that) {
			var result = new TypedValue();
			result.SetValueEx(MFDataType.MFDatatypeText, that);
			return result;
		}

		public static TypedValue ToMultiLineTextTypedValue(this string that) {
			var result = new TypedValue();
			result.SetValueEx(MFDataType.MFDatatypeMultiLineText, that);
			return result;
		}
	}
}
