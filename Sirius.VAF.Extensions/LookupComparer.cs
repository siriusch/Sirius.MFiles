using System;
using System.Collections.Generic;
using System.Linq;

using MFilesAPI;

namespace Sirius.VAF {
	public class LookupComparer: IEqualityComparer<Lookup>, IEqualityComparer<Lookups> {
		public static readonly LookupComparer Default = new(false);
		public static readonly LookupComparer VersionInsensitive = new(true);

		private readonly bool ignoreVersion;

		public LookupComparer(bool ignoreVersion) {
			this.ignoreVersion = ignoreVersion;
		}

		public bool Equals(Lookup x, Lookup y) {
			if (ReferenceEquals(x, y)) {
				return true;
			}
			if (x == null || y == null) {
				return false;
			}
			return x.Item == y.Item && (ignoreVersion || x.Version == y.Version);
		}

		public int GetHashCode(Lookup obj) {
			return obj.Item.GetHashCode()^(ignoreVersion ? 0 : obj.Version).GetHashCode();
		}

		public bool Equals(Lookups x, Lookups y) {
			if (ReferenceEquals(x, y)) {
				return true;
			}
			if (x == null || y == null || x.Count != y.Count) {
				return false;
			}
			return x.Cast<Lookup>()
					.OrderBy(l => l.Item)
					.SequenceEqual(y.Cast<Lookup>()
							.OrderBy(l => l.Item), this);
		}

		public int GetHashCode(Lookups obj) {
			return obj.Cast<Lookup>()
					.Aggregate(0, (hc, l) => hc^GetHashCode(l));
		}
	}
}
