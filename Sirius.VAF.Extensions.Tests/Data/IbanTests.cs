using System.Diagnostics;

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
