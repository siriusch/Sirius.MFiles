using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

using JetBrains.Annotations;

using Sirius.VAF.VaultDom.Archive;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.VAF.VaultDom {
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
				foreach (var element in archive.Descendants().Where(e => e.GetType() == typeof(XElement))) {
					var isFlags = false;
					for (var parentType = element.Parent?.GetType(); parentType != null; parentType = parentType.BaseType) {
						if (parentType.IsGenericType == true && parentType.GetGenericTypeDefinition() == typeof(FlagsElementBase<>)) {
							isFlags = true;
							break;
						}
					}
					if (isFlags) {
						continue;
					}
					if (element.Name == "customdata") {
						continue;
					}
					if (element.Parent?.Name == "flags") {
						continue;
					}
					if (element.Parent?.Name == "propertydef" && element.Name == "defaultvalue") {
						continue;
					}
					if (element.Parent?.Name == "validation" && element.Name == "string") {
						continue;
					}
					if (element.Parent?.Name == "signature" && (element.Name == "reason" || element.Name == "meaning" || element.Name == "additionalinfo" || element.Name == "signatureid")) {
						continue;
					}
					if ((element.Parent?.Name == "workflow.2" || element.Parent?.Name == "state.2" || element.Parent?.Name == "transition") && element.Name == "description") {
						continue;
					}
					if ((element.Parent?.Name == "createassignment" || element.Parent?.Name == "CreateSeparateAssignment") && (element.Name == "title" || element.Name == "description")) {
						continue;
					}
					if (element.Parent?.Name == "sendnotification" && (element.Name == "subject" || element.Name == "message")) {
						continue;
					}
					if (element.Parent?.Name == "external" && (element.Name == "externalname" || element.Name == "organizationalunit")) {
						continue;
					}
					Output.WriteLine(element.Parent?.Name+": "+element.ToString(SaveOptions.DisableFormatting));
				}
				Output.WriteLine(string.Join(", ", archive.Root.Elements().Select(e => e.Name)));
			}
		}
	}
}
