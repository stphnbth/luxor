namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/semantics.html#htmlanchorelement
    public class HTMLAnchorElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _target;
        private string _download;
        private string _ping;
        private string _rel;
        private List<string> _relList;
        private string _hreflang;
        private string _type;
        private string _text;
        private string _referrerPolicy;

        // PUBLIC PROPERTIES
        // TODO: HTMLAnchorElement

        // CONSTRUCTOR
        public HTMLAnchorElement(Document nodeDocument) : base(nodeDocument) {}
    }
}