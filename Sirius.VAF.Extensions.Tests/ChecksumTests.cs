using System;

using Xunit;
using Xunit.Abstractions;

using JetBrains.Annotations;

namespace Sirius.VAF {
	[TestSubject(typeof(Checksum))]
	public partial class ChecksumTests {
		protected ITestOutputHelper Output {
			get;
		}

		public ChecksumTests(ITestOutputHelper output) {
			Output = output;
		}

		[Theory]
		[InlineData("000000000000000", 0)]
		[InlineData("11111111111111111111111111", 0)]
		[InlineData("123456789012345", 6)]
		[InlineData("12345678901234578901234567", 6)]
		public void ComputeMod10Rec_ValidInput(string input, int expected) {
			var result = Checksum.ComputeMod10Rec(input);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("0000000000000000", true)]
		[InlineData("111111111111111111111111110", true)]
		[InlineData("1234567890123456", true)]
		[InlineData("123456789012345789012345676", true)]
		[InlineData("123456789012345789012345678", false)]
		public void CheckMod10Rec_ValidInput(string input, bool expected) {
			var result = Checksum.CheckMod10Rec(input);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("123456789A")]
		[InlineData("098765432!")]
		public void Mod10Rec_InvalidInput(string input) {
			Assert.Throws<ArgumentException>(() => Checksum.ComputeMod10Rec(input));
		}

		[Theory]
		[InlineData("12345678", 8)]
		[InlineData("98765432", 6)]
		public void ComputeMod11_ValidInput(string value, int expected) {
			var result = Checksum.ComputeMod11(value);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("123456788", true)]
		[InlineData("987654326", true)]
		[InlineData("987654321", false)]
		public void CheckMod11_ValidInput(string value, bool expected) {
			var result = Checksum.CheckMod11(value);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("123456789A")]
		[InlineData("098765432!")]
		public void Mod11_InvalidInput(string input) {
			Assert.Throws<ArgumentException>(() => Checksum.ComputeMod10Rec(input));
		}

		[Theory]
		[InlineData("CH", "002300A1023502601", 10)]
		[InlineData("RF", "539007547034", 18)]
		[InlineData("RF", "000000000539007547034", 18)]
		public void ComputeMod97_ValidInput(string prefix, string value, int expectedResult) {
			var result = Checksum.ComputeMod97(prefix, value);
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData("CH10002300A1023502601", true)]
		[InlineData("RF18539007547034", true)]
		[InlineData("RF18000000000539007547034", true)]
		[InlineData("RF18100000000539007547034", false)]
		public void CheckMod97_ValidInput(string value, bool expectedResult) {
			var result = Checksum.CheckMod97(value);
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData("123456789012", 8)]
		[InlineData("112233445566", 6)]
		public void ComputeEan13_ValidInput(string value, int expected) {
			var result = Checksum.ComputeEan13(value);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("1234567890128", true)]
		[InlineData("1234567890127", false)]
		public void CheckEan13_ValidInput(string value, bool expected) {
			var result = Checksum.CheckEan13(value);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("1234567890", 3, false, "123 456 789 0")]
		[InlineData("1234567890", 3, true, "1 234 567 890")]
		[InlineData("1234567890", 5, false, "12345 67890")]
		[InlineData("1234567890", 5, true, "12345 67890")]
		public void AddWhitespaceTest(string nr, int groupSize, bool rightToLeft, string expected) {
			var result = Checksum.AddWhitespace(nr, groupSize, rightToLeft);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("Hello World", "HelloWorld")]
		[InlineData("   Leading whitespace", "Leadingwhitespace")]
		[InlineData("Trailing whitespace   ", "Trailingwhitespace")]
		[InlineData("  Multiple   Spaces  ", "MultipleSpaces")]
		[InlineData("", "")]
		[InlineData(null, null)]
		public void StripWhitespace_ShouldRemoveAllWhitespace(string input, string expected) {
			// Act
			var result = Checksum.StripWhitespace(input);
			// Assert
			Assert.Equal(expected, result);
			Output.WriteLine($"Input: {input}, Expected: {expected}, Result: {result}");
		}
	}
}
