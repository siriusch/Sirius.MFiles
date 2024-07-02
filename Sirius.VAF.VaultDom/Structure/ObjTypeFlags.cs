using System;

namespace Sirius.VAF.VaultDom.Structure {
	[Flags]
	public enum ObjTypeFlags {
		None = 0,
		CanHaveFiles = 1,
		AllowAddingNewObjects = 2,
		External = 4,
		ShowCreationCommand = 8,
		DisallowAsGroupingLevel = 128
	}
}
