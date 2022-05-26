namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/media.html#the-track-element
    public class HTMLTrackElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _kind;
        private string _src;
        private string _srclang;
        private string _label;
        private bool _default;
        private ushort _readyState;
        private TextTrack track;

        // PUBLIC PROPERTIES
        // TODO: HTMLTrackElement

        // CONSTRUCTOR
        public HTMLTrackElement(Document nodeDocument) : base(nodeDocument) {}
    }
}