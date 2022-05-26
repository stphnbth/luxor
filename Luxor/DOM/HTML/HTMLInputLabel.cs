namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/input.html#htmlinputelement
    public class HTMLInputElement : HTMLElement, IForm, ISelection, IValidityState
    {
        // PRIVATE FIELDS
        private string _accept;
        private string _alt;
        private string _autocomplete;
        private bool _defaultChecked;
        private bool _checked;
        private string _dirName;
        private List<DOMFile>? _files;
        private ulong _height;
        private bool indeterminate;
        private HTMLElement? _list;
        private string _max;
        private long _maxLength;
        private string _min;
        private long _minLength;
        private bool _multiple;
        private string _pattern;
        private string _placeholder;
        private bool _readOnly;
        private bool _required;
        private ulong _size;
        private string _src;
        private string _step;
        private string _type;
        private string _defaultValue;
        private object? _valueAsDate;
        private double _valueAsNumber;
        private ulong _width;
        private List<Node>? _labels;

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
        // TODO: HTMLInputElement

        // CONSTRUCTOR
        public HTMLInputElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public void stepUp(long n = 1)
        {
            throw new NotImplementedException();
        }

        public void stepDown(long n = 1)
        {
            throw new NotImplementedException();
        }

        public void showPicker()
        {
            throw new NotImplementedException();
        }
    }

    public struct DOMFile
    {
        public string name;
        public long lastModified;
    }
}