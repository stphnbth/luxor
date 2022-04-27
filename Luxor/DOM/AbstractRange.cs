using System.Diagnostics;

namespace Luxor.DOM
{
    public abstract class AbstractRange
    {
        protected AbstractRange(Node startContainer, int startOffset, Node endContainer,
                    int endOffset, bool collapsed)
        {
            StartContainer = startContainer;
            StartOffset = startOffset;
            EndContainer = endContainer;
            EndOffset = endOffset;
            Collapsed = collapsed;
        }

        public Node StartContainer { get; }
        public int StartOffset { get; }
        public Node EndContainer { get; }
        public int EndOffset { get; }
        public bool Collapsed { get; }
    } 
}