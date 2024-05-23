using System.Text;

using MFilesAPI;

namespace Sirius.VAF {
	public static class StringBuilderExtensions {
		public static TypedValue ToTextTypedValue(this StringBuilder that) {
			var result = new TypedValue();
			result.SetValue(MFDataType.MFDatatypeText, that.ToString());
			return result;
		}

		public static TypedValue ToMultiLineTextTypedValue(this StringBuilder that) {
			var result = new TypedValue();
			result.SetValue(MFDataType.MFDatatypeMultiLineText, that.ToString());
			return result;
		}
	}
}
