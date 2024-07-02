using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class ObjectVersionAndPropertiesExtensions {
		public static ObjVerEx ToObjVerEx(this ObjectVersionAndProperties that) {
			if (that == null) {
				return null;
			}
			return new ObjVerEx(that.Vault, that);
		}
	}
}
