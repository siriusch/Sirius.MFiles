using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.DataTypes {
	public abstract class LookupElementBase: DataTypeElementBase {
		internal static readonly XName StaticFilterName = "staticfilter";
		private static readonly XName AllowAutomaticPermissionsName = "allowautomaticpermissions";
		private static readonly XName IsConflictName = "isconflict";
		private static readonly XName IsDefaultName = "isdefault";
		private static readonly XName IsOwnerName = "isowner";
		private static readonly XName NameName = "name";
		private static readonly XName OtIdName = "otid";

		protected LookupElementBase(XName name): base(name) { }

		protected LookupElementBase(XElement node): base(node) { }

		public bool AllowAutomaticPermissions {
			get => ElementAttribute<bool>.Get(this, AllowAutomaticPermissionsName);
			set => ElementAttribute<bool>.Set(this, AllowAutomaticPermissionsName, value);
		}

		public bool IsConflict {
			get => ElementAttribute<bool>.Get(this, IsConflictName);
			set => ElementAttribute<bool>.Set(this, IsConflictName, value);
		}

		public bool IsDefault {
			get => ElementAttribute<bool>.Get(this, IsDefaultName);
			set => ElementAttribute<bool>.Set(this, IsDefaultName, value);
		}

		public bool IsOwner {
			get => ElementAttribute<bool>.Get(this, IsOwnerName);
			set => ElementAttribute<bool>.Set(this, IsOwnerName, value);
		}

		public new string Name {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}

		public int OtId {
			get => ElementAttribute<int>.Get(this, OtIdName);
			set => ElementAttribute<int>.Set(this, OtIdName, value);
		}

		public SortingElement Sorting {
			get => (SortingElement)Element(SortingElement.ElementName);
			set => this.SetElement(SortingElement.ElementName, value);
		}

		public OwnerPdElement OwnerPd {
			get => (OwnerPdElement)Element(OwnerPdElement.ElementName);
			set => this.SetElement(OwnerPdElement.ElementName, value);
		}

		public CollectionElement<SearchCondElement> StaticFilter {
			get => (CollectionElement<SearchCondElement>)Element(StaticFilterName);
			set => this.SetElement(StaticFilterName, value);
		}
	}
}
