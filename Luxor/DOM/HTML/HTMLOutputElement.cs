namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/image-maps.html#htmlareaelement
    public class HTMLOutputElement : HTMLElement, IValidityState
    {
        // PRIVATE FIELDS
        private List<string> _htmlFor;
        private HTMLFormElement? _form;
        private string _name;
        private string _type;
        private string _defaultValue;
        private string _value;
        private List<Node> _labels;

        // IValidityState Implementation
        private bool _willValidate;
        private ValidityState _validity;
        private string _validationMessage;

        public bool WillValidate { get => _willValidate; set => _willValidate = value; }
        public ValidityState Validity { get => _validity; set => _validity = value; }
        public string ValidationMessage { get => _validationMessage; set => _validationMessage = value; }

        // PUBLIC PROPERTIES
        // TODO: HTMLOutputElement

        // CONSTRUCTOR
        public HTMLOutputElement(Document nodeDocument) : base(nodeDocument) {}
    }
}