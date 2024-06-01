using System.Xml.Linq;

using JetBrains.Annotations;

using Xunit;
using Xunit.Abstractions;

namespace Sirius.XML {
	[TestSubject(typeof(TypedXElementRegistry))]
	public class TypedXElementRegistryTests {
		public class Root: XElement {
			public new static readonly XName Name = "root";

			public Root(): base(Name) { }

			public Root(XElement other): base(other) { }

			public int Version {
				get => ElementAttribute<int>.Get(this, "version");
				set => ElementAttribute<int>.Set(this, "version", value);
			}
		}

		public class A: XElement {
			public new static readonly XName Name = "a";

			public A(): base(Name) { }

			public A(XElement other): base(other) { }
		}

		public class B: XElement {
			public new static readonly XName Name = "b";

			public B(): base(Name) { }

			public B(XElement other): base(other) { }
		}

		protected ITestOutputHelper Output {
			get;
		}

		public TypedXElementRegistryTests(ITestOutputHelper output) {
			Output = output;
		}

		[Fact]
		public void TypedDocument() {
			var doc = XDocument.Parse("<root version='1'><a><b/></a><b><a/></b></root>");
			var registry = new TypedXElementRegistry();
			registry.RegisterRootElement<Root>(Root.Name);
			registry.RegisterElement<Root, A>(A.Name);
			registry.RegisterElement<Root, B>(B.Name);
			registry.RegisterElement<A, B>(B.Name);
			// Note: A cannot be nested in B
			registry.ApplyToDocument(doc);
			Assert.IsType<Root>(doc.Root);
			Assert.IsType<A>(doc.Root.Element(A.Name));
			Assert.IsType<B>(doc.Root.Element(B.Name));
			Assert.IsType<B>(doc.Root.Element(A.Name).Element(B.Name));
			Assert.IsType<XElement>(doc.Root.Element(B.Name).Element(A.Name));
			Assert.Equal(1, ((Root)doc.Root).Version);
		}
	}
}
