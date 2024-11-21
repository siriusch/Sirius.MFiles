using System.Collections.Generic;
using System.Linq;

using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class LookupExtensions {
		public static Lookup ToLatestVersionLookup(this Lookup that) {
			return new Lookup() {
					ObjectType = that.ObjectType,
					Item = that.Item,
					Version = -1
			};
		}

		public static Lookup ToSpecificVersionLookup(this Lookup that, int version) {
			return new Lookup() {
					ObjectType = that.ObjectType,
					Item = that.Item,
					Version = version
			};
		}
	}
}
