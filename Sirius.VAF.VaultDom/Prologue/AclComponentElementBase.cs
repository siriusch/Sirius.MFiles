using System;
using System.Xml.Linq;

namespace Sirius.VAF.VaultDom.Prologue {
	public abstract class AclComponentElementBase: XElement {
		protected AclComponentElementBase(XName name): base(name) { }

		protected AclComponentElementBase(XElement node): base(node) { }
	}
}
