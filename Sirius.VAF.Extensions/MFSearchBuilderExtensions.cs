using System.Collections.Generic;

using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class MFSearchBuilderExtensions {
		public static MFSearchBuilder ObjectIds(this MFSearchBuilder that, IEnumerable<int> items) {
			var lookups = new Lookups();
			foreach (var item in items) {
				lookups.Add(-1, new LookupClass {
						Item = item
				});
			}
			return ObjectIds(that, lookups);
		}

		public static MFSearchBuilder ObjectIds(this MFSearchBuilder that, Lookups items) {
			var condition = new SearchCondition();
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID);
			condition.ConditionType = MFConditionType.MFConditionTypeEqual;
			condition.TypedValue.SetValueToMultiSelectLookup(items);
			that.Conditions.Add(-1, condition);
			return that;
		}

		public static bool HasAny(this MFSearchBuilder that, MFSearchFlags searchFlags = MFSearchFlags.MFSearchFlagDisableRelevancyRanking, int searchTimeoutInSeconds = 60) {
			return that.Find(searchFlags, false, 1, searchTimeoutInSeconds).Count > 0;
		}

		public static bool HasOne(this MFSearchBuilder that, MFSearchFlags searchFlags = MFSearchFlags.MFSearchFlagDisableRelevancyRanking, int searchTimeoutInSeconds = 60) {
			var objectSearchResults = that.Find(searchFlags, false, 1, searchTimeoutInSeconds);
			return objectSearchResults.Count > 0 && !objectSearchResults.MoreResults;
		}
	}
}
