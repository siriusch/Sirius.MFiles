using System;
using System.Xml.Linq;

namespace Sirius.VAF.VaultDom {
	public interface IElementInfoProvider {
		bool TryGetElementInfo(XName name, out (Type Type, Func<XElement, XElement> Clone) elementInfo);
	}
}
