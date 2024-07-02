using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.Data {
	[TestSubject(typeof(PhoneNumber))]
	public class PhoneNumberTests {
		protected ITestOutputHelper Output {
			get;
		}

		public PhoneNumberTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("0033123456789", "+33 1 23 45 67 89", true)]
		[InlineData("+41613757575", "+41 61 375 75 75", true)]
		[InlineData("0613757575", "+41 61 375 75 75", true)]
		[InlineData("123", null, false)]
		public void CheckAndNormalizeTest(string phoneNumber, string expectedFormattedPhoneNumber, bool expected) {
			var result = new PhoneNumber("CH").CheckAndNormalize(phoneNumber, out var formattedPhoneNumber, out var message);
			if (result) {
				Output.WriteLine($"Formatted phone number: {formattedPhoneNumber}");
				Assert.Equal(expectedFormattedPhoneNumber, formattedPhoneNumber);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}
	}
}
