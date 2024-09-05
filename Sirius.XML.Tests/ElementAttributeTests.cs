using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.XML {
	[TestSubject(typeof(ElementAttribute<>))]
	public class ElementAttributeTests {
		private static readonly Func<string, string> Passthrough = s => s;

		private XElement element = new("elem",
				new XAttribute("yes", "true"),
				new XAttribute("no", "false"),
				new XAttribute("one", "1"),
				new XAttribute("zero", "0"),
				new XAttribute("empty", ""),
				new XAttribute("anything", "900F21A1-42D9-4B8A-B1A6-0D69B87A16F7"));

		protected ITestOutputHelper Output {
			get;
		}

		public ElementAttributeTests(ITestOutputHelper output) {
			Output = output;
		}

		[Fact]
		public void Get_OK() {
			Assert.Equal("true", ElementAttribute<string>.Get(element, "yes", Passthrough));
		}

		[Fact]
		public void Get_Error() {
			Assert.Throws<InvalidOperationException>(() => ElementAttribute<string>.Get(element, "err", Passthrough));
		}

		[Fact]
		public void GetOrDefault_Value() {
			Assert.Equal("true", ElementAttribute<string>.GetOrDefault(element, "yes", "def", Passthrough));
		}

		[Fact]
		public void GetOrDefault_Default() {
			Assert.Equal("def", ElementAttribute<string>.GetOrDefault(element, "err", "def", Passthrough));
		}

		[Fact]
		public void Set_New() {
			Assert.Null(element.Attribute("new"));
			ElementAttribute<string>.Set(element, "new", "val", Passthrough);
			Assert.Equal("val", element.Attribute("new").Value);
		}

		[Fact]
		public void Set_Existing() {
			var anyting = Guid.NewGuid().ToString().ToUpperInvariant();
			Assert.NotEqual(anyting, element.Attribute("anything").Value);
			ElementAttribute<string>.Set(element, "anything", anyting, Passthrough);
			Assert.Equal(anyting, element.Attribute("anything").Value);
		}

		[Fact]
		public void Set_Error() {
			Assert.Throws<ArgumentNullException>(() => ElementAttribute<string>.Set(element, "anything", null, Passthrough));
		}

		[Fact]
		public void SetOrRemove_New() {
			Assert.Null(element.Attribute("new"));
			ElementAttribute<string>.SetOrRemove(element, "new", "val", Passthrough);
			Assert.Equal("val", element.Attribute("new").Value);
		}

		[Fact]
		public void SetOrRemove_Existing() {
			var anyting = Guid.NewGuid().ToString().ToUpperInvariant();
			Assert.NotEqual(anyting, element.Attribute("anything").Value);
			ElementAttribute<string>.SetOrRemove(element, "anything", anyting, Passthrough);
			Assert.Equal(anyting, element.Attribute("anything").Value);
		}

		[Fact]
		public void SetOrRemove_Remove() {
			element.Add(new XAttribute("temp", "remove-me"));
			ElementAttribute<string>.SetOrRemove(element, "temp", null, Passthrough);
			Assert.Null(element.Attribute("temp"));
		}

		[Fact]
		public void SetOrRemove_Nothing() {
			Assert.Null(element.Attribute("temp"));
			ElementAttribute<string>.SetOrRemove(element, "temp", null, Passthrough);
			Assert.Null(element.Attribute("temp"));
		}

		[Theory]
		[InlineData("true", true)]
		[InlineData("false", false)]
		[InlineData(null, null)]
		public void NullableBoolean_Stringify(string expected, bool? value) {
			Assert.Equal(expected, ElementAttribute<bool?>.Stringify(value));
		}

		[Theory]
		[InlineData(true, "true")]
		[InlineData(true, "1")]
		[InlineData(false, "false")]
		[InlineData(false, "0")]
		[InlineData(null, null)]
		[InlineData(null, "")]
		public void NullableBoolean_Parse_OK(bool? expected, string value) {
			Assert.Equal(expected, ElementAttribute<bool?>.Parse(value));
		}

		[Theory]
		[InlineData("999")]
		[InlineData("TRUE")]
		[InlineData("False")]
		public void NullableBoolean_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<bool?>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("true", true)]
		[InlineData("false", false)]
		public void Boolean_Stringify(string expected, bool value) {
			Assert.Equal(expected, ElementAttribute<bool>.Stringify(value));
		}

		[Theory]
		[InlineData(true, "true")]
		[InlineData(true, "1")]
		[InlineData(false, "false")]
		[InlineData(false, "0")]
		public void Boolean_Parse_OK(bool expected, string value) {
			Assert.Equal(expected, ElementAttribute<bool>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("999")]
		[InlineData("TRUE")]
		[InlineData("False")]
		public void Boolean_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<bool>.Parse(value)).Message);
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		[InlineData(-1, "-1")]
		public void Int_Parse_OK(int expected, string value) {
			Assert.Equal(expected, ElementAttribute<int>.Parse(value));
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		[InlineData("-1", -1)]
		public void Int_Stringify(string expected, int value) {
			Assert.Equal(expected, ElementAttribute<int>.Stringify(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		public void Int_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<int>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1.1", 1.1)]
		[InlineData("0", 0.0)]
		[InlineData("-1.1", -1.1)]
		public void Double_Stringify(string expected, double value) {
			Assert.Equal(expected, ElementAttribute<double>.Stringify(value));
		}

		[Theory]
		[InlineData(1.1, "1.1")]
		[InlineData(0.0, "0.0")]
		[InlineData(-1.1, "-1.1")]
		public void Double_Parse_OK(double expected, string value) {
			Assert.Equal(expected, ElementAttribute<double>.Parse(value), 1);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void Double_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<double>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("2000-01-01T00:00:00Z", "2000-01-01T00:00:00")]
		public void DateTime_Stringify(string expected, string value) {
			Assert.Equal(expected, ElementAttribute<DateTime>.Stringify(DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)));
		}

		[Theory]
		[InlineData("2000-01-01T00:00:00", "2000-01-01T00:00:00Z")]
		[InlineData("2000-01-01T00:00:00", "2000-01-01")]
		public void DateTime_Parse_OK(string expected, string value) {
			Assert.Equal(DateTime.Parse(expected), ElementAttribute<DateTime>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void DateTime_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<DateTime>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("2000-01-01T00:00:00+00:00", "2000-01-01T00:00:00+00:00")]
		public void DateTimeOffset_Parse_OK(string expected, string value) {
			Assert.Equal(XmlConvert.ToDateTimeOffset(expected), ElementAttribute<DateTimeOffset>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void DateTimeOffset_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<DateTimeOffset>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("2000-01-01T00:00:00Z", "2000-01-01T00:00:00+00:00")]
		public void DateTimeOffset_Stringify(string expected, string value) {
			Assert.Equal(expected, ElementAttribute<DateTimeOffset>.Stringify(XmlConvert.ToDateTimeOffset(value)));
		}

		[Theory]
		[InlineData("1.1", 1.1)]
		[InlineData("0", 0.0)]
		[InlineData("-1.1", -1.1)]
		public void Decimal_Stringify(string expected, decimal value) {
			Assert.Equal(expected, ElementAttribute<decimal>.Stringify(value));
		}

		[Theory]
		[InlineData(1.1, "1.1")]
		[InlineData(0.0, "0.0")]
		[InlineData(-1.1, "-1.1")]
		public void Decimal_Parse_OK(decimal expected, string value) {
			Assert.Equal(expected, ElementAttribute<decimal>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void Decimal_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<decimal>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		[InlineData("-1", -1)]
		public void Long_Stringify(string expected, long value) {
			Assert.Equal(expected, ElementAttribute<long>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		[InlineData(-1, "-1")]
		public void Long_Parse_OK(long expected, string value) {
			Assert.Equal(expected, ElementAttribute<long>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		public void Long_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<long>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		[InlineData("-1", -1)]
		public void Short_Stringify(string expected, short value) {
			Assert.Equal(expected, ElementAttribute<short>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		[InlineData(-1, "-1")]
		public void Short_Parse_OK(short expected, string value) {
			Assert.Equal(expected, ElementAttribute<short>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		public void Short_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<short>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1.1", 1.1f)]
		[InlineData("0", 0.0f)]
		[InlineData("-1.1", -1.1f)]
		public void Float_Stringify(string expected, float value) {
			Assert.Equal(expected, ElementAttribute<float>.Stringify(value));
		}

		[Theory]
		[InlineData(1.1f, "1.1")]
		[InlineData(0.0f, "0.0")]
		[InlineData(-1.1f, "-1.1")]
		public void Float_Parse_OK(float expected, string value) {
			Assert.Equal(expected, ElementAttribute<float>.Parse(value), 1);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void Float_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<float>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		public void Byte_Stringify(string expected, byte value) {
			Assert.Equal(expected, ElementAttribute<byte>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		public void Byte_Parse_OK(byte expected, string value) {
			Assert.Equal(expected, ElementAttribute<byte>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		[InlineData("-1")]
		public void Byte_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<byte>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		[InlineData("-1", -1)]
		public void SByte_Stringify(string expected, sbyte value) {
			Assert.Equal(expected, ElementAttribute<sbyte>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		[InlineData(-1, "-1")]
		public void SByte_Parse_OK(sbyte expected, string value) {
			Assert.Equal(expected, ElementAttribute<sbyte>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		public void SByte_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<sbyte>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		public void UInt_Stringify(string expected, uint value) {
			Assert.Equal(expected, ElementAttribute<uint>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		public void UInt_Parse_OK(uint expected, string value) {
			Assert.Equal(expected, ElementAttribute<uint>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		[InlineData("-1")]
		public void UInt_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<uint>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		public void ULong_Stringify(string expected, ulong value) {
			Assert.Equal(expected, ElementAttribute<ulong>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		public void ULong_Parse_OK(ulong expected, string value) {
			Assert.Equal(expected, ElementAttribute<ulong>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		[InlineData("-1")]
		public void ULong_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<ulong>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("0", 0)]
		public void UShort_Stringify(string expected, ushort value) {
			Assert.Equal(expected, ElementAttribute<ushort>.Stringify(value));
		}

		[Theory]
		[InlineData(1, "1")]
		[InlineData(0, "0")]
		public void UShort_Parse_OK(ushort expected, string value) {
			Assert.Equal(expected, ElementAttribute<ushort>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("1.1")]
		[InlineData("abc")]
		[InlineData("-1")]
		public void UShort_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<ushort>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("a", 'a')]
		public void Char_Stringify(string expected, char value) {
			Assert.Equal(expected, ElementAttribute<char>.Stringify(value));
		}

		[Theory]
		[InlineData('a', "a")]
		public void Char_Parse_OK(char expected, string value) {
			Assert.Equal(expected, ElementAttribute<char>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void Char_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<char>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("PT1S", "00:00:01")]
		public void TimeSpan_Stringify(string expected, string value) {
			Assert.Equal(expected, ElementAttribute<TimeSpan>.Stringify(TimeSpan.Parse(value)));
		}

		[Theory]
		[InlineData("PT1S", "PT1S")]
		public void TimeSpan_Parse_OK(string expected, string value) {
			Assert.Equal(XmlConvert.ToTimeSpan(expected), ElementAttribute<TimeSpan>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void TimeSpan_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<TimeSpan>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000")]
		[InlineData("aff57eaf-78c6-487f-8a77-a6be298deca1", "AFF57EAF-78C6-487F-8A77-A6BE298DECA1")]
		public void Guid_Stringify(string expected, string value) {
			Assert.Equal(expected, ElementAttribute<Guid>.Stringify(Guid.Parse(value)));
		}

		[Theory]
		[InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000")]
		[InlineData("AFF57EAF-78C6-487F-8A77-A6BE298DECA1", "AFF57EAF-78C6-487F-8A77-A6BE298DECA1")]
		[InlineData("AFF57EAF-78C6-487F-8A77-A6BE298DECA1", "{AFF57EAF-78C6-487F-8A77-A6BE298DECA1}")]
		public void Guid_Parse_OK(string expected, string value) {
			Assert.Equal(Guid.Parse(expected), ElementAttribute<Guid>.Parse(value));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("abc")]
		public void Guid_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<Guid>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("0.0.0.0", "0.0.0.0")]
		[InlineData("1.2.3.4", "1.2.3.4")]
		[InlineData("1.2.3", "1.2.3")]
		[InlineData("1.2", "1.2")]
		public void Version_Stringify(string expected, string value) {
			Assert.Equal(expected, ElementAttribute<Version>.Stringify(Version.Parse(value)));
		}

		[Theory]
		[InlineData("0.0.0.0", "0.0.0.0")]
		[InlineData("1.2.3.4", "1.2.3.4")]
		[InlineData("1.2.3", "1.2.3")]
		[InlineData("1.2", "1.2")]
		public void Version_Parse_OK(string expected, string value) {
			Assert.Equal(Version.Parse(expected), ElementAttribute<Version>.Parse(value));
		}

		[Theory]
		[InlineData("abc")]
		[InlineData("-1.0")]
		[InlineData("12345678901.0")]
		[InlineData("1.2.3.4.5")]
		public void Version_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<Version>.Parse(value)).Message);
		}

		[Theory]
		[InlineData("All", AttributeTargets.All)]
		[InlineData("Parameter", AttributeTargets.Parameter)]
		public void Enum_Stringify(string expected, AttributeTargets value) {
			Assert.Equal(expected, ElementAttribute<AttributeTargets>.Stringify(value));
		}

		[Theory]
		[InlineData(AttributeTargets.All, "All")]
		[InlineData(AttributeTargets.Parameter, "Parameter")]
		public void Enum_Parse_OK(AttributeTargets expected, string value) {
			Assert.Equal(expected, ElementAttribute<AttributeTargets>.Parse(value));
		}

		[Theory]
		[InlineData("abc")]
		[InlineData("-1")]
		public void Enum_Parse_Error(string value) {
			Output.WriteLine(Assert.ThrowsAny<Exception>(() => ElementAttribute<AttributeTargets>.Parse(value)).Message);
		}
	}
}
