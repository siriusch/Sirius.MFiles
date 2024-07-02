using System.Xml.Linq;

using JetBrains.Annotations;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class AssignedToElement: AssignmentSubjectsElementBase {
		public static readonly XName ElementName = "assignedto";

		public AssignedToElement(): base(ElementName) { }

		public AssignedToElement([NotNull] XElement other): base(other) { }
	}
}
