using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MFiles.VAF.Common;

using MFilesAPI;

namespace Sirius.VAF {
	public static class PropertyValueExtensions {
		public static bool HasValue(this PropertyValue that) {
			return that != null && !that.Value.IsEmpty() && !that.Value.IsNULL() && !that.Value.IsUninitialized();
		}

		public static ObjVerEx LookupToObjVerEx(this PropertyValue that, Vault vault) {
			return that?.Value.GetValueAsLookup().ToObjVerEx(vault);
		}

		public static ValueListItem LookupToItem(this PropertyValue that, Vault vault) {
			if (that == null) {
				return null;
			}
			var propDef = vault.PropertyDefOperations.GetPropertyDef(that.PropertyDef); // TODO: Ask Craig why the MFIdentifier.ValueList is not populated
			var valListObjType = vault.ValueListOperations.GetValueList(propDef.ValueList);
			Debug.Assert(!valListObjType.RealObjectType);
			return vault.ValueListItemOperations.GetValueListItemByID(valListObjType.ID, that.Value.GetLookupID());
		}

		public static IEnumerable<ObjVerEx> LookupsToObjVerEx(this PropertyValue that, Vault vault) {
			if (that == null) {
				return Enumerable.Empty<ObjVerEx>();
			}
			return that
					.Value
					.GetValueAsLookups()
					.Cast<Lookup>()
					.Select(l => l.ToObjVerEx(vault));
		}

		public static IEnumerable<ValueListItem> LookupsToItems(this PropertyValue that, Vault vault) {
			if (that == null) {
				return Enumerable.Empty<ValueListItem>();
			}
			var propLookups = that.Value.GetValueAsLookups();
			var propDef = vault.PropertyDefOperations.GetPropertyDef(that.PropertyDef); // TODO: Ask Craig why the MFIdentifier.ValueList is not populated
			var valListObjType = vault.ValueListOperations.GetValueList(propDef.ValueList);
			Debug.Assert(!valListObjType.RealObjectType);
			return propLookups
					.Cast<Lookup>()
					.Select(l => vault.ValueListItemOperations.GetValueListItemByID(valListObjType.ID, l.Item));
		}
	}
}
