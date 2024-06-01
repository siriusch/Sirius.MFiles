using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveDelta: XElement {
		public new static readonly XName Name = "delta";

		public ArchiveDelta(): base(Name) { }

		public ArchiveDelta(XElement other): base(other) { }
	}
}
