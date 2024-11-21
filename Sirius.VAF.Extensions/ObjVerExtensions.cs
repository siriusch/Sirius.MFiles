using MFilesAPI;

namespace Sirius.VAF {
	public static class ObjVerExtensions {
		public static Lookup ToSpecificVersionLookup(this ObjVer that) {
			return new Lookup() {
					ObjectType = that.Type,
					Item = that.ID,
					Version = that.Version
			};
		}

		public static Lookup ToLatestVersionLookup(this ObjVer that) {
			return new Lookup() {
					ObjectType = that.Type,
					Item = that.ID,
					Version = -1
			};
		}
	}
}
