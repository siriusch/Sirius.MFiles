using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.MFiles.VafUtil.Vault {
	public class ArchiveDocument: XDocument {
		private static readonly TypedXElementRegistry registry = CreateRegistry();

		private static TypedXElementRegistry CreateRegistry() {
			var result = new TypedXElementRegistry();
			result.RegisterRootElement<Archive>(Archive.Name);
			result.RegisterElement<Archive, ArchiveFeatures>(ArchiveFeatures.Name);
			result.RegisterElement<ArchiveFeatures, ArchiveFeature>(ArchiveFeature.Name);
			result.RegisterElement<Archive, ArchiveTimestamp>(ArchiveTimestamp.Name);
			result.RegisterElement<Archive, ArchiveDelta>(ArchiveDelta.Name);
			result.RegisterElement<Archive, ArchiveVault>(ArchiveVault.Name);
			result.RegisterElement<Archive, ArchiveStatistics>(ArchiveStatistics.Name);
			result.RegisterElement<Archive, ArchiveFlags>(ArchiveFlags.Name);
			result.RegisterElement<ArchiveFlags, ArchiveFlag>(ArchiveFlag.Name);
			return result;
		}

		public ArchiveDocument(object content) {
			registry.ApplyToDocument(this);
			Add(content);
		}

		public Archive Archive => (Archive)Root;
	}
}
