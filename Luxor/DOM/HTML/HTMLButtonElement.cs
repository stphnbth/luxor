namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmlbuttonelement
    public class HTMLButtonElement : HTMLElement, IForm, IValidityState
    {
        // PRIVATE FIELDS
        private string _type;

        // IForm Implementation
        private bool _disabled;
        private HTMLFormElement? _form;
        private string _formAction;
        private string _formEnctype;
        private string _formMethod;
        private bool _formNoValidate;
        private string _formTarget;
        private string _name;
        private string _value;

        public bool Disabled { get => _disabled; set => _disabled = value; }
        public HTMLFormElement? Form { get => _form; }
        public string FormAction { get => _formAction; set => _formAction = value; }
        public string FormEnctype { get => _formEnctype; set => _formEnctype = value; }
        public string FormMethod { get => _formMethod; set => _formMethod = value; }
        public bool FormNoValidate { get => _formNoValidate; set => _formNoValidate = value; }
        public string FormTarget { get => _formTarget; set => _formTarget = value; }
        public string Name { get => _name; set => _name = value; }
        public string Value { get => _value; set => _value = value; }

        // IValidityState Implementation
        private bool _willValidate;
        private ValidityState _validity;
        private string _validationMessage;

        public bool WillValidate { get => _willValidate; }
        public ValidityState Validity { get => _validity; }
        public string ValidationMessage { get => _validationMessage; }


        // PUBLIC PROPERTIES
        // TODO: HTMLButtonElement

        // CONSTRUCTOR
        public HTMLButtonElement(Document nodeDocument) : base(nodeDocument) {}
    }
}