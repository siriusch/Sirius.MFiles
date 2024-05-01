using System;

using MFiles.VAF.Common;
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

		public static ObjVerEx ToObjVerEx(this ObjectVersionAndProperties that) {
			if (that == null) {
				return null;
			}
			return new ObjVerEx(that.Vault, that);
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
	}
}
