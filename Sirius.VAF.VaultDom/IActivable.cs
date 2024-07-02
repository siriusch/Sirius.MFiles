using System;

namespace Sirius.VAF.VaultDom {
	internal interface IActivable<TEnum> where TEnum: Enum {
		TEnum Type {
			get;
		}

		bool Active {
			get;
			set;
		}
	}
}
