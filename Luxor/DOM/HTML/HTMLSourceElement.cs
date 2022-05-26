namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/embedded-content.html#htmlsourceelement
    public class HTMLSourceElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _src;
        private string _type;
        private string _srcset;
        private string _sizes;
        private string _media;
        private ulong _width;
        private ulong _height;

        // PUBLIC PROPERTIES
        // TODO: HTMLModElement

        // CONSTRUCTOR
        public HTMLSourceElement(Document nodeDocument) : base(nodeDocument) {}
    }
}
