using System;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom {
	public abstract class ActivableElementBase<TEnum>: XElement, IActivable<TEnum> where TEnum: Enum {
		private static readonly XName ActiveName = "active";

		protected ActivableElementBase(XName name): base(name) { }

		protected ActivableElementBase(XElement node): base(node) { }

		public abstract TEnum Type {
			get;
		}

		public bool Active {
			get => ElementAttribute<bool>.Get(this, ActiveName);
			set {
				if (ElementAttribute<bool>.Set(this, ActiveName, value)) {
					((INotifyActiveChange<TEnum>)Parent)?.ActiveChanged(this);
				}
			}
		}
	}
}
