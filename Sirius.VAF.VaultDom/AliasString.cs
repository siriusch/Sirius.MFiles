using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sirius.VAF.VaultDom {
	public readonly struct AliasString {
		internal static readonly Func<string, AliasString> Parse = str => new AliasString(str);
		internal static readonly Func<AliasString, string> Stringify = value => value.value;

		private static readonly Regex rxAlias=new Regex(@"\G\s*(?<alias>[^;]+?)\s*(?:;|$)", RegexOptions.Compiled|RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture);

		private readonly string value;

		public AliasString(string value) {
			this.value = value;
		}

		public bool Empty => !Split().Any();

		public string PrimaryAlias => Split().FirstOrDefault();

		public IEnumerable<string> Split() {
			if (string.IsNullOrEmpty(value)) {
				yield break;
			}
			for (var match = rxAlias.Match(value); match.Success; match = match.NextMatch()) {
				var alias = match.Groups["alias"].Value;
				if (!string.IsNullOrEmpty(alias)) {
					yield return alias;
				}
			}
		}

		public string this[int index] => index<0 ? null : Split().Skip(index).FirstOrDefault();

		public bool this[string alias] => Split().Any(a => StringComparer.Ordinal.Equals(a, alias));

		public override string ToString() {
			return value;
		}
	}
}
