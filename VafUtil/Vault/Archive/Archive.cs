using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class Archive: XElement {
		public new static readonly XName Name = "archive";

		public Archive(): base(Name) { }

		public Archive(XElement other): base(other) { }
	}
}
