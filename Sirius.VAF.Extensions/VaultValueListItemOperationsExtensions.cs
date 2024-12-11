using System;
using System.Collections.Generic;

using MFiles.VAF.Configuration;

using MFilesAPI;

namespace Sirius.VAF {
	public static class VaultValueListItemOperationsExtensions {
		public static bool TryGetValueListItemByName(this VaultValueListItemOperations that, MFIdentifier valueList, string value, out ValueListItem item) {
			if (string.IsNullOrEmpty(value)) {
				item = null;
				return false;
			}
			var condition = new SearchCondition();
			condition.Expression.SetValueListItemExpression(MFValueListItemPropertyDef.MFValueListItemPropertyDefName, MFParentChildBehavior.MFParentChildBehaviorNone);
			condition.ConditionType = MFConditionType.MFConditionTypeEqual;
			condition.TypedValue.SetValueEx(MFDataType.MFDatatypeText, value);
			var result = that.SearchForValueListItems(valueList, new SearchConditions() { { -1, condition } });
			if (result.Count == 1) {
				item = result[1];
				return true;
			}
			item = null;
			return false;
		}

		public static ValueListItem GetValueListItemByName(this VaultValueListItemOperations that, MFIdentifier valueList, string value) {
			if (TryGetValueListItemByName(that, valueList, value, out var item)) {
				return item;
			}
			throw new KeyNotFoundException(Strings.ValueListValueNotFound(value, valueList.Alias ?? valueList.ID.ToString()));
		}

		public static ValueListItem GetOrAddValueListItemByName(this VaultValueListItemOperations that, MFIdentifier valueList, string value) {
			if (string.IsNullOrEmpty(value)) {
				throw new ArgumentNullException(nameof(value));
			}
			if (TryGetValueListItemByName(that, valueList, value, out var item)) {
				return item;
			}
			return that.AddValueListItem(valueList, new ValueListItem() { Name = value });
		}
	}
}
