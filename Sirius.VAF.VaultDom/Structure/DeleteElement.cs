using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class DeleteElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "delete";

		public DeleteElement(): base(ElementName) { }

		public DeleteElement(XElement node): base(node) { }

		public override StateAction Type => StateAction.Delete;
	}
}
