using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;

using MFilesAPI;

namespace Sirius.VAF {
	public static class ObjVerExExtensions {
		public static void SavePropertyIfDifferent([NotNull] this ObjVerEx that, [NotNull] MFIdentifier propertyDef, MFDataType dataType, [CanBeNull] object value) {
			if (propertyDef == null) {
				throw new ArgumentNullException(nameof(propertyDef));
			}
			var existing = that.GetProperty(propertyDef);
			var typedValue = new TypedValue();
			typedValue.SetValueEx(dataType, value);
			if (existing?.TypedValue.IsEqual(typedValue, EqualityCompareOptions.TextCaseSensitive) != true) {
				that.SaveProperty(propertyDef, typedValue.DataType, typedValue.Value);
			}
		}

		public static void SavePropertyIfDifferent([NotNull] this ObjVerEx that, [NotNull] PropertyValue propertyValue) {
			if (propertyValue == null) {
				throw new ArgumentNullException(nameof(propertyValue));
			}
			var existing = that.GetProperty(propertyValue.PropertyDef);
			var typedValue = propertyValue.TypedValue;
			if (existing?.TypedValue.IsEqual(typedValue, EqualityCompareOptions.TextCaseSensitive) != true) {
				that.SaveProperty(propertyValue.PropertyDef, typedValue.DataType, typedValue.Value);
			}
		}

		public static void SavePropertiesIfDifferent([NotNull] this ObjVerEx that, [NotNull] PropertyValues properties) {
			if (properties == null) {
				throw new ArgumentNullException(nameof(properties));
			}
			var isDifferent = false;
			foreach (PropertyValue newProperty in properties) {
				if (!that.TryGetProperty(newProperty.PropertyDef, out var oldProperty)) {
					// We found a new property
					if (newProperty.Value.IsNULL() || newProperty.Value.IsUninitialized()) {
						// But it is nothing, so move along
						continue;
					}
					isDifferent = true;
					break;
				}
				if ((oldProperty.Value.IsNULL() || oldProperty.Value.IsUninitialized()) && (newProperty.Value.IsNULL() || newProperty.Value.IsUninitialized())) {
					// Both empty, next property
					continue;
				}
				if (!oldProperty.TypedValue.IsEqual(newProperty.TypedValue, EqualityCompareOptions.TextCaseSensitive)) {
					isDifferent = true;
					break;
				}
			}
			// If any property was different, save the changes to the ObjVerEx object
			if (isDifferent) {
				that.SaveProperties(properties);
			}
		}

		public static ObjectFiles GetFiles([NotNull] this ObjVerEx that) {
			return that.Vault.ObjectFileOperations.GetFiles(that.ObjVer);
		}

		public static ObjectFile GetSingleFile([NotNull] this ObjVerEx that) {
			return that.Vault.ObjectFileOperations.GetFiles(that.ObjVer)[1];
		}

		public static IReadOnlyCollection<int> GetClasses([NotNull] this ObjVerEx that) {
			var result = new HashSet<int>() {
					that.Class
			};
			if (that.TryGetPropertyWithValue(MFBuiltInPropertyDef.MFBuiltInPropertyDefAdditionalClasses, out var classes)) {
				result.UnionWith(classes.Value.GetValueAsLookupIDs());
			}
			return result;
		}

		public static bool UserHasPermissionTo(this ObjVerEx that, int userId, MFPermissionsExpressionType permission, MFConditionType condition = MFConditionType.MFConditionTypeEqual) {
			return new MFSearchBuilder(that.Vault)
					.ObjType(that.ObjID.Type)
					.ObjectId(that.ObjID.ID)
					.UserHasPermissionTo(userId, permission, condition)
					.Find(maxResults: 1)
					.Count > 0;
		}

		public static bool VisibleTo(this ObjVerEx that, int userId) {
			return that.UserHasPermissionTo(userId, MFPermissionsExpressionType.MFVisibleTo);
		}

		public static bool EditableBy(this ObjVerEx that, int userId) {
			return that.UserHasPermissionTo(userId, MFPermissionsExpressionType.MFEditableBy);
		}

		public static bool DeletableBy(this ObjVerEx that, int userId) {
			return that.UserHasPermissionTo(userId, MFPermissionsExpressionType.MFDeletableBy);
		}

		public static bool TryGetPropertyWithValue(this ObjVerEx that, MFIdentifier property, out PropertyValue propVal) {
			return that.TryGetProperty(property, out propVal) && !(propVal.Value.IsEmpty() || propVal.Value.IsNULL() || propVal.Value.IsUninitialized());
		}

		public static string GetPropertyDisplayValue(this ObjVerEx that, MFIdentifier property, string defaultText = "") {
			return that.TryGetPropertyWithValue(property, out var propVal)
					? propVal.Value.DisplayValue
					: defaultText;
		}

		public static string GetPropertyTextEx(this ObjVerEx that, MFIdentifier property, bool localized, bool longDateFormat, bool noSeconds, bool numericValueAsKilobytes, bool allowDigitGrouping, string defaultText = "") {
			return that.TryGetPropertyWithValue(property, out var propVal)
					? propVal.GetValueAsTextEx(localized, true, false, longDateFormat, noSeconds, numericValueAsKilobytes, allowDigitGrouping)
					: defaultText;
		}

		public static string GetPropertyUnlocalizedText(this ObjVerEx that, MFIdentifier property, string defaultText = "") {
			return that.TryGetPropertyWithValue(property, out var propVal)
					? propVal.GetValueAsUnlocalizedText()
					: defaultText;
		}

		public static string GetPropertyLocalizedText(this ObjVerEx that, MFIdentifier property, string defaultText = "") {
			return that.TryGetPropertyWithValue(property, out var propVal)
					? propVal.GetValueAsLocalizedText()
					: defaultText;
		}

		public static string GetPropertyLocalizedTextEx(this ObjVerEx that, MFIdentifier property, bool allowDigitGrouping, string defaultText = "") {
			return that.TryGetPropertyWithValue(property, out var propVal)
					? propVal.GetValueAsLocalizedTextEx(allowDigitGrouping)
					: defaultText;
		}

		public static Lookup ToSpecificVersionLookup(this ObjVerEx that) {
			return new Lookup() {
					ObjectType = that.Type,
					Item = that.ID,
					Version = that.Version
			};
		}

		public static Lookup ToLatestVersionLookup(this ObjVerEx that) {
			return new Lookup() {
					ObjectType = that.Type,
					Item = that.ID,
					Version = -1
			};
		}

		public static string GetUid(this ObjVerEx that) {
			return that.Info.ObjectGUID.Trim('{', '}');
		}
	}
}
