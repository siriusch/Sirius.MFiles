using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.Data {
	[TestSubject(typeof(Oasi))]
	public class OasiTests {
		protected ITestOutputHelper Output {
			get;
		}

		public OasiTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("1234567890128", "123.4567.8901.28", true)] 
		[InlineData("123.4567.8901.28", "123.4567.8901.28", true)] 
		[InlineData("GB82TEST12345698765432", null, false)] 
		public void CheckAndNormalizeTest(string oasi, string expectedFormattedOasi, bool expected) {
			var result = Oasi.CheckAndNormalize(oasi, out var formattedOasi, out var message);
			if (result) {
				Output.WriteLine($"Formatted OASI number: {formattedOasi}");
				Assert.Equal(expectedFormattedOasi, formattedOasi);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}
	}
}
