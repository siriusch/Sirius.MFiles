using System;

namespace Sirius.VAF.VaultDom.Structure {
	[Flags]
	public enum CondsFlags {
		None = 0,
		Deleted = 1,
		ValidAccessedBy = 2
	}
}
