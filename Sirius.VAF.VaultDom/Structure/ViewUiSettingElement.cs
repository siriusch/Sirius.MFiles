using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewUiSettingElement: XElement {
		public static readonly XName ElementName = "setting";
		private static readonly XName ChangedAtName = "changedat";
		private static readonly XName CommonName = "common";
		private static readonly XName CompressedName = "compressed";
		private static readonly XName ResetAtName = "resetat";
		private static readonly XName TypeName = "type";
		private static readonly XName VersionName = "version";

		public ViewUiSettingElement(): base(ElementName) { }

		public ViewUiSettingElement(XElement node): base(node) { }

		public DateTime ChangedAt {
			get => ElementAttribute<DateTime>.Get(this, ChangedAtName);
			set => ElementAttribute<DateTime>.Set(this, ChangedAtName, value);
		}

		public bool Common {
			get => ElementAttribute<bool>.Get(this, CommonName);
			set => ElementAttribute<bool>.Set(this, CommonName, value);
		}

		public bool Compressed {
			get => ElementAttribute<bool>.Get(this, CompressedName);
			set => ElementAttribute<bool>.Set(this, CompressedName, value);
		}

		public DateTime ResetAt {
			get => ElementAttribute<DateTime>.Get(this, ResetAtName);
			set => ElementAttribute<DateTime>.Set(this, ResetAtName, value);
		}

		public int Type {
			get => ElementAttribute<int>.Get(this, TypeName);
			set => ElementAttribute<int>.Set(this, TypeName, value);
		}

		public int Version {
			get => ElementAttribute<int>.Get(this, VersionName);
			set => ElementAttribute<int>.Set(this, VersionName, value);
		}
	}
}
