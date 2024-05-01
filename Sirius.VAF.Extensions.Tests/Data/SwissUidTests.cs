using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.Data {
	[TestSubject(typeof(SwissUid))]
	public class SwissUidTests {
		protected ITestOutputHelper Output {
			get;
		}

		public SwissUidTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("CHE105449419", "CHE-105.449.419", true)]
		[InlineData("CHE-105.449.419", "CHE-105.449.419", true)]
		[InlineData("GER-123.456.789", null, false)]
		public void CheckAndNormalizeTest(string uid, string expectedFormattedUid, bool expected) {
			var result = SwissUid.CheckAndNormalize(uid, out var formattedUid, out var message);
			if (result) {
				Output.WriteLine($"Formatted company UID: {formattedUid}");
				Assert.Equal(expectedFormattedUid, formattedUid);
			} else {
				Output.WriteLine($"Error message: {message}");
			}
			Assert.Equal(expected, result);
		}
	}
}
