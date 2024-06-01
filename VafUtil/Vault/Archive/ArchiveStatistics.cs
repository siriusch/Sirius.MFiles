using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveStatistics: XElement {
		public new static readonly XName Name = "statistics";

		public ArchiveStatistics(): base(Name) { }

		public ArchiveStatistics(XElement other): base(other) { }
	}
}
