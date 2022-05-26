namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/semantics.html#htmltitleelement
    public class HTMLTitleElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _text;

        // PUBLIC PROPERTIES
        public string Text
        {
            get => _text;
            set { StringReplaceAll(value); }
        }

        // CONSTRUCTOR
        public HTMLTitleElement(Document nodeDocument) : base(nodeDocument) {}
    }
}