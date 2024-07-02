using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure.Expressions {
	public class TypedValueExprElement: ExpressionElementBase {
		private static readonly Mapping<MFDataType> TypeMapping = new() {
				{ "Text", MFDataType.MFDatatypeText },
				{ "Integer", MFDataType.MFDatatypeInteger },
				{ "Floating", MFDataType.MFDatatypeFloating },
				{ "Date", MFDataType.MFDatatypeDate },
				{ "Time", MFDataType.MFDatatypeTime },
				{ "Timestamp", MFDataType.MFDatatypeTimestamp },
				{ "Boolean", MFDataType.MFDatatypeBoolean },
				{ "Lookup", MFDataType.MFDatatypeLookup },
				{ "MultiSelectLookup", MFDataType.MFDatatypeMultiSelectLookup },
				{ "Integer64", MFDataType.MFDatatypeInteger64 },
				{ "FILETIME", MFDataType.MFDatatypeFILETIME },
				{ "MultiLineText", MFDataType.MFDatatypeMultiLineText },
				{ "ACL", MFDataType.MFDatatypeACL }
		};

		public static readonly XName ElementName = "TypedValueExpr";
		private static readonly XName OtIdName = "otid";
		private static readonly XName TypeName = "type";
		private static readonly XName ParentChildBehaviorName = "parentchildbehavior";

		public TypedValueExprElement(): base(ElementName) { }

		public TypedValueExprElement(XElement node): base(node) { }

		public int OtId {
			get => ElementAttribute<int>.Get(this, OtIdName);
			set => ElementAttribute<int>.Set(this, OtIdName, value);
		}

		public MFDataType Type {
			get => ElementAttribute<MFDataType>.Get(this, TypeName, TypeMapping.Parse);
			set => ElementAttribute<MFDataType>.Set(this, TypeName, value, TypeMapping.Stringify);
		}

		public override MFExpressionType ExpressionType => MFExpressionType.MFExpressionTypeTypedValue;

		public ParentChildBehavior ParentChildBehavior {
			get => ElementAttribute<ParentChildBehavior>.Get(this, ParentChildBehaviorName);
			set => ElementAttribute<ParentChildBehavior>.Set(this, ParentChildBehaviorName, value);
		}
	}
}
