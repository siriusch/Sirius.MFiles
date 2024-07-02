using System;

namespace Sirius.VAF.VaultDom {
	internal interface INotifyActiveChange<TEnum> where TEnum: Enum {
		void ActiveChanged(IActivable<TEnum> sender);
	}
}
