using System;
using System.Collections.Generic;
using System.Linq;

using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class LookupsExtensions {
		public static Lookups ToLookups(this IEnumerable<Lookup> that, bool latestVersion) {
			var result = new Lookups();
			foreach (var lookup in that.Where(l => l != null).Distinct(latestVersion ? LookupEqualityComparer.DefaultIgnoreVersion : LookupEqualityComparer.Default)) {
				result.Add(-1, new Lookup() {
						Item = lookup.Item,
						ObjectType = lookup.ObjectType,
						Version = latestVersion ? -1 : lookup.Version
				});
			}
			return result;
		}

		public static Lookups ToLookups(this IEnumerable<int> that, int? objectType = default) {
			var result = new Lookups();
			result.AddRange(that.Distinct().Select(i => {
				var lookup = new Lookup() { Item = i, Version = -1 };
				if (objectType.HasValue) {
					lookup.ObjectType = objectType.Value;
				}
				return lookup;
			}));
			return result;
		}

		public static IEnumerable<int> GetLookupIDs(this Lookups that) {
			return that.Cast<Lookup>().Select(l => l.Item);
		}
	}
}
