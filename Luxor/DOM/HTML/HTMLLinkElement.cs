namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/semantics.html#htmllinkelement
    public class HTMLLinkElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _href;
        private string _crossOrigin;
        private string _rel;
        private string _as;
        private List<string> _relList;
        private string _media;
        private string _integrity;
        private string _hreflang;
        private string _type;
        private List<string> _sizes;
        private string _imageSrcset;
        private string _imageSizes;
        private string _referrerPolicy;
        private List<string> blocking;
        private bool _disabled;

        // PUBLIC PROPERTIES
        // TODO: HTMLLinkElement


        // CONSTRUCTOR
        public HTMLLinkElement(Document nodeDocument) : base(nodeDocument) {}
    }
}