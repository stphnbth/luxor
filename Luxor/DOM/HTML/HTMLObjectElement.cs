namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/iframe-embed-object.html#htmlobjectelement
    public class HTMLObjectElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _data;
        private string _type;
        private string _name;
        private HTMLFormElement? _form;
        private string _width;
        private string _height;
        private Document? contentDocument;
        // private WindowProxy? contentWindow;

        // PUBLIC PROPERTIES
        // TODO: HTMLObjectElement

        // CONSTRUCTOR
        public HTMLObjectElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public Document? getSVGDocument()
        {
            throw new NotImplementedException();
        }
    }
}