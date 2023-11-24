using System;

using CommandLine;

namespace Sirius.MFiles.VafUtil.Options {
	[Verb("generate")]
	public class GenerateOptions {
		public enum GenerateKind {
			AliasFile,
			ConfigurationFile
		}

		[Value(0, Required = true, HelpText = "Output to generate.")]
		public GenerateKind Kind {
			get;
			set;
		}

		[Value(1, Required = true, HelpText = "M-Files Configuration Export folder.")]
		public string ConfigFolder {
			get;
			set;
		}

		[Value(2, Required = true, HelpText = "Output filename.")]
		public string Output {
			get;
			set;
		}

		[Option('n', "namespace", HelpText = "C# Namespace.")]
		public string Namespace {
			get;
			set;
		}

		[Option("views", HelpText = "Also generate code for views.", Default = false)]
		public bool Views {
			get;
			set;
		}

		[Option("verbose", HelpText = "Verbose output.", Default = false)]
		public bool Verbose {
			get;
			set;
		}
	}
}
