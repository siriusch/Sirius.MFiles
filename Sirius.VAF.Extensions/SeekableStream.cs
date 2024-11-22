using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Sirius.VAF {
	public sealed class SeekableStream: MemoryStream {
		public enum Mode {
			Read,
			Write,
			WriteNoFlush
		}

		public static Stream Ensure([NotNull] Stream stream) {
			return stream.CanSeek ? stream : new SeekableStream(stream, stream.CanRead ? Mode.Read : Mode.Write);
		}

		private readonly Stream underlyingStream;
		private readonly Mode mode;
		private bool disposed;
		private long bytesFlushed = 0;

		public SeekableStream([NotNull] Stream underlyingStream, Mode mode): base() {
			this.underlyingStream = underlyingStream ?? throw new ArgumentNullException(nameof(underlyingStream));
			this.mode = Mode.WriteNoFlush; // for the initial CopyTo we need to be writeable
			if (mode == Mode.Read) {
				this.underlyingStream.CopyTo(this);
				Seek(0, SeekOrigin.Begin);
			}
			this.mode = mode;
		}

		public override bool CanWrite => !disposed && mode != Mode.Read;

		public override bool CanRead => !disposed;

		public override bool CanSeek => !disposed;

		public override void SetLength(long value) {
			if (mode == Mode.Read) {
				throw new NotSupportedException();
			}
			if (value < bytesFlushed) {
				throw new InvalidOperationException("Cannot set length before the flushed position of the stream");
			}
			base.SetLength(value);
		}

		public override void Flush() {
			if (mode == Mode.Read) {
				throw new NotSupportedException();
			}
			if (mode != Mode.WriteNoFlush) {
				FlushInternal();
			}
		}

		private void FlushInternal() {
			if (bytesFlushed < Length) {
				var position = Position;
				try {
					Seek(bytesFlushed, SeekOrigin.Begin);
					CopyTo(underlyingStream);
					bytesFlushed = Position;
				} finally {
					Seek(position, SeekOrigin.Begin);
				}
			}
		}

		public override void Write(byte[] buffer, int offset, int count) {
			if (mode == Mode.Read) {
				throw new NotSupportedException();
			}
			if (Position < bytesFlushed) {
				throw new InvalidOperationException("Cannot modify the flushed position of the stream");
			}
			base.Write(buffer, offset, count);
		}

		protected override void Dispose(bool disposing) {
			try {
				if (!disposed && disposing) {
					if (mode != Mode.Read) {
						FlushInternal();
					}
					underlyingStream.Close();
				}
			} finally {
				disposed = true;
				base.Dispose(disposing);
			}
		}
	}
}
