namespace Luxor.DOM.HTML
{
    // TODO: HTMLBaseElement
    // https://html.spec.whatwg.org/multipage/semantics.html#htmlbaseelement
    public class HTMLBaseElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _href;
        private string _target;

        // PUBLIC PROPERTIES
        public string Href { get => _href; }
        public string Target { get => _target; }

        // CONSTRUCTOR
        public HTMLBaseElement(Document nodeDocument) : base(nodeDocument) {}
    }
}