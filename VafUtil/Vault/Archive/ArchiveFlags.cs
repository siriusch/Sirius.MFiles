using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveFlags: XElement {
		public new static readonly XName Name = "flags";

		public ArchiveFlags(): base(Name) { }

		public ArchiveFlags(XElement other): base(other) { }
	}
}
