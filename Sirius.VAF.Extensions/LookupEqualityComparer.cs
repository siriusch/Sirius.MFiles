using System.Collections.Generic;

using MFilesAPI;

namespace Sirius.VAF {
	public class LookupEqualityComparer: IEqualityComparer<Lookup> {
		public static readonly LookupEqualityComparer Default = new(false);
		public static readonly LookupEqualityComparer DefaultIgnoreVersion = new(true);

		private readonly bool ignoreVersion;

		public LookupEqualityComparer(bool ignoreVersion) {
			this.ignoreVersion = ignoreVersion;
		}

		public bool Equals(Lookup x, Lookup y) {
			if (ReferenceEquals(x, y)) {
				return true;
			}
			if (x == null || y == null) {
				return false;
			}
			return x.ObjectType == y.ObjectType && x.Item == y.Item && (ignoreVersion || x.Version == y.Version);
		}

		public int GetHashCode(Lookup obj) {
			return (obj.ObjectType * 123457)^(obj.Item * 257)^(ignoreVersion ? -1 : obj.Version);
		}
	}
}
