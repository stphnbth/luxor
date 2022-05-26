namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/embedded-content.html#htmlimageelement
    public class HTMLImageElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _alt;
        private string _src;
        private string _srcset;
        private string _sizes;
        private string _crossOrigin;
        private string _useMap;
        private string _isMap;
        private ulong _width;
        private ulong _height;
        private ulong _naturalWidth;
        private ulong _naturalHeight;
        private bool _complete;
        private string _currentSrc;
        private string _referrerPolicy;
        private string _decoding;
        private string _loading;

        // PUBLIC PROPERTIES
        // TODO: HTMLModElement

        // Promise<undefined> decode();

        // CONSTRUCTOR
        public HTMLImageElement(Document nodeDocument) : base(nodeDocument) {}
    }
}