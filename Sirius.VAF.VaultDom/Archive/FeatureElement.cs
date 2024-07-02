using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Archive {
	public class FeatureElement: XElement {
		private static readonly Func<Guid, string> StringifyGuid = value => value.ToString("D").ToUpperInvariant();

		public static readonly XName ElementName = "feature";

		public FeatureElement(): base(ElementName) { }

		public FeatureElement(XElement node): base(node) { }

		public Guid Id {
			get => ElementAttribute<Guid>.Get(this, "id");
			set => ElementAttribute<Guid>.Set(this, "id", value, StringifyGuid);
		}

		public string FeatureName {
			get => ElementAttribute<string>.Get(this, "name");
			set => ElementAttribute<string>.Set(this, "name", value);
		}

		public int PkgVer {
			get => ElementAttribute<int>.Get(this, "pkgver");
			set => ElementAttribute<int>.Set(this, "pkgver", value);
		}
	}
}
