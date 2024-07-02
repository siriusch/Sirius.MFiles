using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class ExpressionExFlagsElement: FlagsElementBase<MFExpressionExFlag> {
		public static readonly XName ElementName = "flags";

		public ExpressionExFlagsElement(): base(ElementName) { }

		public ExpressionExFlagsElement(XElement node): base(node) { }

		protected override string Prefix => "MFExpressionEx";
	}
}
