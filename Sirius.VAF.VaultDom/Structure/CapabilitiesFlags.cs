using System;

namespace Sirius.VAF.VaultDom.Structure {
	[Flags]
	public enum CapabilitiesFlags {
		None = 0,
		DescribeContent = 8,
		MapSemantics = 16,
		DescribeStructure = 512
	}
}
