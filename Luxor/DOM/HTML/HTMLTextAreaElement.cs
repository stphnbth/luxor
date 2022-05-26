namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmltextareaelement
    public class HTMLTextAreaElement : HTMLElement, IValidityState
    {
        // PRIVATE FIELDS
        private string _autocomplete;
        private ulong _cols;
        private string _dirName;
        private bool _disabled;
        private HTMLFormElement? _form;
        private long _maxLength;
        private long _minLength;
        private string _name;
        private string _placeholder;
        private bool _readOnly;
        private bool _required;
        private ulong _rows;
        private string _wrap;
        private string _type;
        private string _defaultValue;
        private string _value;
        private ulong _textLength;
        private List<Node> _labels;

        // ISelection Implementation
        private ulong _selectionStart;
        private ulong _selectionEnd;
        private string _selectionDirection;

        public ulong SelectionStart { get => _selectionStart; set => _selectionStart = value; }
        public ulong SelectionEnd { get => _selectionEnd; set => _selectionEnd = value; }
        public string SelectionDirection { get => _selectionDirection; set => _selectionDirection = value; }

        // IValidityState Implementation
        private bool _willValidate;
        private ValidityState _validity;
        private string _validationMessage;

        public bool WillValidate { get => _willValidate; set => _willValidate = value; }
        public ValidityState Validity { get => _validity; set => _validity = value; }
        public string ValidationMessage { get => _validationMessage; set => _validationMessage = value; }

        // PUBLIC PROPERTIES
        // TODO: HTMLTextAreaElement

        // CONSTRUCTOR
        public HTMLTextAreaElement(Document nodeDocument) : base(nodeDocument) {}
    }
}