using System;
using System.Text.RegularExpressions;

namespace Sirius.VAF.Data {
	public static class SwissUid {
		public static bool CheckAndNormalize(string uid, out string formattedUid, out string message) {
			if (!Regex.IsMatch(uid, @"^CHE-?[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}$", RegexOptions.CultureInvariant|RegexOptions.IgnoreCase)) {
				message = Strings.UidInvalidFormat;
				formattedUid = null;
				return false;
			}
			var digits = Regex.Replace(uid, "[^0-9]+", "", RegexOptions.CultureInvariant);
			if (!Checksum.CheckMod11(digits)) {
				message = Strings.UidInvalidChecksum;
				formattedUid = null;
				return false;
			}
			message = null;
			formattedUid = $"CHE-{digits.Substring(0, 3)}.{digits.Substring(3, 3)}.{digits.Substring(6, 3)}";
			return true;
		}
	}
}
