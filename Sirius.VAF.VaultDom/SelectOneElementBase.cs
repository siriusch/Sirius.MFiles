using System;
using System.Linq;
using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom {
	public abstract class SelectOneElementBase<TEnum>: XElement, INotifyActiveChange<TEnum> where TEnum: Enum {
		private static readonly XName TypeName = "type";

		protected SelectOneElementBase(XName name): base(name) { }

		protected SelectOneElementBase(XElement node): base(node) { }

		protected virtual TEnum ParseType(string str) {
			return ElementAttribute<TEnum>.Parse(str);
		}

		protected virtual string StringifyType(TEnum value) {
			return ElementAttribute<TEnum>.Stringify(value);
		}

		public TEnum Type {
			get => ElementAttribute<TEnum>.GetOrDefault(this, TypeName, default, ParseType);
			set {
				if (ElementAttribute<TEnum>.Set(this, TypeName, value, StringifyType)) {
					var elem = Elements().OfType<IActivable<TEnum>>().SingleOrDefault(e => System.Collections.Generic.EqualityComparer<TEnum>.Default.Equals(e.Type, value));
					if (elem != null) {
						elem.Active = true;
					}
				}
			}
		}

		void INotifyActiveChange<TEnum>.ActiveChanged(IActivable<TEnum> sender) {
			if (sender.Active) {
				ElementAttribute<TEnum>.Set(this, TypeName, sender.Type);
				foreach (var elementBase in Elements().OfType<IActivable<TEnum>>()) {
					if (elementBase != sender) {
						elementBase.Active = false;
					}
				}
			} else if (System.Collections.Generic.EqualityComparer<TEnum>.Default.Equals(sender.Type, Type)) {
				Type = (TEnum)Enum.ToObject(typeof(TEnum), 0);
			}
		}
	}
}
