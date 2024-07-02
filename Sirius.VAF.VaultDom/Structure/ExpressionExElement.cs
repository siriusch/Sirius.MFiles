using System.Xml.Linq;

using MFilesAPI;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ExpressionExElement: XElement {
		private static readonly Mapping<MFFolderListingAlgorithm> AlgorithmMapping = new() {
				{ "None", MFFolderListingAlgorithm.MFFolderListingAlgorithmNone },
				{ "TestEachValue", MFFolderListingAlgorithm.MFFolderListingAlgorithmTestEachValue },
				{ "GetValuesByDistinctUse", MFFolderListingAlgorithm.MFFolderListingAlgorithmGetValuesByDistinctUse },
				{ "TestEachValueWithSeparateQueries", MFFolderListingAlgorithm.MFFolderListingAlgorithmTestEachValueWithSeparateQueries }
		};

		public static readonly XName ElementName = "level";
		private static readonly XName AlgorithmName = "algorithm";
		private static readonly XName NullFolderNameName = "nullfoldername";
		private static readonly XName ObjectTypeToGroupByName = "objecttypetogroupby";
		private static readonly XName PosName = "pos";
		private static readonly XName ReferFromViewLevelName = "referfromviewlevel";
		private static readonly XName ShowEmptyName = "showempty";
		private static readonly XName ShowJitName = "showjit";
		private static readonly XName ShowMatchingObjectsName = "showmatchingobjects";
		private static readonly XName ShowNullName = "shownull";
		private static readonly XName ShowNullContentsName = "shownullcontents";
		private static readonly XName SearchCondsName = "searchconds";

		public ExpressionExElement(): base(ElementName) { }

		public ExpressionExElement(XName name): base(name) { }

		public ExpressionExElement(XElement node): base(node) { }

		public MFFolderListingAlgorithm Algorithm {
			get => ElementAttribute<MFFolderListingAlgorithm>.Get(this, AlgorithmName, AlgorithmMapping.Parse);
			set => ElementAttribute<MFFolderListingAlgorithm>.Set(this, AlgorithmName, value, AlgorithmMapping.Stringify);
		}

		public string NullFolderName {
			get => ElementAttribute<string>.Get(this, NullFolderNameName);
			set => ElementAttribute<string>.Set(this, NullFolderNameName, value);
		}

		public int ObjectTypeToGroupBy {
			get => ElementAttribute<int>.Get(this, ObjectTypeToGroupByName);
			set => ElementAttribute<int>.Set(this, ObjectTypeToGroupByName, value);
		}

		public int Pos {
			get => ElementAttribute<int>.Get(this, PosName);
			set => ElementAttribute<int>.Set(this, PosName, value);
		}

		public bool ReferFromViewLevel {
			get => ElementAttribute<bool>.Get(this, ReferFromViewLevelName);
			set => ElementAttribute<bool>.Set(this, ReferFromViewLevelName, value);
		}

		public bool ShowEmpty {
			get => ElementAttribute<bool>.Get(this, ShowEmptyName);
			set => ElementAttribute<bool>.Set(this, ShowEmptyName, value);
		}

		public bool ShowJit {
			get => ElementAttribute<bool>.Get(this, ShowJitName);
			set => ElementAttribute<bool>.Set(this, ShowJitName, value);
		}

		public bool ShowMatchingObjects {
			get => ElementAttribute<bool>.Get(this, ShowMatchingObjectsName);
			set => ElementAttribute<bool>.Set(this, ShowMatchingObjectsName, value);
		}

		public bool ShowNull {
			get => ElementAttribute<bool>.Get(this, ShowNullName);
			set => ElementAttribute<bool>.Set(this, ShowNullName, value);
		}

		public bool ShowNullContents {
			get => ElementAttribute<bool>.Get(this, ShowNullContentsName);
			set => ElementAttribute<bool>.Set(this, ShowNullContentsName, value);
		}

		public CollectionElement<SearchCondElement> SearchConds {
			get => (CollectionElement<SearchCondElement>)Element(SearchCondsName);
			set => this.SetElement(SearchCondsName, value);
		}

		public ExpressionElement Expression {
			get => (ExpressionElement)Element(ExpressionElement.ElementName);
			set => this.SetElement(ExpressionElement.ElementName, value);
		}

		public ExpressionExFlagsElement Flags {
			get => (ExpressionExFlagsElement)Element(ExpressionExFlagsElement.ElementName);
			set => this.SetElement(ExpressionExFlagsElement.ElementName, value);
		}
	}
}
