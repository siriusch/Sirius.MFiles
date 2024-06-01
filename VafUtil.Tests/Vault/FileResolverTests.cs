using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

using JetBrains.Annotations;

using Sirius.MFiles.VafUtil.Vault;

using Xunit;
using Xunit.Abstractions;

namespace VafUtil.Vault {
	[TestSubject(typeof(FileResolver))]
	public class FileResolverTests {
		protected ITestOutputHelper Output {
			get;
		}

		public FileResolverTests(ITestOutputHelper output) {
			Output = output;
		}

		[Fact]
		public void ResolveEntity() {
			var fileResolver = new FileResolver(@"C:\hg\VafCommune\Vault");
			var result = (Stream)fileResolver.GetEntity(new Uri("Index.xml", UriKind.Relative), null, typeof(Stream));
			Assert.NotNull(result);
			using (var reader = XmlReader.Create(result, new XmlReaderSettings() {
					       XmlResolver = fileResolver,
					       DtdProcessing = DtdProcessing.Parse
			       })) {
				var doc = XDocument.Load(reader);
				var archive = new ArchiveDocument(doc.Root);
				Output.WriteLine(archive.ToString());
				Output.WriteLine(string.Join(", ", archive.Root.Elements().Select(e => e.Name)));
			}
		}
	}
}
