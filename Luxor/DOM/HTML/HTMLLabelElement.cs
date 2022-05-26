namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/forms.html#htmllabelelement
    public class HTMLLabelElement : HTMLElement
    {
        // PRIVATE FIELDS
        private HTMLFormElement _form;
        private string _htmlFor;
        private HTMLElement? _control;

        // PUBLIC PROPERTIES
        // TODO: HTMLLabelElement

        // CONSTRUCTOR
        public HTMLLabelElement(Document nodeDocument) : base(nodeDocument) {}
    }
}