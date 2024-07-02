using System.Xml.Linq;

using JetBrains.Annotations;

namespace Sirius.VAF.VaultDom.Structure {
	public class MonitoredByElement: AssignmentSubjectsElementBase {
		public static readonly XName ElementName = "monitoredby";

		public MonitoredByElement(): base(ElementName) { }

		public MonitoredByElement([NotNull] XElement other): base(other) { }
	}
}
