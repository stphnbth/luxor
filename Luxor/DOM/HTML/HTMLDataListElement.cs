namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmldatalistelement
    public class HTMLDataListElement : HTMLElement
    {
        // PRIVATE FIELDS
        private List<HTMLOptionElement> _options;

        // PUBLIC PROPERTIES
        // TODO: HTMLDataListElement

        // CONSTRUCTOR
        public HTMLDataListElement(Document nodeDocument) : base(nodeDocument) {}
    }
}