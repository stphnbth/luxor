using Luxor.DOM.HTML;

namespace Luxor.DOM.Events
{
    public class OffscreenCanvas : EventTarget
    {
        // PRIVATE FIELDS
        private ulong _width;
        private ulong _height;

        private EventHandler oncontextlost;
        private EventHandler ontextrestored;

        // PUBLIC PROPERTIES
        public ulong Width { get => _width; set => _width = value; }
        public ulong Height { get => _height; set => _height = value; }

        // CONSTRUCTOR
        OffscreenCanvas(ulong width, ulong height) : base() {}

        // PUBLIC METHODS
        public OffscreenRenderingContext? GetContext(OffscreenRenderingContextId contextId, object? options = null)
        {
            throw new NotImplementedException();
        }

        public ImageBitmap TransferToImageBitmap()
        {
            throw new NotImplementedException();
        }

        public async Task<Span<Int64>> ConvertToBlob(ImageEncodeOptions? options = null)
        {
            throw new NotImplementedException();
        }
        
    }

    public abstract class OffscreenRenderingContext {}

    public interface OffscreenCanvasRenderingContext2D :
        CanvasState, CanvasTransform, CanvasCompositing, CanvasImageSmoothing, CanvasFillStrokeStyles,
        CanvasShadowStyles, CanvasFilters, CanvasRect, CanvasDrawPath, CanvasText, CanvasDrawImage,
        CanvasImageData, CanvasPathDrawingStyles, CanvasTextDrawingStyles, CanvasPath
    {
        public OffscreenCanvas Canvas { get; }

        public void commit()
        {
            throw new NotImplementedException();
        }
    }

    public struct ImageEncodeOptions
    {
        public string type = "image/png";
        public double quality = 0;
        public ImageEncodeOptions() {}
    }

    public enum OffscreenRenderingContextId
    {
        twoD,
        bitmaprenderer,
        webgl,
        webgl2,
        webgpu
    }
}