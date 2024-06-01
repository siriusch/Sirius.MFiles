using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

using JetBrains.Annotations;

namespace Sirius.XML {
	public static class ElementAttribute<T> {
		public static readonly Func<string, T> Parse;
		public static readonly Func<T, string> Stringify;

		static ElementAttribute() {
			// For strings, create a simple pass-through
			if (typeof(T) == typeof(string)) {
				var paraString = Expression.Parameter(typeof(string));
				var passthrough = Expression.Lambda<Func<string, string>>(paraString, paraString).Compile();
				Parse = (Func<string, T>)(object)passthrough;
				Stringify = (Func<T, string>)(object)passthrough;
				return;
			}
			// For nullable types, create a wrapper to handle the nullable and invoke the non-nullable ElementAttribute
			var underlyingType = Nullable.GetUnderlyingType(typeof(T));
			if (underlyingType != null) {
				var underlyingElementAttribute = typeof(ElementAttribute<>).MakeGenericType(underlyingType);
				var paraString = Expression.Parameter(typeof(string));
				Parse = Expression.Lambda<Func<string, T>>(
						Expression.Condition(
								Expression.Call(typeof(string).GetMethod(nameof(string.IsNullOrEmpty), BindingFlags.Static|BindingFlags.Public),
										paraString),
								Expression.Default(typeof(T)),
								Expression.Convert(
										Expression.Invoke(
												Expression.Field(null, underlyingElementAttribute.GetField(nameof(Parse), BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic)),
												paraString),
										typeof(T))),
						paraString).Compile();
				var paraValue = Expression.Parameter(typeof(T));
				Stringify = Expression.Lambda<Func<T, string>>(
						Expression.Condition(
								Expression.Property(paraValue, typeof(T).GetProperty(nameof(Nullable<bool>.HasValue))),
								Expression.Invoke(
										Expression.Field(null, underlyingElementAttribute.GetField(nameof(Stringify), BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic)),
										Expression.Property(paraValue, typeof(T).GetProperty(nameof(Nullable<bool>.Value)))),
								Expression.Default(typeof(string))),
						paraValue).Compile();
				return;
			}
			// Special handling of DateTime, we want this in universal time and without fraction
			if (typeof(T) == typeof(DateTime)) {
				Parse = (Func<string, T>)(object)new Func<string, DateTime>(str => XmlConvert.ToDateTime(str, XmlDateTimeSerializationMode.Utc));
				Stringify = (Func<T, string>)(object)new Func<DateTime, string>(value => XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc));
				return;
			}
			// If supported by XmlConvert, use these methods directly
			var toStringMethod = typeof(XmlConvert).GetMethod(nameof(XmlConvert.ToString), BindingFlags.Static|BindingFlags.Public, null, new[] { typeof(T) }, null);
			if (toStringMethod != null) {
				var toTypeMethod = typeof(XmlConvert).GetMethods(BindingFlags.Static|BindingFlags.Public).SingleOrDefault(m => m.ReturnType == typeof(T) && m.Name.StartsWith("To", StringComparison.Ordinal) && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(string) }));
				if (toTypeMethod != null) {
					Parse = (Func<string, T>)Delegate.CreateDelegate(typeof(Func<string, T>), toTypeMethod);
					Stringify = (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), toStringMethod);
					return;
				}
			}
			// Otherwise revert to Convert.ChangeType and ToString
			Parse = str => (T)Convert.ChangeType(str, typeof(T), CultureInfo.InvariantCulture);
			Stringify = typeof(IConvertible).IsAssignableFrom(typeof(T))
					? value => ((IConvertible)value)?.ToString(CultureInfo.InvariantCulture)
					: value => value?.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Get([NotNull] XElement owner, [NotNull] XName name) {
			return Get(owner, name, Parse);
		}

		public static T Get([NotNull] XElement owner, [NotNull] XName name, [NotNull] Func<string, T> parse) {
			var attr = owner.Attribute(name);
			if (attr == null) {
				throw new InvalidOperationException($"The attribute {name} is missing on the {owner.Name} element.");
			}
			return parse(attr.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetOrDefault([NotNull] XElement owner, [NotNull] XName name, T @default = default) {
			return GetOrDefault(owner, name, @default, Parse);
		}

		public static T GetOrDefault([NotNull] XElement owner, [NotNull] XName name, T @default, [NotNull] Func<string, T> parse) {
			var attr = owner.Attribute(name);
			return attr == null ? @default : parse(attr.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set([NotNull] XElement owner, [NotNull] XName name, [NotNull] T value) {
			Set(owner, name, value, Stringify);
		}

		public static void Set([NotNull] XElement owner, [NotNull] XName name, [NotNull] T value, [NotNull] Func<T, string> stringify) {
			var attr = owner.Attribute(name);
			if (attr == null) {
				attr = new XAttribute(name, stringify(value));
				owner.Add(attr);
			} else {
				attr.Value = stringify(value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetOrRemove([NotNull] XElement owner, [NotNull] XName name, T value) {
			SetOrRemove(owner, name, value, Stringify);
		}

		public static void SetOrRemove([NotNull] XElement owner, [NotNull] XName name, T value, [NotNull] Func<T, string> stringify) {
			var attr = owner.Attribute(name);
			if (EqualityComparer<T>.Default.Equals(value, default)) {
				attr?.Remove();
			} else if (attr == null) {
				attr = new XAttribute(name, stringify(value));
				owner.Add(attr);
			} else {
				attr.Value = stringify(value);
			}
		}
	}
}
