using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class RelativeTimeElement: ActivableElementBase<MFAutoStateTransitionMode> {
		public static readonly XName ElementName = "RelativeTime";
		private static readonly XName ValueName = "value";

		public RelativeTimeElement(): base(ElementName) { }

		public RelativeTimeElement(XElement node): base(node) { }

		public override MFAutoStateTransitionMode Type => MFAutoStateTransitionMode.MFASTModeRelativeTime;

		public new int Value {
			get => ElementAttribute<int>.Get(this, ValueName);
			set => ElementAttribute<int>.Set(this, ValueName, value);
		}
	}
}
