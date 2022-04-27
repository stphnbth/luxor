namespace Luxor.DOM
{
    public class HTMLSelectElement : HTMLElement
    {
        protected string Autocomplete { get; set; }
        protected bool Disabled { get; set; }
        protected HTMLFormElement? Form { get; }
        protected bool Multiple { get; set; }
        protected string Name { get; set; }
        protected bool Required { get; set; }
        protected UInt32 Size { get; set; }
        protected string Type { get; }
        protected List<HTMLOptionElement> Options { get; }
        protected UInt32 Length { get; set; }
        protected List<Element> SelectedOptions { get; }
        protected long SelectedIndex { get; set; }
        protected string Value { get; set; }
        protected bool WillValidate { get; }
        protected string Validity { get; }
        protected string ValidationMessage { get; }
        protected List<Node> Labels { get; }
                            
        public HTMLSelectElement() {}

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
