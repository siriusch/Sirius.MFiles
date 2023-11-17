using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using CommandLine;
using CommandLine.Text;

using Sirius.MFiles.VafUtil.Options;
using Sirius.MFiles.VafUtil.Verbs;

namespace Sirius.MFiles.VafUtil {
	public static class Program {
		private static void Main(string[] args) {
			var parser = new Parser(settings => {
				settings.CaseSensitive = false;
				settings.CaseInsensitiveEnumValues = true;
			});
			var result = parser.ParseArguments<GenerateOptions, object>(args);
			result.WithParsed<GenerateOptions>(GenerateVerb.Execute)
					.WithNotParsed(errors => {
						var helpText = HelpText.AutoBuild(result,
								h => HelpText.DefaultParsingErrorsHandler(result, h),
								e => e);
						Console.WriteLine(helpText);
					});
			WaitForKeyPress();
		}

		[Conditional("DEBUG")]
		private static void WaitForKeyPress() {
			if (Debugger.IsAttached) {
				Console.WriteLine("Completed, press a key");
				Console.ReadKey();
			}
		}
	}
}
