using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewAllowedOperationsElement: XElement {
		public static readonly XName ElementName = "allowedoperations";
		private static readonly XName CopyInUiName = "copyinui";
		private static readonly XName CreateChildViewsInUiName = "createchildviewsinui";
		private static readonly XName DeleteName = "delete";
		private static readonly XName EditName = "edit";

		public ViewAllowedOperationsElement(): base(ElementName) { }

		public ViewAllowedOperationsElement(XElement node): base(node) { }

		public bool CopyInUi {
			get => ElementAttribute<bool>.Get(this, CopyInUiName);
			set => ElementAttribute<bool>.Set(this, CopyInUiName, value);
		}

		public bool CreateChildViewsInUi {
			get => ElementAttribute<bool>.Get(this, CreateChildViewsInUiName);
			set => ElementAttribute<bool>.Set(this, CreateChildViewsInUiName, value);
		}

		public bool Delete {
			get => ElementAttribute<bool>.Get(this, DeleteName);
			set => ElementAttribute<bool>.Set(this, DeleteName, value);
		}

		public bool Edit {
			get => ElementAttribute<bool>.Get(this, EditName);
			set => ElementAttribute<bool>.Set(this, EditName, value);
		}
	}
}
