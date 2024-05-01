using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sirius.VAF.Data {
	public class InvoiceReference {
		private static readonly Regex RxScor = new(@"^RF[0-9]{2}[0-9A-Za-z]{1,21}$", RegexOptions.ExplicitCapture);

		public static bool IsValid(string value, string checksum) {
			return !string.IsNullOrEmpty(checksum) && Checksum.ComputeMod10Rec(value) == int.Parse(checksum, NumberStyles.Integer, CultureInfo.InvariantCulture);
		}

		public static bool CheckAndNormalize(string refNr, out string formattedRefNr, out string message) {
			return refNr.StartsWith("RF", StringComparison.InvariantCulture)
					? CheckAndNormalizeScor(refNr, out formattedRefNr, out message)
					: CheckAndNormalizeQrr(refNr, out formattedRefNr, out message);
		}

		public static bool CheckAndNormalizeScor(string refNr, out string formattedRefNr, out string message) {
			formattedRefNr = refNr;
			var shortRefNr = Checksum.StripWhitespace(refNr);
			if (!RxScor.IsMatch(shortRefNr)) {
				message = Strings.InvoiceReferenceScorInvalidFormat;
				return false;
			}
			if (!Checksum.CheckMod97(shortRefNr)) {
				message = Strings.InvoiceReferenceScorInvalidChecksum;
				return false;
			}
			formattedRefNr = Checksum.AddWhitespace(shortRefNr, 4);
			message = "";
			return true;
		}

		public static bool CheckAndNormalizeQrr(string refNr, out string formattedRefNr, out string message) {
			formattedRefNr = refNr;
			var shortRefNr = Checksum.StripWhitespace(refNr).PadLeft(27, '0');
			if (!Regex.IsMatch(shortRefNr, "^[0-9]{27}$")) {
				message = Strings.InvoiceReferenceQrrInvalidFormat;
				return false;
			}
			if (!Checksum.CheckMod10Rec(shortRefNr)) {
				message = Strings.InvoiceReferenceQrrInvalidChecksum;
				return false;
			}
			formattedRefNr = Checksum.AddWhitespace(shortRefNr, 5, true);
			message = "";
			return true;
		}
	}
}
