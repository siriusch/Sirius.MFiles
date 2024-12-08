using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.Data {
	[TestSubject(typeof(Iban))]
	public class IbanTests {
		protected ITestOutputHelper Output {
			get;
		}

		public IbanTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("CH45 0023 0230 9999 9999 A", false)]
		[InlineData("CH450023023099999999A", false)]
		[InlineData("GB80 3000 0A02 3502 6012 3", false)]
		[InlineData("GB8030000A02350260123", false)]
		[InlineData("CH57 3000 0123 0008 8901 2", true)]
		[InlineData("CH5730000123000889012", true)]
		[InlineData("CH44 3199 9123 0008 8901 2", true)]
		[InlineData("CH4431999123000889012", true)]
		public void IsQrIbanTest(string iban, bool expected) {
			var result = Iban.IsQrIban(iban);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("CH45 0023 0230 9999 9999 A", "CH45 0023 0230 9999 9999 A", true)] // valid IBAN
		[InlineData("CH450023023099999999A", "CH45 0023 0230 9999 9999 A", true)] // valid IBAN
		[InlineData("CH09 WEST 1234 5698 7654 3", null, false)] // valid IBAN checksum, but not valid for CH
		[InlineData("CH09WEST1234569876543", null, false)] // valid IBAN checksum, but not valid for CH
		[InlineData("LI22 WEST 1234 5698 7654 3", null, false)] // valid IBAN checksum, but not valid for LI
		[InlineData("LI22WEST1234569876543", null, false)] // valid IBAN checksum, but not valid for LI
		[InlineData("GB82 WEST 1234 5698 7654 32", "GB82 WEST 1234 5698 7654 32", true)] // valid IBAN
		[InlineData("GB82WEST12345698765432", "GB82 WEST 1234 5698 7654 32", true)] // valid IBAN
		[InlineData("GB82 TEST 1234 5698 7654 32", null, false)] // invalid IBAN
		[InlineData("GB82TEST12345698765432", null, false)] // invalid IBAN
		public void CheckAndNormalizeTest(string iban, string expectedFormattedIban, bool expected) {
			var result = Iban.CheckAndNormalize(iban, out var formattedIban, out var message);
			if (result) {
				Output.WriteLine($"Formatted IBAN: {formattedIban}");
				Assert.Equal(expectedFormattedIban, formattedIban);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}
	}
}
