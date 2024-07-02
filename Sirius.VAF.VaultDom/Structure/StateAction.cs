using System;

namespace Sirius.VAF.VaultDom.Structure {
	[Flags]
	public enum StateAction {
		None = 0,
		ChangePermissions = 1,
		Delete = 2,
		MarkForArchiving = 4,
		CreateAssignmewnt = 8,
		SendNotification = 16,
		SetProperties = 32,
		RunScript = 64,
		CreateSeparateAssignment = 128,
		ConvertToPdf = 256
	}
}
