using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class ViewLocationElement: XElement {
		public static readonly XName ElementName = "viewlocation";
		internal static readonly XName ParentFoldersName = "parentfolders";

		public ViewLocationElement(): base(ElementName) { }

		public ViewLocationElement(XElement node): base(node) { }

		public CollectionElement<FolderReferenceElement> ParentFolders {
			get => (CollectionElement<FolderReferenceElement>)Element(ParentFoldersName);
			set => this.SetElement(ParentFoldersName, value);
		}

		public ViewOverlappingElement Overlapping {
			get => (ViewOverlappingElement)Element(ViewOverlappingElement.ElementName);
			set => this.SetElement(ViewOverlappingElement.ElementName, value);
		}
	}
}
