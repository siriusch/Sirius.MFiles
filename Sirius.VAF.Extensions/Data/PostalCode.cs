using System;
using System.Configuration;

using MFilesAPI;

namespace Sirius.VAF.Data {
	public class PostalCode {
		public string CountryCode {
			get;
		}

		public PostalCode(string countryCode) {
			this.CountryCode = countryCode;
		}

		public bool CheckAndNormalize(string postalCode, out string formattedPostalCode, out string message) {
			formattedPostalCode = postalCode;
			if (!string.IsNullOrEmpty(CountryCode)) {
				try {
					var country = PostalCodes.CountryFactory.Instance.CreateCountry(CountryCode);
					var parsedPostalCode = PostalCodes.PostalCodeFactory.Instance.CreatePostalCode(country, postalCode);
					formattedPostalCode = parsedPostalCode.ToHumanReadableString();
				} catch (ArgumentException) {
					message = Strings.PostalCodeInvalid(CountryCode);
					return false;
				}
			} 
			message = "";
			return !string.IsNullOrEmpty(postalCode);
		}
	}
}
