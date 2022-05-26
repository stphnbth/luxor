namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmlprogresselement
    public class HTMLFieldSetElement : HTMLElement, IValidityState
    {
        // PRIVATE FIELDS
        private bool _disabled;
        private HTMLFormElement? form;
        private string _name;
        private string _type;
        private List<Element> _elements;

        // IValidityState Implementation
        private bool _willValidate;
        private ValidityState _validity;
        private string _validationMessage;

        public bool WillValidate { get => _willValidate; set => _willValidate = value; }
        public ValidityState Validity { get => _validity; set => _validity = value; }
        public string ValidationMessage { get => _validationMessage; set => _validationMessage = value; }

        // PUBLIC PROPERTIES
        // TODO: HTMLFieldSetElement

        // CONSTRUCTOR
        public HTMLFieldSetElement(Document nodeDocument) : base(nodeDocument) {}
    }
}