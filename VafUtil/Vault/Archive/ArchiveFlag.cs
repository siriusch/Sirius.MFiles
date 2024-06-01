using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveFlag: XElement {
		public new static readonly XName Name = "flag";

		public ArchiveFlag(): base(Name) { }

		public ArchiveFlag(XElement other): base(other) { }
	}
}
