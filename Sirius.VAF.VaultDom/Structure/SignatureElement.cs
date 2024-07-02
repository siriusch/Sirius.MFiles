using System.Xml.Linq;

using Sirius.XML;

namespace Sirius.VAF.VaultDom.Structure {
	public class SignatureElement: XElement {
		public static readonly XName ElementName = "signature";
		private static readonly XName ContextName = "context";
		private static readonly XName ModeName = "mode";
		private static readonly XName RequiredName = "required";
		private static readonly XName SeparateName = "separate";
		private static readonly XName ReasonName = "reason";
		private static readonly XName MeaningName = "meaning";
		private static readonly XName AdditionalInfoName = "additionalinfo";
		private static readonly XName SignatureIdName = "signatureid";

		public SignatureElement(): base(ElementName) { }

		public SignatureElement(XElement node): base(node) { }

		public SignatureContext Context {
			get => ElementAttribute<SignatureContext>.Get(this, ContextName);
			set => ElementAttribute<SignatureContext>.Set(this, ContextName, value);
		}

		public SignatureMode Mode {
			get => ElementAttribute<SignatureMode>.Get(this, ModeName);
			set => ElementAttribute<SignatureMode>.Set(this, ModeName, value);
		}

		public bool Required {
			get => ElementAttribute<bool>.Get(this, RequiredName);
			set => ElementAttribute<bool>.Set(this, RequiredName, value);
		}

		public bool Separate {
			get => ElementAttribute<bool>.Get(this, SeparateName);
			set => ElementAttribute<bool>.Set(this, SeparateName, value);
		}

		public ManifestationElement Manifestation {
			get => (ManifestationElement)Element(ManifestationElement.ElementName);
			set => this.SetElement(ManifestationElement.ElementName, value);
		}

		public XElement Reason {
			get => Element(ReasonName);
			set => this.SetElement(ReasonName, value);
		}

		public XElement Meaning {
			get => Element(MeaningName);
			set => this.SetElement(MeaningName, value);
		}

		public FreeformElement Freeform {
			get => (FreeformElement)Element(FreeformElement.ElementName);
			set => this.SetElement(FreeformElement.ElementName, value);
		}

		public XElement AdditionalInfo {
			get => Element(AdditionalInfoName);
			set => this.SetElement(AdditionalInfoName, value);
		}

		public XElement SignatureId {
			get => Element(SignatureIdName);
			set => this.SetElement(SignatureIdName, value);
		}
	}
}
