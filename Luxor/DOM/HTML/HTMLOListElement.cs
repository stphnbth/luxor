namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/grouping-content.html#htmlolistelement
    public class HTMLOListElement : HTMLElement
    {
        // PRIVATE FIELDS
        private bool _reversed;
        private long _start;
        private string _type;

        // PUBLIC PROPERTIES
        // TODO: HTMLOListElement

        // CONSTRUCTOR
        public HTMLOListElement(Document nodeDocument) : base(nodeDocument) {}
    }
}