using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sirius.MFiles.VafUtil.Vault {
	public interface IElementInfoProvider {
		bool TryGetElementInfo(XName name, out (Type Type, Func<XElement, XElement> Clone) elementInfo);
	}
}
