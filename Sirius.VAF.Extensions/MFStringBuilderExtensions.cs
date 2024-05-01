using System;

using MFiles.VAF.Common;
using MFiles.VAF.Configuration;

using MFilesAPI;

using OneOf;

namespace Sirius.VAF {
	public static class MFStringBuilderExtensions {
		public static OneOf<MFIdentifier, MFBuiltInPropertyDef>[] Prop(this MFIdentifier that, OneOf<MFIdentifier, MFBuiltInPropertyDef> segment) {
			return new[] { that, segment };
		}

		public static OneOf<MFIdentifier, MFBuiltInPropertyDef>[] Prop(this MFBuiltInPropertyDef that, OneOf<MFIdentifier, MFBuiltInPropertyDef> segment) {
			return new[] { that, segment };
		}

		public static OneOf<MFIdentifier, MFBuiltInPropertyDef>[] Prop(this OneOf<MFIdentifier, MFBuiltInPropertyDef>[] that, OneOf<MFIdentifier, MFBuiltInPropertyDef> segment) {
			Array.Resize(ref that, that.Length+1);
			that[that.Length-1] = segment;
			return that;
		}

		public static (OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter) Prop(this MFIdentifier that, OneOf<MFIdentifier, MFBuiltInPropertyDef> segment, OneOf<string, Func<PropertyValue, string>> formatter) {
			return (new[] { that, segment }, formatter);
		}

		public static (OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter) Prop(this MFBuiltInPropertyDef that, OneOf<MFIdentifier, MFBuiltInPropertyDef> segment, OneOf<string, Func<PropertyValue, string>> formatter) {
			return (new[] { that, segment }, formatter);
		}

		public static (OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter)
				Prop(this OneOf<MFIdentifier, MFBuiltInPropertyDef>[] that, OneOf<MFIdentifier, MFBuiltInPropertyDef> segment, OneOf<string, Func<PropertyValue, string>> formatter) {
			Array.Resize(ref that, that.Length+1);
			that[that.Length-1] = segment;
			return (that, formatter);
		}

		public static TypedValue BuildText(this ObjVerEx that, params OneOf<
				string,
				MFIdentifier,
				(MFIdentifier PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				MFBuiltInPropertyDef,
				(MFBuiltInPropertyDef PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				OneOf<MFIdentifier, MFBuiltInPropertyDef>[],
				(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter)>[] fragments) {
			return BuildString(that, fragments).ToTextTypedValue();
		}

		public static TypedValue BuildMultiLineText(this ObjVerEx that, params OneOf<
				string,
				MFIdentifier,
				(MFIdentifier PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				MFBuiltInPropertyDef,
				(MFBuiltInPropertyDef PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				OneOf<MFIdentifier, MFBuiltInPropertyDef>[],
				(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter)>[] fragments) {
			return BuildString(that, fragments).ToMultiLineTextTypedValue();
		}

		public static string BuildString(this ObjVerEx that, params OneOf<
				string,
				MFIdentifier,
				(MFIdentifier PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				MFBuiltInPropertyDef,
				(MFBuiltInPropertyDef PropertyDef, OneOf<string, Func<PropertyValue, string>> Formatter),
				OneOf<MFIdentifier, MFBuiltInPropertyDef>[],
				(OneOf<MFIdentifier, MFBuiltInPropertyDef>[] PropertyDefs, OneOf<string, Func<PropertyValue, string>> Formatter)>[] fragments) {
			return new MFStringBuilder(that)
					.AppendRange(fragments)
					.ToString();
		}
	}
}
