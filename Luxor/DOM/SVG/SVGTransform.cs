namespace Luxor.DOM.SVG
{
    public class SVGTransform
    {
        // PRIVATE FIELDS
        private ushort _unknown;
        private ushort _matrix;
        private ushort _translate;
        private ushort _scale;
        private ushort _rotate;
        private ushort _skewX;
        private ushort _skewY;

        private ushort _type;
        private float _angle;

        // private DOMMatrix _matrix;

        // PUBLIC PROPERTIES
        public ushort Unknown { get => _unknown; set => _unknown = value; }
        public ushort Matrix { get => _matrix; set => _matrix = value; }
        public ushort Translate { get => _translate; set => _translate = value; }
        public ushort Scale { get => _scale; set => _scale = value; }
        public ushort Rotate { get => _rotate; set => _rotate = value; }
        public ushort SkewX { get => _skewX; set => _skewX = value; }
        public ushort SkewY { get => _skewY; set => _skewY = value; }

        public ushort Type { get => _type; }
        public float Angle { get => _angle; }


        // PUBLIC METHODS
        /*
        public SVGTransform createSVGTransformFromMatrix(DOMMatrix2DInit matrix = {})
        public SVGTransform? consolidate()
        */

        // CONSTRUCTOR
        public SVGTransform() {}
    }

    public struct SVGAnimatedTransform
    {
        SVGTransform baseVal, animVal;

        public SVGAnimatedTransform(SVGTransform baseVal, SVGTransform animVal)
        {
            this.baseVal = baseVal;
            this.animVal = animVal;
        }
    }
}