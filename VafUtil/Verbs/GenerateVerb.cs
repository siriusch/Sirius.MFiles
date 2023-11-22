using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

using CommandLine;

using Sirius.MFiles.VafUtil.Options;

namespace Sirius.MFiles.VafUtil.Verbs {
	internal class GenerateVerb {
		private class Extension: IDisposable {
			private readonly Dictionary<string, string> aliasToName = new Dictionary<string, string>(StringComparer.InvariantCulture);
			private readonly Dictionary<string, string> nameToAlias = new Dictionary<string, string>(StringComparer.InvariantCulture);

			private static readonly Regex rxAliasToName = new Regex(@"^\p{Lu}{2,}|\p{L}(\p{Ll}|[0-9_])*", RegexOptions.Compiled|RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture);
			private static readonly Regex rxMarks = new Regex(@"\p{M}+", RegexOptions.Compiled|RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture);
			private readonly CodeDomProvider codeDomProvider;

			public Extension() {
				codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
			}

			public string NormalizeName(string alias) {
				if (aliasToName.TryGetValue(alias, out var name)) {
					return name;
				}
				var result = new StringBuilder(alias.Length);
				for (var match = rxAliasToName.Match(rxMarks.Replace(alias
								     .Replace("ä", "ae")
								     .Replace("Ä", "Ae")
								     .Replace("ö", "oe")
								     .Replace("Ö", "Oe")
								     .Replace("ü", "ue")
								     .Replace("Ü", "Ue")
								     .Replace("ß", "ss")
								     .Normalize(NormalizationForm.FormD), "")
						     .Normalize());
				     match.Success;
				     match = match.NextMatch()) {
					result.Append(char.ToUpperInvariant(match.Value[0]));
					result.Append(match.Value.Substring(1).ToLowerInvariant());
				}
				name = result.ToString();
				if (string.IsNullOrEmpty(name)) {
					throw new InvalidOperationException($"Alias '{alias}' cannot be converted to a name");
				}
				if (nameToAlias.TryGetValue(name, out var existingAlias) && !string.Equals(alias, existingAlias, StringComparison.InvariantCulture)) {
					throw new InvalidOperationException($"Both aliases '{existingAlias}' and '{alias}' yield the same name '{name}'");
				}
				aliasToName.Add(alias, name);
				nameToAlias.Add(name, alias);
				return name;
			}

			public string CSharpString(string name, int depth) {
				using (var writer = new StringWriter()) {
					codeDomProvider.GenerateCodeFromExpression(new CodePrimitiveExpression(name), writer, new CodeGeneratorOptions {
							IndentString = new string('\t', depth)
					});
					return writer.ToString();
				}
			}

			void IDisposable.Dispose() {
				codeDomProvider.Dispose();
			}
		}

		public static void Execute(GenerateOptions opts) {
			var transform = new XslCompiledTransform();
			using (var reader = XmlReader.Create(typeof(GenerateVerb).Assembly.GetManifestResourceStream(typeof(GenerateVerb), "StructureToCs.xslt") ?? throw new InvalidOperationException(), new XmlReaderSettings() {
					       XmlResolver = null,
					       DtdProcessing = DtdProcessing.Ignore
			       })) {
				transform.Load(reader);
			}
			using (var extension = new Extension()) {
				var args = new XsltArgumentList();
				args.AddExtensionObject("urn:VafUtil", extension);
				args.AddParam("kind", "", opts.Kind.ToString());
				args.AddParam("namespace", "", opts.Namespace);
				args.AddParam("views", "", opts.Views);
				args.XsltMessageEncountered += (_, a) => Console.WriteLine(a.Message);
				var structureXml = XDocument.Load(Path.Combine(opts.ConfigFolder, @"Metadata\Structure.xml"));
				using (var output = File.CreateText(opts.Output)) {
					transform.Transform(structureXml.CreateNavigator(), args, output);
				}
			}
		}
	}
}
