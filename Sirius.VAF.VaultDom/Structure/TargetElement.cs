using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class TargetElement: XElement {
		public static readonly XName ElementName = "target";
		private static readonly XName OtNameName = "otname";
		private static readonly XName OtIdName = "otid";

		public TargetElement(): base(ElementName) { }

		public TargetElement(XElement node): base(node) { }

		public int OtId {
			get => ElementAttribute<int>.Get(this, OtIdName);
			set => ElementAttribute<int>.Set(this, OtIdName, value);
		}

		public string OtName {
			get => ElementAttribute<string>.Get(this, OtNameName);
			set => ElementAttribute<string>.Set(this, OtNameName, value);
		}
	}
}
