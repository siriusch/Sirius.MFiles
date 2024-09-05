using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Sirius.VAF.VaultDom.Prologue;
using Sirius.VAF.VaultDom.Structure;

namespace Sirius.VAF.VaultDom {
	public static class Extensions {
		public static AclDefinitionElement GetAcl<T>(this T that) where T: XElement, IElementWithAcl {
			var cachedId = that.Acl?.Cached?.Id;
			if (string.IsNullOrEmpty(cachedId)) {
				return null;
			}
			return that
					.Ancestors(PrologueElement.AclCacheName)
					.Elements()
					.OfType<AclDefinitionElement>()
					.SingleOrDefault(acl => acl.Id == cachedId);
		}

		public static Stream OpenRead<T>(this T that, XmlResolver resolver) where T: IFileReference {
			if (string.IsNullOrEmpty(that.PathFromBase)) {
				return null;
			}
			return (Stream)resolver.GetEntity(new Uri(that.PathFromBase, UriKind.Relative), null, typeof(Stream));
		}

		public static Stream OpenRead<T>(this T that, DirectoryInfo rootDirectory) where T: IFileReference {
			if (string.IsNullOrEmpty(that.PathFromBase)) {
				return null;
			}
			var fullPath = Path.GetFullPath(Path.Combine(rootDirectory.FullName, that.PathFromBase));
			if (!fullPath.StartsWith(rootDirectory.FullName)) {
				throw new InvalidOperationException("The file path is invalid");
			}
			return File.OpenRead(fullPath);
		}
	}
}
