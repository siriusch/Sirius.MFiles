using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Prologue {
	public class AceRightElement: XElement {
		public static readonly XName ReadElementName = "read";
		public static readonly XName EditElementName = "edit";
		public static readonly XName ChangePermissionsElementName = "changepermissions";
		public static readonly XName DeleteElementName = "delete";
		public static readonly XName SeeElementName = "see";
		public static readonly XName AttachObjectsElementName = "attachobjects";
		public static readonly XName MoveInElementName = "movein";
		public static readonly XName MoveOutElementName = "moveout";
		public static readonly XName TransitionElementName = "transition";
		public static readonly XName SeeNameElementName = "seename";
		public static readonly XName AddValuesElementName = "addvalues";
		private static readonly XName PosName = "pos";
		private static readonly XName ValueName = "value";

		public AceRightElement(XName name): base(name) { }

		public AceRightElement(XElement node): base(node) { }

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public new AceRightValue Value {
			get => ElementAttribute<AceRightValue>.GetOrDefault(this, ValueName);
			set => ElementAttribute<AceRightValue>.Set(this, ValueName, value);
		}
	}
}
