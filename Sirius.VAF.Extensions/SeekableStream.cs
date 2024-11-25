using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Sirius.VAF {
	public sealed class SeekableStream: MemoryStream {
		public const int ChunkSize = 65536; // largest power of 2 below Large Object threshold of 85k

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
		private int bytesProcessed = 0;

		public SeekableStream([NotNull] Stream underlyingStream, Mode mode): base((int)(underlyingStream ?? throw new ArgumentNullException(nameof(underlyingStream))).Length * (mode == Mode.Read ? 1 : 0)) {
			Debug.Assert(underlyingStream.Position == 0);
			this.underlyingStream = underlyingStream;
			if (mode == Mode.Read) {
				this.mode = Mode.WriteNoFlush; // required for the SetLength setup call to succeed
				base.SetLength(this.underlyingStream.Length);
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
			if (value < bytesProcessed) {
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
			var buffer = GetBuffer();
			while (bytesProcessed < Length) {
				var count = Math.Min((int)Length, bytesProcessed-bytesProcessed % ChunkSize+ChunkSize)-bytesProcessed;
				underlyingStream.Write(buffer, bytesProcessed, count);
				bytesProcessed += count;
			}
		}

		public override void Write(byte[] buffer, int offset, int count) {
			if (mode == Mode.Read) {
				throw new NotSupportedException();
			}
			if (Position < bytesProcessed) {
				throw new InvalidOperationException("Cannot modify the flushed position of the stream");
			}
			base.Write(buffer, offset, count);
		}

		public override int Read(byte[] buffer, int offset, int count) {
			EnsureDataAvailable((int)Math.Min(Position+count, Length));
			return base.Read(buffer, offset, count);
		}

		public override byte[] GetBuffer() {
			EnsureDataAvailable((int)Length);
			return base.GetBuffer();
		}

		private void EnsureDataAvailable(int length) {
			if (mode != Mode.Read || length <= bytesProcessed) {
				return;
			}
			Debug.Assert(length <= Length);
			var buffer = base.GetBuffer();
			do {
				var bytesRead = underlyingStream.Read(buffer, bytesProcessed, Math.Min(ChunkSize, buffer.Length-bytesProcessed));
				if (bytesRead == 0) {
					throw new InvalidOperationException("Unexpected end of underlying stream");
				}
				bytesProcessed += bytesRead;
			} while (bytesProcessed < length);
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
