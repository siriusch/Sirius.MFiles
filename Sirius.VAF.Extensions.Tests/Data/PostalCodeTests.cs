using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.Data {
	[TestSubject(typeof(PostalCode))]
	public class PostalCodeTests {
		protected ITestOutputHelper Output {
			get;
		}

		public PostalCodeTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("CA", "N0M1C0", "N0M 1C0", true)]
		[InlineData("CH", "2762", "2762", true)]
		[InlineData("DE", "12345", "12345", true)]
		[InlineData("CH", "12345", null, false)]
		public void CheckAndNormalizeTest(string country, string postalCode, string expectedFormattedPostalCode, bool expected) {
			var result = new PostalCode(country).CheckAndNormalize(postalCode, out var formattedPostalCode, out var message);
			if (result) {
				Output.WriteLine($"Formatted postal code: {formattedPostalCode}");
				Assert.Equal(expectedFormattedPostalCode, formattedPostalCode);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}
	}
}
