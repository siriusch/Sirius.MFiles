namespace Sirius.VAF.VaultDom.Structure {
	public interface IFileReference {
		public string ContentType {
			get;
			set;
		}

		public string PathFromBase {
			get;
			set;
		}
	}
}
