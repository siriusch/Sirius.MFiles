using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.Data {
	[TestSubject(typeof(InvoiceReference))]
	public class InvoiceReferenceTests {
		protected ITestOutputHelper Output {
			get;
		}

		public InvoiceReferenceTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("INVALID", null, false)]
		[InlineData("RF18539007547034", "RF18 5390 0754 7034", true)]
		[InlineData("000000000000000000000000000", "00 00000 00000 00000 00000 00000", true)]
		public void CheckAndNormalizeTest(string input, string expectedFormattedRefNr, bool expected) {
			var result =InvoiceReference.CheckAndNormalize(input, out var formattedRefNr, out var message);
			if (result) {
				Output.WriteLine($"Formatted: {formattedRefNr}");
				Assert.Equal(expectedFormattedRefNr, formattedRefNr);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("000000000000000000000000000", null, false)]
		[InlineData("RF18539007547034", "RF18 5390 0754 7034", true)]
		[InlineData("RF18 5390 0754 7034", "RF18 5390 0754 7034", true)]
		public void CheckAndNormalizeScorTest(string input, string expectedFormattedRefNr, bool expected) {
			var result =InvoiceReference.CheckAndNormalizeScor(input, out var formattedRefNr, out var message);
			if (result) {
				Output.WriteLine($"Formatted SCOR: {formattedRefNr}");
				Assert.Equal(expectedFormattedRefNr, formattedRefNr);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("RF18539007547034", null, false)]
		[InlineData("000000000000000000000000000", "00 00000 00000 00000 00000 00000", true)]
		[InlineData("00 00000 00000 00000 00000 00000", "00 00000 00000 00000 00000 00000", true)]
		public void CheckAndNormalizeQrrTest(string input, string expectedFormattedRefNr, bool expected) {
			var result =InvoiceReference.CheckAndNormalizeQrr(input, out var formattedRefNr, out var message);
			if (result) {
				Output.WriteLine($"Formatted QRR: {formattedRefNr}");
				Assert.Equal(expectedFormattedRefNr, formattedRefNr);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}
	}
}
