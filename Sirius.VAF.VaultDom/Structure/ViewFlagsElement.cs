using System;
using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewFlagsElement: FlagsElementBase<MFViewFlag> {
		public static readonly XName ElementName = "flags";

		public ViewFlagsElement(): base(ElementName) { }

		public ViewFlagsElement(XElement node): base(node) { }

		protected override string Prefix => nameof(MFViewFlag);
	}
}
