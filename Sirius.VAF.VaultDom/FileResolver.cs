using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Sirius.VAF.VaultDom {
	public class FileResolver: XmlResolver {
		public Uri BaseUri {
			get;
		}

		public FileResolver(string baseDirectory) {
			baseDirectory = Path.GetFullPath(baseDirectory ?? throw new ArgumentNullException(nameof(baseDirectory)));
			if (!Directory.Exists(baseDirectory)) {
				throw new ArgumentException("The base directory must exist");
			}
			BaseUri = new Uri(FormattableString.Invariant($"file:{baseDirectory}\\"));
		}

		public override Uri ResolveUri(Uri baseUri, string relativeUri) {
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			var resolvedUri = new Uri(baseUri == null || baseUri.OriginalString.Length == 0 ? BaseUri : baseUri, relativeUri);
			AssertAbsoluteUri(ref resolvedUri);
			return resolvedUri;
		}

		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn) {
			AssertAbsoluteUri(ref absoluteUri);
			if (ofObjectToReturn != null && ofObjectToReturn != typeof(Stream)) {
				throw new ArgumentException("Only Stream is supported as type");
			}
			return File.OpenRead(absoluteUri.LocalPath);
		}

		private void AssertAbsoluteUri(ref Uri absoluteUri) {
			if (!absoluteUri.IsAbsoluteUri) {
				absoluteUri = new Uri(BaseUri, absoluteUri);
			}
			if (!BaseUri.IsBaseOf(absoluteUri)) {
				throw new InvalidOperationException("Cannot access paths outside the given base directory");
			}
		}

		public override bool SupportsType(Uri absoluteUri, Type type) {
			return BaseUri.IsBaseOf(absoluteUri) && (type == null || type == typeof(Stream));
		}

		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn) {
			return Task.FromResult(GetEntity(absoluteUri, role, ofObjectToReturn));
		}
	}
}
