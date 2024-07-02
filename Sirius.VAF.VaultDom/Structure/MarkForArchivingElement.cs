using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Structure {
	public class MarkForArchivingElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "markforarchiving";

		public MarkForArchivingElement(): base(ElementName) { }

		public MarkForArchivingElement(XElement node): base(node) { }

		public override StateAction Type => StateAction.MarkForArchiving;
	}
}
