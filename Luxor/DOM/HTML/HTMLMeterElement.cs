namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/form-elements.html#htmlprogresselement
    public class HTMLMeterElement : HTMLElement
    {
        // PRIVATE FIELDS
        private double _value;
        private double _min;
        private double _max;
        private double _low;
        private double _high;
        private double _optimum;
        private List<Node> _labels;

        // PUBLIC PROPERTIES
        // TODO: HTMLMeterElement

        // CONSTRUCTOR
        public HTMLMeterElement(Document nodeDocument) : base(nodeDocument) {}
    }
}