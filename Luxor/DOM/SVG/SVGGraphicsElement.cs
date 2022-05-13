namespace Luxor.DOM.SVG
{
    public class SVGGraphicsElement : SVGElement
    {
        // PRIVATE FIELDS
        private List<SVGAnimatedTransform> _transform;

        // PUBLIC PROPERTIES
        public List<SVGAnimatedTransform> Transform { get => _transform; }

        // PUBLIC METHODS
        /*
            // GEOMETRY //
        private DOMRect getBBox(SVGBoundingBoxOptions options = {})
        private DOMMatrix? getCTM();
        private DOMMatrix? getScreenCTM();
        */

        // CONSTRUCTOR
        public SVGGraphicsElement(Document nodeDocument) : base(nodeDocument) {}
    }
}