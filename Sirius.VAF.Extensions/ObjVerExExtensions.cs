using System;
using System.Collections.Generic;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;

using MFilesAPI;

namespace Sirius.VAF {
	public static class ObjVerExExtensions {
		public static void SavePropertiesIfDifferent(this ObjVerEx objVer, PropertyValues properties) {
			var isDifferent = false;
			foreach (PropertyValue newProperty in properties) {
				if (!objVer.TryGetProperty(newProperty.PropertyDef, out var oldProperty)) {
					// We found a new property
					if (newProperty.Value.IsNULL() || newProperty.Value.IsUninitialized()) {
						// But it is nothing, so move along
						continue;
					}
					isDifferent = true;
					break;
				}
				if ((oldProperty.Value.IsNULL() || oldProperty.Value.IsUninitialized()) == (newProperty.Value.IsNULL() || newProperty.Value.IsUninitialized())) {
					// Both empty, next property
					continue;
				}
				switch (oldProperty.Value.DataType) {
				case MFDataType.MFDatatypeLookup:
					var oldLookup = oldProperty.Value.GetValueAsLookup();
					var newLookup = newProperty.Value.GetValueAsLookup();
					if (!LookupComparer.Default.Equals(oldLookup, newLookup)) {
						isDifferent = true;
					}
					break;
				case MFDataType.MFDatatypeMultiSelectLookup:
					var oldMultiSelectLookup = oldProperty.Value.GetValueAsLookups();
					var newMultiSelectLookup = newProperty.Value.GetValueAsLookups();
					if (!LookupComparer.Default.Equals(oldMultiSelectLookup, newMultiSelectLookup)) {
						isDifferent = true;
						break;
					}
					break;
				case MFDataType.MFDatatypeText:
				case MFDataType.MFDatatypeMultiLineText:
					var oldText = (string)oldProperty.Value.Value;
					var newText = (string)newProperty.Value.Value;
					if (!string.Equals(oldText, newText, StringComparison.InvariantCulture)) {
						isDifferent = true;
					}
					break;
				default:
					var oldValue = oldProperty.Value.Value;
					var newValue = newProperty.Value.Value;
					if (!Equals(oldValue, newValue)) {
						isDifferent = true;
					}
					break;
				}
				if (isDifferent) {
					break;
				}
			}
			// If any property was different, save the changes to the ObjVerEx object
			if (isDifferent) {
				objVer.SaveProperties(properties);
			}
		}

		public static ObjectFiles GetFiles(this ObjVerEx that) {
			return that.Vault.ObjectFileOperations.GetFiles(that.ObjVer);
		}

		public static ObjectFile GetSingleFile(this ObjVerEx that) {
			return that.Vault.ObjectFileOperations.GetFiles(that.ObjVer)[1];
		}

		public static IReadOnlyCollection<int> GetClasses(this ObjVerEx that) {
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
	}
}
