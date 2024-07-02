using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public class FileValueExprElement: ExpressionElementBase {
		private static readonly Mapping<MFFileValueType> TypeMapping = new() {
				{ "HasFiles", MFFileValueType.MFFileValueTypeHasFiles },
				{ "FileName", MFFileValueType.MFFileValueTypeFileName },
				{ "FileSize", MFFileValueType.MFFileValueTypeFileSize },
				{ "FileID", MFFileValueType.MFFileValueTypeFileID },
				{ "CreationTime", MFFileValueType.MFFileValueTypeCreationTime },
				{ "ChangeTime", MFFileValueType.MFFileValueTypeChangeTime },
				{ "LinkedToExtLoc", MFFileValueType.MFFileValueTypeLinkedToExtLoc },
				{ "LinkedToExtLocID", MFFileValueType.MFFileValueTypeLinkedToExtLocID },
				{ "DuplicatesOnly", MFFileValueType.MFFileValueTypeDuplicatesOnly }
		};

		public static readonly XName ElementName = "FileValueExpr";
		private static readonly XName TypeName = "type";

		public FileValueExprElement(): base(ElementName) { }

		public FileValueExprElement(XElement node): base(node) { }

		public MFFileValueType Type {
			get => ElementAttribute<MFFileValueType>.Get(this, TypeName, TypeMapping.Parse);
			set => ElementAttribute<MFFileValueType>.Set(this, TypeName, value, TypeMapping.Stringify);
		}

		public override MFExpressionType ExpressionType => MFExpressionType.MFExpressionTypeFileValue;
	}
}
