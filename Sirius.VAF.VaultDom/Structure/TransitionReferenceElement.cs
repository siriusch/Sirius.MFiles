using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class TransitionReferenceElement: XElement {
		public static readonly XName ElementName = "transition";
		private static readonly XName EvalOrderName = "evalorder";
		private static readonly XName FromStateName = "fromstate";
		private static readonly XName IdName = "id";
		private static readonly XName NameName = "name";
		private static readonly XName ToStateName = "tostate";

		public TransitionReferenceElement(): base(ElementName) { }

		public TransitionReferenceElement(XElement node): base(node) { }

		public int EvalOrder {
			get => ElementAttribute<int>.Get(this, EvalOrderName);
			set => ElementAttribute<int>.Set(this, EvalOrderName, value);
		}

		public int FromState {
			get => ElementAttribute<int>.Get(this, FromStateName);
			set => ElementAttribute<int>.Set(this, FromStateName, value);
		}

		public int Id {
			get => ElementAttribute<int>.Get(this, IdName);
			set => ElementAttribute<int>.Set(this, IdName, value);
		}

		public new string Name {
			get => ElementAttribute<string>.Get(this, NameName);
			set => ElementAttribute<string>.Set(this, NameName, value);
		}

		public int ToState {
			get => ElementAttribute<int>.Get(this, ToStateName);
			set => ElementAttribute<int>.Set(this, ToStateName, value);
		}
	}
}
