using System.IO;

using MFilesAPI;
using MFilesAPI.Extensions;

namespace Sirius.VAF {
	public static class ObjectFileExtensions {
		public static Stream OpenReadSeekable(this ObjectFile that, Vault vault, MFFileFormat fileFormat = MFFileFormat.MFFileFormatNative) {
			return new SeekableStream(that.OpenRead(vault, fileFormat), SeekableStream.Mode.Read);
		}

		public static Stream OpenWriteSeekable(this ObjectFile that, Vault vault, ObjID objectId, bool automaticallyCommitOnDisposal = true) {
			return new SeekableStream(that.OpenWrite(vault, objectId, automaticallyCommitOnDisposal), SeekableStream.Mode.Write);
		}
	}
}
