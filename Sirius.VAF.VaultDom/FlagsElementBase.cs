using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Sirius.VAF.VaultDom {
	public abstract class FlagsElementBase<TFlags>: XElement where TFlags: Enum {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsPowerOfTwo(uint x) {
			return x != 0 && (x&(x-1)) == 0;
		}

		public FlagsElementBase(XName name): base(name) { }

		public FlagsElementBase(XElement node): base(node) { }

		protected virtual string Prefix => "";

		public new TFlags Value {
			get {
				// First try to use the value attribute, as this would be the most efficient approach
				var attr = Attribute("value");
				if (attr != null && uint.TryParse(attr.Value, NumberStyles.AllowLeadingWhite|NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out var value)) {
					return (TFlags)Enum.ToObject(typeof(TFlags), value);
				}
				// Second try to use the value attribute of each flag element
				var allParsed = true;
				uint combinedValue = 0;
				foreach (var element in Elements()) {
					attr = element.Attribute("value");
					if (attr == null || !uint.TryParse(attr.Value.StartsWith(Prefix, StringComparison.Ordinal) ? attr.Value.Substring(Prefix.Length) : attr.Value, NumberStyles.AllowLeadingWhite|NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out value)) {
						allParsed = false;
						break;
					}
					combinedValue |= value;
				}
				// If none of these worked, parse the names of the elements as enum values
				if (!allParsed) {
					combinedValue = 0;
					foreach (var element in Elements()) {
						combinedValue |= Convert.ToUInt32(Enum.Parse(typeof(TFlags), element.Name.LocalName));
					}
				}
				return (TFlags)Enum.ToObject(typeof(TFlags), combinedValue);
			}
			set {
				if (!EqualityComparer<TFlags>.Default.Equals(value, Value)) {
					RemoveNodes();
					var combinedValue = Convert.ToUInt32(value);
					foreach (var flag in ((TFlags[])Enum.GetValues(typeof(TFlags))).Where(f => IsPowerOfTwo(Convert.ToUInt32(f)))) {
						var numericValue = Convert.ToUInt32(flag);
						if (numericValue != 0 && (combinedValue&numericValue) == numericValue) {
							Add(new XElement(Prefix+flag, new XAttribute("value", numericValue)));
						}
					}
					SetAttributeValue("value", combinedValue);
				}
			}
		}
	}
}
