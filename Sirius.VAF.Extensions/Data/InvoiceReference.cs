using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sirius.VAF.Data {
	public class InvoiceReference {
		private static readonly Regex RxScor = new(@"^RF[0-9]{2}[0-9A-Za-z]{1,21}$", RegexOptions.ExplicitCapture);

		public static bool IsQrReference(string refNr) {
			return IsQrReferenceInternal(Checksum.StripWhitespace(refNr));
		}

		private static bool IsQrReferenceInternal(string shortRefNr) {
			return !string.IsNullOrEmpty(shortRefNr) && !shortRefNr.StartsWith("RF", StringComparison.InvariantCulture);
		}

		public static bool CheckAndNormalizeScor(string refNr, out string formattedRefNr, out string message) {
			formattedRefNr = refNr;
			return CheckAndNormalizeScorInternal(Checksum.StripWhitespace(refNr), ref formattedRefNr, out message);
		}

		private static bool CheckAndNormalizeScorInternal(string shortRefNr, ref string formattedRefNr, out string message) {
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
			return CheckAndNormalizeQrrInternal(Checksum.StripWhitespace(refNr).PadLeft(27, '0'), ref formattedRefNr, out message);
		}

		private static bool CheckAndNormalizeQrrInternal(string shortRefNr, ref string formattedRefNr, out string message) {
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

		public InvoiceReference(string iban) {
			Iban = iban;
		}

		public string Iban {
			get;
		}

		public bool CheckAndNormalize(string refNr, out string formattedRefNr, out string message) {
			formattedRefNr = refNr;
			var shortRefNr = Checksum.StripWhitespace(refNr);
			var isQrReference = IsQrReferenceInternal(shortRefNr);
			var isQrIban = Data.Iban.IsQrIban(Iban);
			if (isQrReference) {
				if (!isQrIban) {
					message = Strings.InvoiceReferenceQrrRequiresQrIban;
					return false;
				}
				return CheckAndNormalizeQrrInternal(shortRefNr, ref formattedRefNr, out message);
			}
			if (isQrIban) {
				message = Strings.InvoiceReferenceQrIbanRequiresQrr;
				return false;
			}
			if (string.IsNullOrEmpty(shortRefNr)) {
				message = default;
				return true;
			}
			return CheckAndNormalizeScorInternal(shortRefNr, ref formattedRefNr, out message);
		}
	}
}
