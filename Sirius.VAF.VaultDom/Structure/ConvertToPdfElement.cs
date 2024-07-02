using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ConvertToPdfElement: ActivableElementBase<StateAction> {
		public static readonly XName ElementName = "converttopdf";
		private static readonly XName FailOnUnsupportedName = "failonunsupported";
		private static readonly XName OverwriteName = "overwrite";
		private static readonly XName PdfA1BName = "pdfa1b";
		private static readonly XName SeparateFileName = "separatefile";

		public ConvertToPdfElement(): base(ElementName) { }

		public ConvertToPdfElement(XElement node): base(node) { }

		public bool FailOnUnsupported {
			get => ElementAttribute<bool>.Get(this, FailOnUnsupportedName);
			set => ElementAttribute<bool>.Set(this, FailOnUnsupportedName, value);
		}

		public bool Overwrite {
			get => ElementAttribute<bool>.Get(this, OverwriteName);
			set => ElementAttribute<bool>.Set(this, OverwriteName, value);
		}

		public bool PdfA1B {
			get => ElementAttribute<bool>.Get(this, PdfA1BName);
			set => ElementAttribute<bool>.Set(this, PdfA1BName, value);
		}

		public bool SeparateFile {
			get => ElementAttribute<bool>.Get(this, SeparateFileName);
			set => ElementAttribute<bool>.Set(this, SeparateFileName, value);
		}

		public override StateAction Type => StateAction.ConvertToPdf;
	}
}
