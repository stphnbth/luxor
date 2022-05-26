namespace Luxor.DOM
{
    public interface INonDocumentTypeChildNode
    {
        // PUBLIC PROPERTIES
        public Element? PreviousElementSibling { get; }
        public Element? NextElementSibling { get; }
    }
}