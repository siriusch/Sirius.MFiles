using System;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Structure;
using Sirius.XML;

namespace Sirius.VAF.VaultDom.Archive {
	public class ArchiveElement: XElement {
		public static readonly XName ElementName = "archive";
		internal static readonly XName FeaturesName = "features";
		private static readonly XName GuidName = "guid";
		private static readonly XName VersionName = "version";
		private static readonly XName BuildName = "build";
		private static readonly XName OldestCompatibleBuildName = "oldestcompatiblebuild";

		public ArchiveElement(): base(ElementName) { }

		public ArchiveElement(XElement node): base(node) { }

		public Guid Guid {
			get => ElementAttribute<Guid>.Get(this, GuidName);
			set => ElementAttribute<Guid>.Set(this, GuidName, value, ArchiveDocument.StringifyGuid);
		}

		public int Version {
			get => ElementAttribute<int>.Get(this, VersionName);
			set => ElementAttribute<int>.Set(this, VersionName, value);
		}

		public Version Build {
			get => ElementAttribute<Version>.Get(this, BuildName);
			set => ElementAttribute<Version>.Set(this, BuildName, value);
		}

		public Version OldestCompatibleBuild {
			get => ElementAttribute<Version>.Get(this, OldestCompatibleBuildName);
			set => ElementAttribute<Version>.Set(this, OldestCompatibleBuildName, value);
		}

		public CollectionElement<FeatureElement> Features {
			get => (CollectionElement<FeatureElement>)Element(FeaturesName);
			set => this.SetElement(FeaturesName, value);
		}

		public TimestampElement Timestamp {
			get => (TimestampElement)Element(TimestampElement.ElementName);
			set => this.SetElement(TimestampElement.ElementName, value);
		}

		public DeltaElement Delta {
			get => (DeltaElement)Element(DeltaElement.ElementName);
			set => this.SetElement(DeltaElement.ElementName, value);
		}

		public VaultElement Vault {
			get => (VaultElement)Element(VaultElement.ElementName);
			set => this.SetElement(VaultElement.ElementName, value);
		}

		public StatisticsElement Statistics {
			get => (StatisticsElement)Element(StatisticsElement.ElementName);
			set => this.SetElement(StatisticsElement.ElementName, value);
		}

		public ArchiveFlagsElement Flags {
			get => (ArchiveFlagsElement)Element(ArchiveFlagsElement.ElementName);
			set => this.SetElement(ArchiveFlagsElement.ElementName, value);
		}

		public StructureElement Structure {
			get => (StructureElement)Element(StructureElement.ElementName);
			set => this.SetElement(StructureElement.ElementName, value);
		}
	}
}
