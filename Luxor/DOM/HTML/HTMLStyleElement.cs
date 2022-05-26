namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/semantics.html#the-style-element
    public class HTMLStyleElement : HTMLElement
    {
        // PRIVATE FIELDS
        private bool _disabled;
        private string _media;
        private List<string> blocking;

        // PUBLIC PROPERTIES
        // TODO: HTMLStyleElement

        // CONSTRUCTOR
        public HTMLStyleElement(Document nodeDocument) : base(nodeDocument) {}
    }
}