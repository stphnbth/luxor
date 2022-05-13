namespace Luxor.DOM.SVG
{
    public class SVGElement : Element
    {
        // PRIVATE FIELDS
        private SVGSVGElement _ownerSVGElement;
        private SVGElement _viewportElement;

        // PUBLIC PROPERTIES
        public SVGSVGElement OwnerSVGElement { get => _ownerSVGElement; set => _ownerSVGElement = value; }
        public SVGElement ViewportElement { get => _viewportElement; set => _viewportElement = value; }

        // CONSTRUCTOR
        public SVGElement(Document nodeDocument) : base(nodeDocument) {}
    }
}