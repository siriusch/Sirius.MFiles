using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sirius.VAF.Data {
	public class Iban {
		public static bool TryGetQrIid(string iban, out int iid) {
			if (!string.IsNullOrEmpty(iban)) {
				var shortIban = Checksum.StripWhitespace(iban);
				var match = Regex.Match(shortIban, @"^(CH|LI)[0-9]{2}(?<qriid>3[01][0-9]{3})[0-9A-Z]{12}$", RegexOptions.CultureInvariant);
				if (match.Success) {
					iid = int.Parse(match.Groups["qriid"].Value, NumberStyles.None, CultureInfo.InvariantCulture);
					return true;
				}
			}
			iid = default;
			return false;
		}

		public static bool IsQrIban(string iban) {
			return TryGetQrIid(iban, out _);
		}

		public static bool CheckAndNormalize(string iban, out string formattedIban, out string message) {
			formattedIban = iban;
			var shortIban = Checksum.StripWhitespace(iban);
			if (!Regex.IsMatch(shortIban, @"^(?!RF)((?!CH|LI)[A-Z]{2}[0-9]{2}[A-Z0-9]{11,27}|(CH|LI)[0-9]{7}[0-9A-Z]{12})$", RegexOptions.CultureInvariant)) {
				message = Strings.IbanInvalidFormat;
				return false;
			}
			if (!Checksum.CheckMod97(shortIban)) {
				message = Strings.IbanInvalidChecksum;
				return false;
			}
			formattedIban = Checksum.AddWhitespace(shortIban, 4);
			message = "";
			return true;
		}
	}
}
