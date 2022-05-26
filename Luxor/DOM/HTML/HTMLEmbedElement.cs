namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/iframe-embed-object.html#htmlembedelement
    public class HTMLEmbedElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _src;
        private string _type;
        private string _width;
        private string _height;

        // PUBLIC PROPERTIES
        // TODO: HTMLEmbedElement

        // CONSTRUCTOR
        public HTMLEmbedElement(Document nodeDocument) : base(nodeDocument) {}


        // PUBLIC METHODS
        Document? getSVGDocument()
        {
            throw new NotImplementedException();
        }
    }
}