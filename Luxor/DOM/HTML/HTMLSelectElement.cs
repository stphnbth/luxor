namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmlselectelement
    public class HTMLSelectElement : HTMLElement, IValidityState
    {
        // PRIVATE FIELDS
        private string _autocomplete;
        private bool _disabled;
        private HTMLFormElement? _form;
        private bool _multiple;
        private string _name;
        private bool _required;
        private ulong _size;
        private string _type;
        private List<HTMLOptionElement> _options;
        private ulong _length;
        private List<Element> _selectedOptions;
        private ulong _selectedIndex;
        private string _value;
        private List<Node> _labels;

        // PUBLIC PROPERTIES
        public string Autocomplete { get => _autocomplete; set => _autocomplete = value; }
        public bool Disabled { get => _disabled; set => _disabled = value; }
        public HTMLFormElement? Form { get => _form; set => _form = value; }
        public bool Multiple { get => _multiple; set => _multiple = value; }
        public string Name { get => _name; set => _name = value; }
        public bool Required { get => _required; set => _required = value; }
        public ulong Size { get => _size; set => _size = value; }
        public string Type { get => _type; }
        public List<HTMLOptionElement> Options { get => _options; }
        public ulong Length { get => _length; set => _length = value; }
        public List<Element> SelectedOptions { get => _selectedOptions; }
        public ulong SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }
        public string Value { get => _value; set => _value = value; }
        public List<Node> Labels { get => _labels; }

        // IValidityState Implementation
        private bool _willValidate;
        private ValidityState _validity;
        private string _validationMessage;

        public bool WillValidate { get => _willValidate; set => _willValidate = value; }
        public ValidityState Validity { get => _validity; set => _validity = value; }
        public string ValidationMessage { get => _validationMessage; set => _validationMessage = value; }

        // CONSTRUCTOR
        public HTMLSelectElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public HTMLOptionElement? item(UInt32 index) { throw new NotImplementedException(); }
        public HTMLOptionElement? namedItem(string name) { throw new NotImplementedException(); }
        public void add(Element element, HTMLElement? before = null) { throw new NotImplementedException(); }
        public void remove() { throw new NotImplementedException(); }
        public void remove(Int32 index) { throw new NotImplementedException(); }
        public bool checkValidity() { throw new NotImplementedException(); }
        public bool reportValidity() { throw new NotImplementedException(); }
        public void setCustomValidity(string error) { throw new NotImplementedException(); }
    }
}
