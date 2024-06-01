using System;
using System.Xml;
using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveVault: XElement {
		public new static readonly XName Name = "vault";

		public ArchiveVault(): base(Name) { }

		public ArchiveVault(XElement other): base(other) { }

		public Guid Guid {
			get => XmlConvert.ToGuid(this.Attribute("guid").Value);
			set => this.Attribute("guid").Value = XmlConvert.ToString(value);
		}
	}
}
