using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using MFilesAPI;

namespace Sirius.VAF.VaultDom.Structure {
	public class CriteriaFulfilledElement: ActivableElementBase<MFAutoStateTransitionMode> {
		public static readonly XName ElementName = "CriteriaFulfilled";
		private static readonly XName SearchCondElementName = "searchcond";

		public CriteriaFulfilledElement(): base(ElementName) { }

		public CriteriaFulfilledElement(XElement node): base(node) { }

		public override MFAutoStateTransitionMode Type => MFAutoStateTransitionMode.MFASTModeCriteriaFulfilled;

		public IEnumerable<SearchCondElement> SearchConds => Elements(SearchCondElementName).Cast<SearchCondElement>();
	}
}
