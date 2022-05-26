using Luxor.DOM.Events;
using Luxor.DOM.Geometry;

namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/canvas.html#htmlcanvaselement
    public class HTMLCanvasElement : HTMLElement
    {
        // PRIVATE FIELDS
        private ulong _width;
        private ulong _height;

        // PUBLIC PROPERTIES
        // TODO: HTMLCanvasElement

        // CONSTRUCTOR
        public HTMLCanvasElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public RenderingContext? GetContext(string contextId, object? options = null)
        {
            throw new NotImplementedException();
        }

        public string ToDataURL(string type = "image/png", object quality)
        {
            throw new NotImplementedException();
        }

        public void ToBlob(BlobCallback _callback, string type = "image/png", object quality)
        {
            throw new NotImplementedException();
        }

        public OffscreenCanvas TransferControlToOffscreen()
        {
            throw new NotImplementedException();
        }
    }

    // TODO: Returning Multiple Canvas Types
    public abstract class RenderingContext {}
    public abstract class HTMLOrSVGImageElement {}
    public abstract class CanvasImageSource {}

    public interface CanvasRenderingContext2D :
        CanvasState, CanvasTransform, CanvasCompositing, CanvasImageSmoothing, CanvasFillStrokeStyles,
        CanvasFilters, CanvasRect, CanvasDrawPath, CanvasUserInterface, CanvasText, CanvasDrawImage,
        CanvasImageData, CanvasPathDrawingStyles, CanvasTextDrawingStyles, CanvasPath
    {
        public HTMLCanvasElement Canvas { get; set; }

        public CanvasRenderingContext2DSettings GetContextAttributes()
        {
            throw new NotImplementedException();
        }
    }

    public interface CanvasState
    {
        public void save()
        {
            throw new NotImplementedException();    
        }
        
        public void restore()
        {
            throw new NotImplementedException();
        }
        
        public void reset()
        {
            throw new NotImplementedException();
        }

        public bool isContextLost()
        {
            throw new NotImplementedException();
        }

    }

    public interface CanvasTransform
    {
        public void scale(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void rotate(double angle)
        {
            throw new NotImplementedException();
        }
        
        public void translate(double x, double y)
        {
            throw new NotImplementedException();
        }
        
        public void transform(double a, double b, double c, double d, double e, double f)
        {
            throw new NotImplementedException();
        }
        
        public DOMMatrix getTransform()
        {
            throw new NotImplementedException();
        }
        
        public void setTransform(double a, double b, double c, double d, double e, double f)
        {
            throw new NotImplementedException();
        }
        
        public void setTransform(DOMMatrix2DInit? transform = null)
        {
            throw new NotImplementedException();
        }
        
        public void resetTransform()
        {
            throw new NotImplementedException();
        }

    }
    
    public interface CanvasCompositing
    {
        public double GlobalAlpha { get; set; }
        public string GlobalCompositingOperation { get; set; }
    }

    public interface CanvasImageSmoothing
    {
        public bool ImageSmoothingEnabled { get; set; }
        public ImageSmoothingQuality ImageSmoothingQuality { get; set; }
    }

    public interface CanvasFillStrokeStyles
    {
        // TODO interface for Fill + Stroke Styles
        public Object StrokeStyle { get; set; }
        public Object FillStyle { get; set; }

        public CanvasGradient createLinearGradient(double x0, double y0, double x1, double y1)
        {
            throw new NotImplementedException();
        }

        public CanvasGradient createRadialGradient(double x0, double y0, double r0, double x1, double y1, double r1)
        {
            throw new NotImplementedException();
        }
        
        public CanvasGradient createConicGradient(double startAngle, double x, double y)
        {
            throw new NotImplementedException();
        }
        
        public CanvasPattern? createPattern(CanvasImageSource image, string repetition)
        {
            throw new NotImplementedException();
        }
    }

    public interface CanvasShadowStyles
    {
        public double ShadowOffsetX { get; set; }
        public double ShadowOffsetY { get; set; }
        public double ShadowBlur { get; set; }
        public string ShadowColor { get; set; }
    }
    
    public interface CanvasFilters
    {
        // TODO: Multiple CanvasFilters Return Types
        public CanvasFilter Filter { get; set; }
    }
    
    public interface CanvasRect
    {
        public void clearRect(double x, double y, double w, double h)
        {
            throw new NotImplementedException();
        }

        public void fillRect(double x, double y, double w, double h)
        {
            throw new NotImplementedException();
        }

        public void strokeRect(double x, double y, double w, double h)
        {
            throw new NotImplementedException();
        }
    }

    public interface CanvasDrawPath
    {
        public void beginPath()
        {
            throw new NotImplementedException();
        }
        
        public void fill(CanvasFillRule fillRule = CanvasFillRule.nonzero)
        {
            throw new NotImplementedException();
        }
        
        public void fill(Path2D path, CanvasFillRule fillRule = CanvasFillRule.nonzero)
        {
            throw new NotImplementedException();
        }
        
        public void stroke()
        {
            throw new NotImplementedException();
        }
        
        public void stroke(Path2D path)
        {
            throw new NotImplementedException();
        }

        public void clip(CanvasFillRule fillRule = CanvasFillRule.nonzero)
        {
            throw new NotImplementedException();
        }
        
        public void clip(Path2D path, CanvasFillRule fillRule = CanvasFillRule.nonzero)
        {
            throw new NotImplementedException();
        }
        
        public bool isPointInPath(double x, double y, CanvasFillRule fillRule = CanvasFillRule.nonzero)
        {
            throw new NotImplementedException();
        }
        
        public bool isPointInPath(Path2D path, double x, double y, CanvasFillRule fillRule = CanvasFillRule.nonzero)
        {
            throw new NotImplementedException();
        }
        
        public bool isPointInStroke(double x, double y)
        {
            throw new NotImplementedException();
        }
        
        public bool isPointInStroke(Path2D path, double x, double y)
        {
            throw new NotImplementedException();
        }

    }
    
    public interface CanvasUserInterface
    {
        public void drawFocusIfNeeded(Element element)
        {
            throw new NotImplementedException();
        }
        
        public void drawFocusIfNeeded(Path2D path, Element element)
        {
            throw new NotImplementedException();
        }
        
        public void scrollPathIntoView()
        {
            throw new NotImplementedException();
        }
        
        public void scrollPathIntoView(Path2D path)
        {
            throw new NotImplementedException();
        }
    }

    public interface CanvasText
    {
        public void fillText(string text, double x, double y, double maxWidth)
        {
            throw new NotImplementedException();
        }

        public void strokeText(string text, double x, double y, double maxWidth)
        {
            throw new NotImplementedException();
        }
        
        public TextMetrics measureText(string text)
        {
            throw new NotImplementedException();
        }
        
    }

    public interface CanvasDrawImage
    {
        public void drawImage(CanvasImageSource image, double dx, double dy)
        {
            throw new NotImplementedException();
        }

        public void drawImage(CanvasImageSource image, double dx, double dy, double dw, double dh)
        {
            throw new NotImplementedException();
        }
        
        public void drawImage(CanvasImageSource image, double sx, double sy, double sw, double sh, double dx, double dy, double dw, double dh)
        {
            throw new NotImplementedException();
        }
        
    }

    public interface CanvasImageData
    {
        public ImageData createImageData(long sw, long sh, ImageDataSettings? settings = null)
        {
            throw new NotImplementedException();
        }

        public ImageData createImageData(ImageData imagedata)
        {
            throw new NotImplementedException();
        }
        
        public ImageData getImageData(long sx, long sy, long sw, long sh, ImageDataSettings? settings = null)
        {
            throw new NotImplementedException();
        }

        public void putImageData(ImageData imagedata, long dx, long dy)
        {
            throw new NotImplementedException();
        }
        
        public void putImageData(ImageData imagedata, long dx, long dy, long dirtyX, long dirtyY, long dirtyWidth, long dirtyHeight)
        {
            throw new NotImplementedException();
        }
        
    }

    public interface CanvasPathDrawingStyles
    {
        public double LineWidth { get; set; }
        public CanvasLineCap LineCap { get; set; }
        public CanvasLineJoin LineJoin { get; set; }
        public double MiterLimit { get; set; }
        public double LineDashOffset { get; set; }

        public void setLineDash(IEnumerator<double> segments)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<double> getLineDash()
        {
            throw new NotImplementedException();
        }
    }

    public interface CanvasTextDrawingStyles
    {
        public string Font { get; set; }
        public CanvasTextAlign TextAlign { get; set; }
        public CanvasTextBaseline TextBaseline { get; set; }
        public CanvasDirection Direction { get; set; }
        public string LetterSpacing { get; set; }
        public CanvasFontKerning FontKerning { get; set; }
        public CanvasFontStretch FontStretch { get; set; }
        public CanvasFontVariantCaps FontVariantCaps { get; set; }
        public CanvasTextRendering TextRendering { get; set; }
        public string WordSpacing { get; set; }
    }

    public interface CanvasPath
    {
        public void ClosePath()
        {
            throw new NotImplementedException();
        }

        public void MoveTo(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void LineTo(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void QuadraticCurveTo(double cpx, double cpy, double x, double y)
        {
            throw new NotImplementedException();
        }

        public void BezierCurveTo(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y)
        {
            throw new NotImplementedException();
        }

        public void ArcTo(double x1, double y1, double x2, double y2, double radius)
        {
            throw new NotImplementedException();
        }

        public void Rect(double x, double y, double w, double h)
        {
            throw new NotImplementedException();
        }

        public void RoundRect(double x, double y, double w, double h, double radii = 0)
        {
            throw new NotImplementedException();
        }

        public void Arc(double x, double y, double radius, double startAngle, double endAngle, bool counterclockwise = false)
        {
            throw new NotImplementedException();
        }

        public void Ellipse(double x, double y, double radiusX, double radiusY, double rotation, double startAngle, double endAngle, bool counterclockwise = false)
        {
            throw new NotImplementedException();
        }
    }

    public interface TextMetrics
    {
        public double Width { get; }
        public double ActualBoundingBoxLeft { get; }
        public double ActualBoundingBoxRight { get; }
        public double FontBoundingBoxAscent { get; }
        public double FontBoundingBoxDescent { get; }
        public double ActualBoundingBoxAscent { get; }
        public double ActualBoundingBoxDescent { get; }
        public double EmHeightAscent { get; }
        public double EmHeightDescent { get; }
        public double HangingBaseline { get; }
        public double AlphabeticBaseline { get; }
        public double IdeographicBaseline { get; }
    }

    public interface CanvasGradient
    {
        public void AddColorStop(double offset, string color)
        {
            throw new NotImplementedException();
        }
    }
    
    public interface CanvasPattern
    {
        public void SetTransform(DOMMatrix2DInit? transform = null)
        {
            throw new NotImplementedException();
        }
    }

    // TODO: Canvas Filter
    public interface CanvasFilter {}

    public interface Path2D : CanvasPath
    {
        public void AddPath(Path2D path, DOMMatrix2DInit? transform = null)
        {
            throw new NotImplementedException();
        }
    }

    public interface ImageData
    {
        public ulong Width { get; }
        public ulong Height { get; }
        public List<ushort> Data { get; }
        public PredefinedColorSpace ColorSpace { get; }
    }

    public interface ImageBitmapRenderingContext
    {
        public HTMLCanvasElement Canvas { get; }
    }

    public struct CanvasRenderingContext2DSettings
    {
        public bool alpha = true;
        public bool desynchronized = false;
        public PredefinedColorSpace colorSpace = PredefinedColorSpace.srgb;
        public bool willReadFrequently = false;
        public CanvasRenderingContext2DSettings() {}
    }

    public struct ImageDataSettings
    {
        PredefinedColorSpace colorSpace;
    }

    public struct ImageBitmapRenderingContextSettings
    {
        public bool alpha = true;
        public ImageBitmapRenderingContextSettings() {}
    }

    public enum PredefinedColorSpace
    {
        srgb,
        displayP3
    }

    public enum CanvasFillRule
    {
        nonzero,
        evenodd
    }

    public enum ImageSmoothingQuality
    {
        low,
        medium,
        high
    }

    public enum CanvasLineCap
    { 
        butt,
        round,
        square
    }

    public enum CanvasLineJoin
    {
        round,
        bevel,
        miter
    }

    public enum CanvasTextAlign
    {
        start,
        end,
        left,
        right,
        center
    }
    
    public enum CanvasTextBaseline 
    { 
        top,
        hanging,
        middle,
        alphabetic,
        ideographic,
        bottom
    }
    
    public enum CanvasDirection 
    { 
        ltr,
        rtl,
        inherit
    }

    public enum CanvasFontKerning 
    {
        auto,
        normal,
        none
    }

    public enum CanvasFontStretch 
    { 
        ultracondensed,
        extracondensed,
        condensed,
        semicondensed,
        normal,
        semiexpanded,
        expanded,
        extraexpanded,
        ultraexpanded
    }
    
    public enum CanvasFontVariantCaps 
    { 
        normal,
        smallcaps,
        allsmallcaps,
        petitecaps,
        allpetitecaps,
        unicase,
        titlingcaps
    }
    
    public enum CanvasTextRendering 
    { 
        auto,
        optimizeSpeed,
        optimizeLegibility,
        geometricPrecision
    }
}