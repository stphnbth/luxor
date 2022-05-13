namespace Luxor.DOM.HTML
{
    public class HTMLOptionElement : HTMLElement
    {
        // PRIVATE FIELDS
        private bool _disabled;
        private HTMLFormElement? _form;
        private string _label;
        private bool _defaultSelected;
        private bool _selected;
        private string _value;
        private string _text;
        private long _index;

        // PUBLIC PROPERTIES
        public bool Disabled { get => _disabled; set => _disabled = value; }
        public HTMLFormElement? Form { get => _form; set => _form = value; }
        public string Label { get => _label; set => _label = value; }
        public bool DefaultSelected { get => _defaultSelected; set => _defaultSelected = value; }
        public bool Selected { get => _selected; set => _selected = value; }
        public string Value { get => _value; set => _value = value; }
        public string Text { get => _text; set => _text = value; }
        public long Index { get => _index; set => _index = value; }

        // CONSTRUCTOR
        public HTMLOptionElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public void Option(string value, string text = "", bool? defaultSelected = false, bool? selected = false)
        {
            Text = text;
            Value = value;
            DefaultSelected = defaultSelected.HasValue ? defaultSelected.Value : true;
            Selected = selected.HasValue ? selected.Value : true;
        }
    }
}