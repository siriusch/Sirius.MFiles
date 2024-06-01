using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveFeatures: XElement {
		public new static readonly XName Name = "features";

		public ArchiveFeatures(): base(Name) { }

		public ArchiveFeatures(XElement other): base(other) { }
	}
}
