using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Sirius.VAF {
	public static class Checksum {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int DigitToInt(this char c) {
			return c-'0';
		}

		public static bool CheckMod10Rec(string value) {
			return Regex.IsMatch(value, "^[0-9]{2,}$", RegexOptions.CultureInvariant) &&
			       ComputeMod10RecInternal(value.Substring(0, value.Length-1)) == DigitToInt(value[value.Length-1]);
		}

		public static int ComputeMod10Rec(string value) {
			if (!Regex.IsMatch(value, "^[0-9]+$", RegexOptions.CultureInvariant)) {
				throw new ArgumentException("Invalid data", nameof(value));
			}
			return ComputeMod10RecInternal(value);
		}

		private static int ComputeMod10RecInternal(string value) {
			var checksum = 0;
			for (var i = 0; i < value.Length; i++) {
				var digit = DigitToInt(value[i]);
				checksum = ((checksum+digit) % 10) switch {
						1 => 9,
						2 => 4,
						3 => 6,
						4 => 8,
						5 => 2,
						6 => 7,
						7 => 1,
						8 => 3,
						9 => 5,
						_ => 0
				};
			}
			return (10-checksum) % 10;
		}

		public static int ComputeMod11(string value) {
			if (!Regex.IsMatch(value, "^[0-9]{8}$", RegexOptions.CultureInvariant)) {
				throw new ArgumentException("Invalid data", nameof(value));
			}
			return ComputeMod11Internal(value);
		}

		public static bool CheckMod11(string value) {
			return Regex.IsMatch(value, "^[0-9]{9}$", RegexOptions.CultureInvariant) &&
			       ComputeMod11Internal(value) == value[8].DigitToInt();
		}

		private static int ComputeMod11Internal(string value) {
			return 11-(
					value[0].DigitToInt() * 5+
					value[1].DigitToInt() * 4+
					value[2].DigitToInt() * 3+
					value[3].DigitToInt() * 2+
					value[4].DigitToInt() * 7+
					value[5].DigitToInt() * 6+
					value[6].DigitToInt() * 5+
					value[7].DigitToInt() * 4
			) % 11;
		}

		public static int ComputeMod97(string prefix, string value) {
			if (!Regex.IsMatch(prefix, @"^[A-Z0-9]{2}$")) {
				throw new ArgumentException("Invalid data", nameof(prefix));
			}
			if (!Regex.IsMatch(value, @"^[A-Z0-9]*$")) {
				throw new ArgumentException("Invalid data", nameof(value));
			}
			return ComputeMod97Internal(prefix, value);
		}

		private static int ComputeMod97Internal(string prefix, string value) {
			value = Regex.Replace(value+prefix+"00", "[A-Z]", m => (m.Value[0]-'A'+10).ToString(CultureInfo.InvariantCulture));
			var leftOver = 0;
			for (var index = 0; index < value.Length; index++) {
				leftOver = (10 * leftOver+value[index].DigitToInt()) % 97;
			}
			return 98-leftOver;
		}

		public static bool CheckMod97(string value) {
			return Regex.IsMatch(value, @"^[A-Z0-9]{4,}$") &&
			       ComputeMod97Internal(value.Substring(0, 2), value.Substring(4)) == int.Parse(value.Substring(2, 2), NumberStyles.Integer, CultureInfo.InvariantCulture);
		}

		public static int ComputeEan13(string value) {
			if (!Regex.IsMatch(value, @"^[0-9]{12}$")) {
				throw new ArgumentException("Invalid data", nameof(value));
			}
			return ComputeEan13Internal(value);
		}

		public static bool CheckEan13(string value) {
			return Regex.IsMatch(value, @"^[0-9]{13}$") &&
			       ComputeEan13Internal(value.Substring(0, value.Length-1)) == value[value.Length-1].DigitToInt();
		}

		private static int ComputeEan13Internal(string value) {
			// loop through value character by character
			var leftOver = 0;
			var multiplier = 3;
			for (var index = value.Length-1; index >= 0; index--) {
				leftOver += value[index].DigitToInt() * multiplier;
				multiplier ^= 2;
			}
			leftOver = -leftOver % 10;
			if (leftOver < 0) {
				leftOver += 10;
			}
			return leftOver;
		}

		public static string StripWhitespace(string value) {
			return string.IsNullOrEmpty(value) ? value : Regex.Replace(value, @"\s+", "");
		}

		public static string AddWhitespace(string nr, int groupSize, bool rightToLeft = false) {
			var groups = Enumerable
					.Range(0, (nr.Length+(groupSize-1)) / groupSize)
					.Select(i => Math.Min(groupSize, nr.Length-i * groupSize));
			if (rightToLeft) {
				groups = groups.Reverse();
			}
			var builder = new StringBuilder((nr.Length / groupSize+1) * (groupSize+1));
			var index = 0;
			foreach (var group in groups) {
				builder.Append(nr, index, group);
				builder.Append(' ');
				index += group;
			}
			return builder.ToString(0, builder.Length-1);
		}
	}
}
