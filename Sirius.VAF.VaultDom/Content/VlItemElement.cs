using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Archive;
using Sirius.VAF.VaultDom.Structure;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Content {
	public class VlItemElement: XElement {
		public static readonly XName ElementName = "vlitem";
		private static readonly XName ChangedAtName = "changedat";
		private static readonly XName DeletedName = "deleted";
		private static readonly XName DeletedAtName = "deletedat";
		private static readonly XName ExtIdStatusName = "extidstatus";
		private static readonly XName GuidName = "guid";
		private static readonly XName IdName = "id";
		private static readonly XName OtIdName = "otid";
		private static readonly XName OtNameName = "otname";
		private static readonly XName ValueName = "value";

		public VlItemElement(): base(ElementName) { }

		public VlItemElement(XElement node): base(node) { }

		public DateTime ChangedAt {
			get => ElementAttribute<DateTime>.Get(this, ChangedAtName);
			set => ElementAttribute<DateTime>.Set(this, ChangedAtName, value);
		}

		public bool Deleted {
			get => ElementAttribute<bool>.Get(this, DeletedName);
			set => ElementAttribute<bool>.Set(this, DeletedName, value);
		}

		public DateTime DeletedAt {
			get => ElementAttribute<DateTime>.Get(this, DeletedAtName);
			set => ElementAttribute<DateTime>.Set(this, DeletedAtName, value);
		}

		public string ExtIdStatus {
			get => ElementAttribute<string>.Get(this, ExtIdStatusName);
			set => ElementAttribute<string>.Set(this, ExtIdStatusName, value);
		}

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, GuidName);
			set => ElementAttribute<Guid>.Set(this, GuidName, value, ArchiveDocument.StringifyGuid);
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

		public ParentReferenceElement ParentReference {
			get => (ParentReferenceElement)Element(ParentReferenceElement.ElementName);
			set => this.SetElement(ParentReferenceElement.ElementName, value);
		}

		public AclReferenceElement Acl {
			get => (AclReferenceElement)Element(AclReferenceElement.ElementName);
			set => this.SetElement(AclReferenceElement.ElementName, value);
		}

		public OriginalIdentityElement OriginalIdentity {
			get => (OriginalIdentityElement)Element(OriginalIdentityElement.ElementName);
			set => this.SetElement(OriginalIdentityElement.ElementName, value);
		}
	}
}
