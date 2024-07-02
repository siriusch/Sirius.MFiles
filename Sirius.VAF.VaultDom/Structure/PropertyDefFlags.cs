using System;

namespace Sirius.VAF.VaultDom.Structure {
	[Flags]
	public enum PropertyDefFlags {
		None = 0,
		ObjectsSearchable = 1,
		HistoryVersionsOfObjectsSearchable = 2
	}
}
