using System;
using System.Text.RegularExpressions;

namespace Sirius.VAF.Data {
	public class Oasi {
		public static bool CheckAndNormalize(string oasi, out string formattedNr, out string message) {
			formattedNr = oasi;
			if (!Regex.IsMatch(oasi, @"^[0-9]{3}\.?[0-9]{4}\.?[0-9]{4}\.?[0-9]{2}$", RegexOptions.CultureInvariant)) {
				message = Strings.OasiInvalidFormat;
				return false;
			}
			var shortOasi = oasi.Replace(".", "");
			if (!Checksum.CheckEan13(shortOasi)) {
				message = Strings.OasiInvalidChecksum;
				return false;
			}
			formattedNr = FormattableString.Invariant($"{shortOasi.Substring(0, 3)}.{shortOasi.Substring(3, 4)}.{shortOasi.Substring(7, 4)}.{shortOasi.Substring(11)}");
			message = "";
			return true;
		}
	}
}
