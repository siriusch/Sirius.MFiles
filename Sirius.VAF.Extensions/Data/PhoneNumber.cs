using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoneNumbers;

namespace Sirius.VAF.Data {
	public class PhoneNumber {
		public string DefaultRegion {
			get;
		}

		public PhoneNumber(string defaultRegion) {
			this.DefaultRegion = defaultRegion;
		}

		public bool CheckAndNormalize(string nr, out string formattedNr, out string message) {
			formattedNr = nr;
			PhoneNumbers.PhoneNumber parsedNr;
			var util = PhoneNumberUtil.GetInstance();
			try {
				parsedNr = util.Parse(nr, DefaultRegion);
			} catch (NumberParseException ex) {
				message = Strings.PhoneInvalidFormat(ex.Message);
				return false;
			}
			switch (util.IsPossibleNumberWithReason(parsedNr)) {
			case PhoneNumberUtil.ValidationResult.INVALID_COUNTRY_CODE:
				message = Strings.PhoneInvalidCountryCode;
				return false;
			case PhoneNumberUtil.ValidationResult.INVALID_LENGTH:
				message = Strings.PhoneInvalidLength;
				return false;
			case PhoneNumberUtil.ValidationResult.IS_POSSIBLE_LOCAL_ONLY:
				message = Strings.PhoneLocalNumber;
				return false;
			case PhoneNumberUtil.ValidationResult.TOO_LONG:
				message = Strings.PhoneTooLong;
				return false;
			case PhoneNumberUtil.ValidationResult.TOO_SHORT:
				message = Strings.PhoneTooShort;
				return false;
			}
			if (!util.IsValidNumber(parsedNr)) {
				message = Strings.PhoneInvalidNumber;
				return false;
			}
			message = "";
			formattedNr = util.Format(parsedNr, PhoneNumberFormat.INTERNATIONAL);
			return true;
		}
	}
}
