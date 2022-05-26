namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/iframe-embed-object.html#htmliframeelement
    public class HTMLIFrameElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _src;
        private string _srcdoc;
        private string _name;
        private List<string> sandbox;
        private string _allow;
        private bool _allowFullscreen;
        private string _width;
        private string _height;
        private string _referrerPolicy;
        private string _loading;
        private Document? contentDocument;
        // private WindowProxy? contentWindow;

        // PUBLIC PROPERTIES
        // TODO: HTMLIFrameElement

        // CONSTRUCTOR
        public HTMLIFrameElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        Document? getSVGDocument()
        {
            throw new NotImplementedException();
        }
    }
}