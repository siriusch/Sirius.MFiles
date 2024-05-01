using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;

using MFilesAPI;

using OneOf;

namespace Sirius.VAF {
	public class MFStringBuilder {
		private const char ZeroWidthJoiner = '\u200D';

		private static readonly Regex rxNormalizeSpaces = new(@"^\s+|(?<=[\x20\t\(])[\x20\t]+|[\x20\t]*"+ZeroWidthJoiner+@"[\x20\t]*|[\x20\t]+(?=[\)\.,;])|\s+$", RegexOptions.Singleline|RegexOptions.CultureInvariant|RegexOptions.Compiled|RegexOptions.ExplicitCapture);

		private static string FormatValue(OneOf<string, Func<PropertyValue, string>> formatter, PropertyValue val) {
			return formatter.Match(
					str => val.Value.IsNULL() || val.Value.IsUninitialized()
							? ""
							: str == null
									? val.GetValueAsLocalizedTextEx(true)
									: ((IFormattable)val.Value.Value).ToString(str, null),
					fmt => fmt?.Invoke(val) ?? val.GetValueAsLocalizedTextEx(true));
		}

		private static string SingleText(ObjVerEx objVerEx, MFIdentifier propertyDef, OneOf<string, Func<PropertyValue, string>> formatter) {
			return objVerEx.TryGetProperty(propertyDef, out var val) ? FormatValue(formatter, val) : "";
		}

		[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
		private static string PathText(ObjVerEx obj, IEnumerable<OneOf<MFIdentifier, MFBuiltInPropertyDef>> mfis, OneOf<string, Func<PropertyValue, string>> formatter) {
			using var enumerator = mfis.GetEnumerator();
			if (obj == null || !enumerator.MoveNext()) {
				return "";
			}
			var mfi = enumerator.Current.Match(id => id, p => p);
			if (mfi == null || !obj.TryGetProperty(mfi, out var val)) {
				// Property not defined
				return "";
			}
			if (!enumerator.MoveNext()) {
				// Last item in the path
				return FormatValue(formatter, val);
			}
			return val.Value.DataType switch {
					MFDataType.MFDatatypeLookup => PathText(val.LookupToObjVerEx(obj.Vault), mfis.Skip(1), formatter),
					MFDataType.MFDatatypeMultiSelectLookup => string.Join(", ", val.LookupsToObjVerEx(obj.Vault)
							.Select(o => PathText(o, mfis.Skip(1), formatter))
							.Where(s => !string.IsNullOrEmpty(s))),
					_ => throw new InvalidOperationException($"Cannot use the property {mfi.Alias} with data type {val.Value.DataType} as path property.")
			};
		}

		private readonly List<OneOf<
				string,
				MFIdentifier,
				(MFIdentifier PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				MFBuiltInPropertyDef,
				(MFBuiltInPropertyDef PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				OneOf<MFIdentifier, MFBuiltInPropertyDef>[],
				(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter)>> fragments = new();

		public ObjVerEx ObjVerEx {
			get;
		}

		public char Joiner {
			get;
		}

		public MFStringBuilder(ObjVerEx objVerEx, char joiner = '~') {
			ObjVerEx = objVerEx;
			Joiner = joiner;
		}

		public MFStringBuilder Append(string fragment) {
			fragments.Add(fragment);
			return this;
		}

		public MFStringBuilder Append(MFIdentifier fragment) {
			fragments.Add(fragment);
			return this;
		}

		public MFStringBuilder Append(MFIdentifier fragment, string formatter) {
			fragments.Add((fragment, formatter));
			return this;
		}

		public MFStringBuilder Append(MFIdentifier fragment, Func<PropertyValue, string> formatter) {
			fragments.Add((fragment, formatter));
			return this;
		}

		public MFStringBuilder Append(MFBuiltInPropertyDef fragment) {
			fragments.Add(fragment);
			return this;
		}

		public MFStringBuilder Append(MFBuiltInPropertyDef fragment, string formatter) {
			fragments.Add((fragment, formatter));
			return this;
		}

		public MFStringBuilder Append(MFBuiltInPropertyDef fragment, Func<PropertyValue, string> formatter) {
			fragments.Add((fragment, formatter));
			return this;
		}

		public MFStringBuilder Append(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] fragment) {
			fragments.Add(fragment);
			return this;
		}

		public MFStringBuilder Append(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] fragment, OneOf<string, Func<PropertyValue, string>> formatter) {
			fragments.Add((fragment, formatter));
			return this;
		}

		public MFStringBuilder Append((OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter) fragment) {
			fragments.Add(fragment);
			return this;
		}

		public MFStringBuilder AppendRange(IEnumerable<OneOf<
				string,
				MFIdentifier,
				(MFIdentifier PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				MFBuiltInPropertyDef,
				(MFBuiltInPropertyDef PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				OneOf<MFIdentifier, MFBuiltInPropertyDef>[],
				(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter)>> fragments) {
			this.fragments.AddRange(fragments);
			return this;
		}

		public override string ToString() {
			if (ObjVerEx == null) {
				return string.Empty;
			}
			return rxNormalizeSpaces.Replace(string.Join(" ", fragments.Select(fragment => fragment.Match(
							str => str.Replace(Joiner, ZeroWidthJoiner),
							mfi => SingleText(ObjVerEx, mfi, (string)null),
							mfi => SingleText(ObjVerEx, mfi.PropertyDef, mfi.Formatter),
							mfp => SingleText(ObjVerEx, mfp, (string)null),
							mfp => SingleText(ObjVerEx, mfp.PropertyDef, mfp.Formatter),
							mfis => PathText(ObjVerEx, mfis, (string)null),
							mfis => PathText(ObjVerEx, mfis.PropertyDefs, mfis.Formatter)))),
					"");
		}

		public TypedValue ToTextTypedValue() {
			var result = new TypedValue();
			result.SetValue(MFDataType.MFDatatypeText, ToString());
			return result;
		}

		public TypedValue ToMultiLineTextTypedValue() {
			var result = new TypedValue();
			result.SetValue(MFDataType.MFDatatypeMultiLineText, ToString());
			return result;
		}
	}
}
