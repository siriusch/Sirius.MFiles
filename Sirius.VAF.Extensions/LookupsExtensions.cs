using System;
using System.Collections.Generic;
using System.Linq;

using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class LookupsExtensions {
		public static Lookups ToLookups(this IEnumerable<int> that) {
			var result = new Lookups();
			result.AddRange(that.Select(i => new Lookup() { Item = i }));
			return result;
		}

		public static IEnumerable<int> GetLookupIDs(this Lookups that) {
			return that.Cast<Lookup>().Select(l => l.Item);
		}
	}
}
