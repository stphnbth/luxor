namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmlprogresselement
    public class HTMLProgressElement : HTMLElement
    {
        // PRIVATE FIELDS
        private double _value;
        private double _max;
        private double _position;
        private List<Node> _labels;

        // PUBLIC PROPERTIES
        // TODO: HTMLProgressElement

        // CONSTRUCTOR
        public HTMLProgressElement(Document nodeDocument) : base(nodeDocument) {}
    }
}