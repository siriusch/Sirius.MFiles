using System;
using System.Collections.Generic;

using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class TypedValueExtensions {
		public static IEnumerable<int> GetValueAsLookupIDs(this TypedValue that) {
			return that.GetValueAsLookups().GetLookupIDs();
		}

		public static void SetValueEx(this TypedValue that, MFDataType type, object value) {
			if (value == null) {
				that.SetValueToNULL(type);
			} else {
				switch (type) {
				case MFDataType.MFDatatypeMultiSelectLookup:
					if (value is not Lookups lookups) {
						lookups = new LookupsClass();
						switch (value) {
						case Lookup singleLookup:
							lookups.Add(-1, singleLookup);
							break;
						case int singleItem:
							lookups.Add(-1, new Lookup() {
									Item = singleItem
							});
							break;
						case IEnumerable<int> items:
							foreach (var item in items) {
								lookups.Add(-1, new Lookup() {
										Item = item
								});
							}
							break;
						default:
							throw new ArgumentException($"{value.GetType().FullName} cannot be used as Lookups", nameof(value));
						}
					}
					that.SetValueToMultiSelectLookup(lookups);
					break;
				case MFDataType.MFDatatypeLookup:
					if (value is not Lookup lookup) {
						if (value is int item) {
							lookup = new Lookup() {
									Item = item
							};
						} else {
							throw new ArgumentException($"{value.GetType().FullName} cannot be used as Lookup", nameof(value));
						}
					}
					that.SetValueToLookup(lookup);
					break;
				default:
					that.SetValue(type, value);
					break;
				}
			}
		}
	}
}
