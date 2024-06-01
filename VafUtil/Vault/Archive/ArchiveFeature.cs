using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveFeature: XElement {
		public new static readonly XName Name = "feature";

		public ArchiveFeature(): base(Name) { }

		public ArchiveFeature(XElement other): base(other) { }
	}
}
