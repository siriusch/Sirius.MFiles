using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveTimestamp: XElement {
		public new static readonly XName Name = "timestamp";

		public ArchiveTimestamp(): base(Name) { }

		public ArchiveTimestamp(XElement other): base(other) { }
	}
}
