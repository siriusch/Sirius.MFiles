using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MFiles.VAF.Common;
using MFiles.VAF.MetadataCache;

using MFilesAPI;

using PdfiumViewer;

using ZXing;
using ZXing.Common;

namespace Sirius.VAF.Barcode {
	public class BarcodeFinder {
		public static bool IsPdf(Stream stream) {
			if (!stream.CanSeek) {
				throw new ArgumentException("The stream must be seekable", nameof(stream));
			}
			var position = stream.Position;
			try {
				var magic = new byte[5]; // PDF magic bytes: %PDF-
				return stream.Read(magic, 0, 5) == 5 && magic[0] == 0x25 && magic[1] == 0x50 && magic[2] == 0x44 && magic[3] == 0x46 && magic[4] == 0x2D;
			} finally {
				stream.Seek(position, SeekOrigin.Begin);
			}
		}

		private readonly BarcodeReader barcodeReader;

		public BarcodeFinder(params BarcodeFormat[] possibleFormats): this(new DecodingOptions() {
				PossibleFormats = possibleFormats,
				TryHarder = true
		}) { }

		public BarcodeFinder(DecodingOptions options) {
			barcodeReader = new BarcodeReader {
					Options = options,
					AutoRotate = false
			};
		}

		protected virtual IEnumerable<int> PdfGetPages(PdfDocument pdfDocument) {
			return Enumerable.Range(0, pdfDocument.PageCount);
		}

		protected virtual Bitmap PdfPageRender(PdfDocument pdfDocument, int page) {
			return (Bitmap)pdfDocument.Render(page, 300, 300, PdfRenderFlags.Grayscale|PdfRenderFlags.ForPrinting|PdfRenderFlags.CorrectFromDpi);
		}

		public IEnumerable<Result> DecodeMultiple(ObjVerEx objVerEx, CancellationToken cancellationToken = default) {
			return DecodeMultiple(objVerEx.Vault.ObjectFileOperations.GetFiles(objVerEx.ObjVer), objVerEx.Vault, cancellationToken);
		}

		public IEnumerable<Result> DecodeMultiple(ObjVer objVer, Vault vault, CancellationToken cancellationToken = default) {
			return DecodeMultiple(vault.ObjectFileOperations.GetFiles(objVer), vault, cancellationToken);
		}

		public IEnumerable<Result> DecodeMultiple(ObjectFiles objectFiles, Vault vault, CancellationToken cancellationToken = default) {
			return objectFiles.Cast<ObjectFile>().SelectMany(of => DecodeMultiple(of, vault, cancellationToken));
		}

		public IEnumerable<Result> DecodeMultiple(ObjectFile objectFile, Vault vault, CancellationToken cancellationToken = default) {
			// The iterator state machine is necessary to keep the stream open until done
			using var stream = objectFile.OpenReadSeekable(vault);
			foreach (var result in DecodeMultiple(stream, cancellationToken)) {
				yield return result;
			}
		}

		public IEnumerable<Result> DecodeMultiple(Stream stream, CancellationToken cancellationToken = default) {
			stream = SeekableStream.Ensure(stream);
			if (IsPdf(stream)) {
				var pdfDocument = PdfDocument.Load(stream);
				return PdfGetPages(pdfDocument)
						.SelectMany(page => cancellationToken.IsCancellationRequested ? Enumerable.Empty<Result>() : DecodeMultiple(PdfPageRender(pdfDocument, page), cancellationToken));
			}
			Bitmap bitmap;
			try {
				bitmap = (Bitmap)Image.FromStream(stream);
			} catch (ArgumentException) {
				return Enumerable.Empty<Result>();
			}
			using (bitmap) {
				return DecodeMultiple(bitmap, cancellationToken);
			}
		}

		public Result[] DecodeMultiple(Bitmap bitmap, CancellationToken cancellationToken = default) {
			if (cancellationToken.IsCancellationRequested) {
				return Array.Empty<Result>();
			}
			var results = barcodeReader.DecodeMultiple(bitmap);
			return results?.Length > 0 ? results : Array.Empty<Result>();
		}
	}
}
