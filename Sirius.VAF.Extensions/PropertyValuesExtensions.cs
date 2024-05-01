using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;

using MFilesAPI;

namespace Sirius.VAF {
	public static class PropertyValuesExtensions {
		public static bool HasValue(this PropertyValue that) {
			return that != null && !that.Value.IsEmpty() && !that.Value.IsNULL() && !that.Value.IsUninitialized();
		}

		public static ObjVerEx LookupObjectByProperty(this PropertyValues that, MFIdentifier propertyDef, Vault vault) {
			var propValue = that.SearchForPropertyEx(propertyDef, true);
			if (propValue == null) {
				return null;
			}
			var propLookups = propValue.Value.GetValueAsLookups();
			if (propLookups.Count != 1) {
				return null;
			}
			return propLookups[1].ToObjVerEx(vault);
		}

		public static IEnumerable<ObjVerEx> LookupObjectsByProperty(this PropertyValues that, MFIdentifier propertyDef, Vault vault) {
			return that
					.SearchForPropertyEx(propertyDef, true)
					.LookupsToObjVerEx(vault);
		}

		public static ValueListItem LookupItemByProperty(this PropertyValues that, MFIdentifier propertyDef, Vault vault) {
			var propValue = that.SearchForPropertyEx(propertyDef, true);
			if (propValue == null) {
				return null;
			}
			var propLookups = propValue.TypedValue.GetValueAsLookups();
			if (propLookups.Count != 1) {
				return null;
			}
			var propDef = vault.PropertyDefOperations.GetPropertyDef(propertyDef); // TODO: Ask Craig why the MFIdentifier.ValueList is not populated
			var valListObjType = vault.ValueListOperations.GetValueList(propDef.ValueList);
			Debug.Assert(!valListObjType.RealObjectType);
			return vault.ValueListItemOperations.GetValueListItemByID(valListObjType.ID, propLookups[1].Item);
		}

		public static IEnumerable<ValueListItem> LookupItemsByProperty(this PropertyValues that, MFIdentifier propertyDef, Vault vault) {
			return that
					.SearchForPropertyEx(propertyDef, true)
					.LookupsToItems(vault);
		}

		public static string GetTitleOrName(this PropertyValues that) {
			return that.GetProperty(0).GetValueAsLocalizedText();
		}

		public static PropertyValue GetOrAdd(PropertyValues that, MFIdentifier property) {
			var propertyValue = that.SearchForPropertyEx(property, true);
			if (propertyValue == null) {
				propertyValue = new PropertyValueClass {
						PropertyDef = property
				};
				that.Add(-1, propertyValue);
				return that.SearchForProperty(property);
			}
			return propertyValue;
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, bool? value) {
			GetOrAdd(that, property).Value.SetValueOrNULL(MFDataType.MFDatatypeBoolean, value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, int? value) {
			GetOrAdd(that, property).Value.SetValueOrNULL(MFDataType.MFDatatypeInteger, value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, long? value) {
			GetOrAdd(that, property).Value.SetValueOrNULL(MFDataType.MFDatatypeInteger64, value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, double? value) {
			GetOrAdd(that, property).Value.SetValueOrNULL(MFDataType.MFDatatypeFloating, value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, DateTime? value) {
			GetOrAdd(that, property).Value.SetValueOrNULL(MFDataType.MFDatatypeDate, value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, TypedValue value) {
			GetOrAdd(that, property).Value.CloneFrom(value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, string value, bool multiLine = false) {
			GetOrAdd(that, property).Value.SetValueOrNULL(multiLine ? MFDataType.MFDatatypeMultiLineText : MFDataType.MFDatatypeText, value);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, MFIdentifier value, bool multi = false) {
			SetValue(that, property, value == null
					? null
					: new LookupClass {
							Item = value
					}, multi);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, IEnumerable<MFIdentifier> value) {
			SetValue(that, property, value.Select(id => new LookupClass {
					Item = id
			}));
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, ObjVerEx value, bool multi = false) {
			SetValue(that, property, value == null
					? null
					: new LookupClass {
							Item = value.ID
					}, multi);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, IEnumerable<ObjVerEx> value) {
			SetValue(that, property, value.Select(item => new LookupClass {
					Item = item.ID
			}));
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, ValueListItem value, bool multi = false) {
			SetValue(that, property, value == null
					? null
					: new LookupClass {
							Item = value.ID
					}, multi);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, IEnumerable<ValueListItem> value) {
			SetValue(that, property, value.Select(item => new LookupClass {
					Item = item.ID
			}));
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, Lookup value, bool multi = false) {
			if (multi) {
				SetValue(that, property, value == null ? Enumerable.Empty<Lookup>() : new[] { value });
			} else {
				GetOrAdd(that, property).Value.SetValueOrNULL(MFDataType.MFDatatypeLookup, value);
			}
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, Lookups values) {
			GetOrAdd(that, property).Value.SetValueToMultiSelectLookup(values);
		}

		public static void SetValue(this PropertyValues that, MFIdentifier property, IEnumerable<Lookup> values) {
			var lookups = new LookupsClass();
			lookups.AddRange(values);
			SetValue(that, property, lookups);
		}
	}
}
