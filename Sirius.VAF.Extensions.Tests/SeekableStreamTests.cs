using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF {
	public class SeekableStreamTests {
		protected ITestOutputHelper Output {
			get;
		}

		public SeekableStreamTests(ITestOutputHelper output) {
			Output = output;
		}

		[Fact]
		public void ReadStreamTest() {
			const int length = 200000;
			var underlyingData = new byte[length];
			new Random(0).NextBytes(underlyingData);
			var underlyingStream = new Mock<Stream>(MockBehavior.Strict);
			var underlyingPosition = 0;
			underlyingStream.SetupGet(s => s.CanRead).Returns(true);
			underlyingStream.SetupGet(s => s.CanWrite).Returns(false);
			underlyingStream.SetupGet(s => s.CanSeek).Returns(false);
			underlyingStream.SetupGet(s => s.Length).Returns(underlyingData.Length);
			underlyingStream.SetupGet(s => s.Position).Returns(() => underlyingPosition);
			using (var stream = new SeekableStream(underlyingStream.Object, SeekableStream.Mode.Read)) {
				underlyingStream.Setup(s => s.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
						.Returns((byte[] buffer, int offset, int count) => {
							count = Math.Min(count, underlyingData.Length-underlyingPosition);
							Array.Copy(underlyingData, underlyingPosition, buffer, offset, count);
							underlyingPosition += count;
							Output.WriteLine("Requested {0} bytes", count);
							return count;
						})
						.Verifiable();
				underlyingStream.Verify(s => s.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
				var actual = new byte[25000];
				while (underlyingPosition < underlyingData.Length) {
					var readPosition = (int)stream.Position;
					var readCount = Math.Min(actual.Length, underlyingData.Length-readPosition);
					Output.WriteLine("Read {0} bytes...", readCount);
					Assert.Equal(readCount, stream.Read(actual, 0, actual.Length));
					Assert.Equal(underlyingData.Skip(readPosition).Take(readCount), actual.Take(readCount));
					underlyingStream.Verify(s => s.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly((underlyingPosition+(SeekableStream.ChunkSize-1)) / SeekableStream.ChunkSize));
				}
				underlyingStream.Setup(s => s.Close())
						.Verifiable();
			}
			Output.WriteLine("Read completed");
			underlyingStream.Verify(s => s.Close(), Times.Once);
		}
	}
}
