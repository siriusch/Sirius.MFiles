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

using Sirius.MFiles.VafUtil.Options;

namespace Sirius.MFiles.VafUtil.Verbs {
	internal class GenerateVerb {
		private static readonly Regex rxNamespaceDetect = new(@"^(//[^\r\n]+|/\*((?!\*/).)*\*/|[^{])*\bnamespace\s+(?<ns>[\p{L}_][\p{L}0-9_]*(\s*\.\s*[\p{L}_][\p{L}0-9_]*)*)\b\s*\{", RegexOptions.ExplicitCapture|RegexOptions.CultureInvariant);

		private static XslCompiledTransform LoadEmbeddedTransform(string name) {
			var transform = new XslCompiledTransform();
			using (var reader = XmlReader.Create(typeof(GenerateVerb).Assembly.GetManifestResourceStream(typeof(GenerateVerb), name) ?? throw new InvalidOperationException(), new XmlReaderSettings() {
					       XmlResolver = null,
					       DtdProcessing = DtdProcessing.Ignore
			       })) {
				transform.Load(reader);
			}
			return transform;
		}

		private static void DetectNamespace(GenerateOptions opts, FileStream source) {
			if (string.IsNullOrEmpty(opts.Namespace)) {
				using (var reader = new StreamReader(source, Encoding.UTF8, true, 1024, true)) {
					var match = rxNamespaceDetect.Match(reader.ReadToEnd());
					if (match.Success) {
						opts.Namespace = match.Groups["ns"].Value;
						Console.WriteLine("Detected namespace "+opts.Namespace);
					} else {
						throw new InvalidOperationException("No namespace detected, please specify explicitly");
					}
				}
				source.Seek(0, SeekOrigin.Begin);
			}
		}

		private class Extension: IDisposable {
			private static readonly Regex rxAliasToName = new(@"^\p{Lu}{2,}|\p{L}(\p{Ll}|[0-9_])*", RegexOptions.Compiled|RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture);
			private static readonly Regex rxMarks = new(@"\p{M}+", RegexOptions.Compiled|RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture);

			private readonly Dictionary<string, string> aliasToName = new(StringComparer.InvariantCulture);
			private readonly Dictionary<string, string> nameToAlias = new(StringComparer.InvariantCulture);
			private readonly CodeDomProvider codeDomProvider;

			public Extension() {
				codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
			}

			// ReSharper disable once UnusedMember.Local
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

			// ReSharper disable once UnusedMember.Local
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
			var structureXml = XDocument.Load(Path.Combine(opts.ConfigFolder, @"Metadata\Structure.xml"));
			using (var output = File.Open(opts.Output, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read)) {
				DetectNamespace(opts, output);
				using (var extension = new Extension()) {
					var args = new XsltArgumentList();
					args.AddExtensionObject("urn:VafUtil", extension);
					args.AddParam("kind", "", opts.Kind.ToString());
					args.AddParam("namespace", "", opts.Namespace);
					args.AddParam("views", "", opts.Views);
					args.AddParam("verbose", "", opts.Verbose);
					args.XsltMessageEncountered += (_, a) => Console.WriteLine(a.Message);
					using (var writer = new StreamWriter(output, Encoding.UTF8, 1024, true)) {
						LoadEmbeddedTransform("StructureToCs.xslt").Transform(structureXml.CreateNavigator(), args, writer);
					}
				}
				output.SetLength(output.Position);
			}
		}
	}
}
