namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/semantics.html#htmlmetaelement
    public class HTMLMetaElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _name;
        private string _httpEquiv;
        private string _content;
        private string _media;

        // PUBLIC PROPERTIES
        // TODO: HTMLMetaElement

        // CONSTRUCTOR
        public HTMLMetaElement(Document nodeDocument) : base(nodeDocument) {}
    }
}