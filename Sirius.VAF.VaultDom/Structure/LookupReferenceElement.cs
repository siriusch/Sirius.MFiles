using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class LookupReferenceElement: XElement {
		public static readonly XName ElementName = "lookup";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName ExtIdStatusName = "extidstatus";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName OtIdName = "otid";
		private static readonly XName OtNameName = "otname";
		private static readonly XName ValueName = "value";
		private static readonly XName VersionName = "version";

		public LookupReferenceElement(): base(ElementName) { }

		public LookupReferenceElement(XElement node): base(node) { }

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public string ExtIdStatus {
			get => ElementAttribute<string>.Get(this, ExtIdStatusName);
			set => ElementAttribute<string>.Set(this, ExtIdStatusName, value);
		}

		public string Guid {
			get => ElementAttribute<string>.Get(this, GuidName);
			set => ElementAttribute<string>.Set(this, GuidName, value);
		}

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public int OtId {
			get => ElementAttribute<int>.Get(this, OtIdName);
			set => ElementAttribute<int>.Set(this, OtIdName, value);
		}

		public string OtName {
			get => ElementAttribute<string>.Get(this, OtNameName);
			set => ElementAttribute<string>.Set(this, OtNameName, value);
		}

		public new string Value {
			get => ElementAttribute<string>.Get(this, ValueName);
			set => ElementAttribute<string>.Set(this, ValueName, value);
		}

		public int Version {
			get => ElementAttribute<int>.Get(this, VersionName);
			set => ElementAttribute<int>.Set(this, VersionName, value);
		}
	}
}
