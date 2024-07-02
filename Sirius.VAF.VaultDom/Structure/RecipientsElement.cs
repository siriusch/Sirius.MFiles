using System.Xml.Linq;

using JetBrains.Annotations;

namespace Sirius.VAF.VaultDom.Structure {
	public class RecipientsElement: AssignmentSubjectsElementBase {
		public static readonly XName ElementName = "recipients";

		public RecipientsElement(): base(ElementName) { }

		public RecipientsElement([NotNull] XElement other): base(other) { }
	}
}
