using System.Runtime.Serialization;

using MFiles.VAF.Extensions;

using MFilesAPI;

namespace Sirius.VAF {
	[DataContract]
	public class ObjIDTaskDirective<T>: ObjIDTaskDirective {
		public ObjIDTaskDirective(): base() { }

		public ObjIDTaskDirective(ObjID objID, T data, string displayName = null): base(objID, displayName) {
			Data = data;
		}

		[DataMember]
		public T Data {
			get;
			set;
		}
	}
}
