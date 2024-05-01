using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class PropertyEnvironmentExtensions {
		public delegate bool CheckAndNormalize(string value, out string formattedValue, out string message);

		public static bool ValidateAndNormalize(this PropertyEnvironment env, CheckAndNormalize checkAndNormalize, out string message) {
			if (!env.PropertyValue.TypedValue.IsNULL() && !env.PropertyValue.TypedValue.IsUninitialized()) {
				var value = (string)env.PropertyValue.TypedValue.Value;
				if (!checkAndNormalize(value, out var formattedValue, out message)) {
					return false;
				}
				if (formattedValue != value) {
					env.ObjVerEx.SaveProperty(env.PropertyDefinition.ID, MFDataType.MFDatatypeText, formattedValue);
				}
			}
			message = "";
			return true;
		}
	}
}
