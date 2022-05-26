namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/media.html#htmlvideoelement
    public class HTMLVideoElement : HTMLMediaElement
    {
        // PRIVATE FIELDS
        private ulong _width;
        private ulong _height;
        private ulong _videoWidth;
        private ulong _videoHeight;
        private string _poster;
        private bool _playsInline;

        // PUBLIC PROPERTIES
        // TODO: HTMLVideoElement

        // CONSTRUCTOR
        public HTMLVideoElement(Document nodeDocument) : base(nodeDocument) {}
    }
}