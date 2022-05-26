namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/image-maps.html#htmlmapelement
    public class HTMLMapElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _name;
        private List<string> _areas;

        // PUBLIC PROPERTIES
        // TODO: HTMLMapElement

        // CONSTRUCTOR
        public HTMLMapElement(Document nodeDocument) : base(nodeDocument) {}
    }
}