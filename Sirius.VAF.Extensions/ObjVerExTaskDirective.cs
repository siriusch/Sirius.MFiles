using System.Runtime.Serialization;

using MFiles.VAF.Common;
using MFiles.VAF.Extensions;

using MFilesAPI;

namespace Sirius.VAF {
	[DataContract]
	public class ObjVerExTaskDirective<T>: ObjVerExTaskDirective {
		public ObjVerExTaskDirective(): base() { }

		public ObjVerExTaskDirective(ObjVerEx objVerEx, T data, string displayName = null): base(objVerEx, displayName) {
			Data = data;
		}

		public ObjVerExTaskDirective(ObjVer objVer, T data, string displayName = null): base(objVer, displayName) {
			Data = data;
		}

		[DataMember]
		public T Data {
			get;
			set;
		}
	}
}
