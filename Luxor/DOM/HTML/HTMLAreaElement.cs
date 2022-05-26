namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/image-maps.html#htmlareaelement
    public class HTMLAreaElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _alt;
        private string _coords;
        private string _shape;
        private string _target;
        private string _download;
        private string _ping;
        private string _rel;
        private List<string> _relList;
        private string _referrerPolicy;

        // PUBLIC PROPERTIES
        // TODO: HTMLAreaElement

        // CONSTRUCTOR
        public HTMLAreaElement(Document nodeDocument) : base(nodeDocument) {}
    }
}