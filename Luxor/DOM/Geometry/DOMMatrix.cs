namespace Luxor.DOM.Geometry
{
    public abstract class DOMMatrix
    {

    }

    public struct DOMMatrix2DInit {
        public double a;
        public double b;
        public double c;
        public double d;
        public double e;
        public double f;
        public double m11;
        public double m12;
        public double m21;
        public double m22;
        public double m41;
        public double m42;
    };

    public struct DOMMatrixInit {
        public DOMMatrix2DInit init2D;
        public double m13 = 0;
        public double m14 = 0;
        public double m23 = 0;
        public double m24 = 0;
        public double m31 = 0;
        public double m32 = 0;
        public double m33 = 1;
        public double m34 = 0;
        public double m43 = 0;
        public double m44 = 1;
        public bool is2D;
        public DOMMatrixInit() { init2D = new DOMMatrix2DInit(); is2D = true; }
};
}