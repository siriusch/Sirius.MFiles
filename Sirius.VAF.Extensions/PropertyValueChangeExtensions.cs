using System.Collections.Generic;
using System.Linq;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;

using MFilesAPI;

namespace Sirius.VAF {
	public static class PropertyValueChangeExtensions {
		public static IEnumerable<PropertyValueChange> WithoutBuiltIn(this IEnumerable<PropertyValueChange> that) {
			return that.Where(c => c.PropertyDef >= 1000);
		}

		public static IEnumerable<PropertyValueChange> Without(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef) {
			return that.Where(c => c.PropertyDef != propertyDef);
		}

		public static IEnumerable<PropertyValueChange> Without(this IEnumerable<PropertyValueChange> that, params MFIdentifier[] propertyDefs) {
			return that.Where(c => propertyDefs.All(pd => pd.ID != c.PropertyDef));
		}

		public static bool Added(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef) {
			return that.Any(c => c.ChangeType == PropertyValueChangeType.Added && c.PropertyDef == propertyDef);
		}

		public static bool Removed(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef) {
			return that.Any(c => c.ChangeType == PropertyValueChangeType.Removed && c.PropertyDef == propertyDef);
		}

		public static bool Modified(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef) {
			return that.Any(c => c.ChangeType == PropertyValueChangeType.Modified && c.PropertyDef == propertyDef);
		}

		public static bool ModifiedOrRemoved(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef) {
			return that.Any(c => c.ChangeType is PropertyValueChangeType.Modified or PropertyValueChangeType.Removed && c.PropertyDef == propertyDef);
		}

		public static bool HasChanged(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef) {
			return that.Any(c => c.ChangeType != PropertyValueChangeType.None && c.PropertyDef == propertyDef);
		}

		public static bool HasChanged(this IEnumerable<PropertyValueChange> that, params MFIdentifier[] propertyDefs) {
			return that.Any(c => c.ChangeType != PropertyValueChangeType.None && propertyDefs.All(pd => pd.ID != c.PropertyDef));
		}

		public static bool TryGetOld(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef, out PropertyValue value) {
			value = that.FirstOrDefault(c => c.ChangeType is PropertyValueChangeType.Modified or PropertyValueChangeType.Removed && c.PropertyDef == propertyDef)?.OldValue;
			return value != null;
		}

		public static bool TryGetNew(this IEnumerable<PropertyValueChange> that, MFIdentifier propertyDef, out PropertyValue value) {
			value = that.FirstOrDefault(c => c.ChangeType is PropertyValueChangeType.Added or PropertyValueChangeType.Modified && c.PropertyDef == propertyDef)?.NewValue;
			return value != null;
		}
	}
}
