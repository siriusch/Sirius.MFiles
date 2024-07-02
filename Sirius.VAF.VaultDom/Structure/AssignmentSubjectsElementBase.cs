using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using JetBrains.Annotations;

using Sirius.VAF.VaultDom.Prologue;

namespace Sirius.VAF.VaultDom.Structure {
	public abstract class AssignmentSubjectsElementBase: XElement {
		internal static readonly XName PseudoUserElementsName = "pseudouserelements";

		protected AssignmentSubjectsElementBase([NotNull] XName name): base(name) { }

		protected AssignmentSubjectsElementBase([NotNull] XElement other): base(other) { }

		public IEnumerable<UserReferenceElement> Users => Elements().OfType<UserReferenceElement>();

		public IEnumerable<GroupReferenceElement> Groups => Elements().OfType<GroupReferenceElement>();

		public IEnumerable<PseudoUserElement> PseudoUsers => Elements(PseudoUserElementsName).Elements().OfType<PseudoUserElement>();
	}
}
