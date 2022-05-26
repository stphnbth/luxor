using System.Diagnostics;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#abstractrange
    public abstract class AbstractRange
    {
        // PRIVATE FIELDS
        private Node _startContainer;
        private ulong _startOffset;
        private Node _endContainer;
        private ulong _endOffset;
        private bool _collapsed;

        // PUBLIC PROPERTIES
        public Node StartContainer { get => _startContainer; }
        public ulong StartOffset { get => _startOffset; }
        public Node EndContainer { get => _endContainer; }
        public ulong EndOffset { get => _endOffset; }
        public bool Collapsed { get => _collapsed; }

        // CONSTRUCTOR
        private protected AbstractRange() {}
    }
}