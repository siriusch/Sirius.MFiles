using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Sirius.XML {
	public class TypedXElementRegistry {
		private readonly Dictionary<(Type ParentType, XName Name), (Type Type, Func<XElement, XElement> Clone)> elementTypes = new();

		public void RegisterRootElement<TElement>(XName name) where TElement: XElement {
			elementTypes.Add((null, name), CreateElementInfo<TElement>());
		}

		public void RegisterElement<TParent, TElement>(XName name) where TElement: XElement
		                                                           where TParent: XElement {
			elementTypes.Add((typeof(TParent), name), CreateElementInfo<TElement>());
		}

		private bool TryGetElementInfo(XElement element, out (Type Type, Func<XElement, XElement> Clone) elementInfo) {
			return elementTypes.TryGetValue((element.Parent?.GetType(), element.Name), out elementInfo);
		}

		private static (Type Type, Func<XElement, XElement> Clone) CreateElementInfo<T>() where T: XElement {
			var paraOther = Expression.Parameter(typeof(XElement), "other");
			var cloneFunc = Expression.Lambda<Func<XElement, XElement>>(
					Expression.New(typeof(T).GetConstructor(new[] { typeof(XElement) }) ?? throw new MissingMemberException(typeof(T).FullName, ".ctor(XElement)"), paraOther),
					paraOther).Compile();
			return (typeof(T), cloneFunc);
		}

		private void EnsureElementType(object sender, XObjectChangeEventArgs e) {
			if (e.ObjectChange is XObjectChange.Add or XObjectChange.Name
			    && sender is XElement element) {
				EnsureElementType(element);
			}
		}

		private void EnsureElementType(XElement element) {
			if (TryGetElementInfo(element, out var elementInfo) && !elementInfo.Type.IsInstanceOfType(element)) {
				var typedElement = elementInfo.Clone(element);
				element.ReplaceWith(typedElement);
				EnsureElementsType(typedElement.FirstNode);
			} else if (element.GetType() == typeof(XElement)) {
				EnsureElementsType(element.FirstNode);
			}
		}

		private void EnsureElementsType(XNode node) {
			while (node != null) {
				var elem = node as XElement;
				node = node.NextNode; // NextNode must be read before call to EnsureElementType, since this may replace the node
				if (elem != null) {
					EnsureElementType(elem);
				}
			}
		}

		public void ApplyToDocument(XDocument document) {
			document.Changed += EnsureElementType;
			if (document.Root != null) {
				EnsureElementType(document.Root);
			}
		}
	}
}
