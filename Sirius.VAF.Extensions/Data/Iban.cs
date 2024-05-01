using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sirius.VAF.Data {
	public class Iban {
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
