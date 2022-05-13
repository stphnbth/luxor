namespace Luxor.DOM.SVG
{
    public class SVGSVGElement : SVGGraphicsElement
    {
        // PRIVATE FIELDS
        private int _x;
        private int _y;
        private int _width;
        private int _height;

        // TODO: SVGAnimatedLength

        private float _currentScale;
        // private DOMPointReadOnly _currentTranslate;

        // PUBLIC PROPERTIES
        public int X { get => _x; }
        public int Y { get => _y; }
        public int Width { get => _width; }
        public int Height { get => _height; }

        // PUBLIC METHODS
        /*
        public List<Node> getIntersectionList(DOMRectReadOnly rect, SVGElement? referenceElement)
        public List<Node> getEnclosureList(DOMRectReadOnly rect, SVGElement? referenceElement)
        public bool checkIntersection(SVGElement element, DOMRectReadOnly rect)
        public bool checkEnclosure(SVGElement element, DOMRectReadOnly rect)
        public void deselectAll()
        public SVGNumber createSVGNumber()
        public SVGLength createSVGLength()
        public SVGAngle createSVGAngle()
        public DOMPoint createSVGPoint()
        public DOMMatrix createSVGMatrix()
        public DOMRect createSVGRect()
        public SVGTransform createSVGTransform()
        public SVGTransform createSVGTransformFromMatrix(DOMMatrix2DInit matrix = {})
        public Element getElementById(DOMString elementId)
        */

        // CONSTRUCTOR
        public SVGSVGElement(Document nodeDocument) : base(nodeDocument) {}
    }
}