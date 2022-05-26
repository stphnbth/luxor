namespace Luxor.DOM
{
    public class StaticRange : AbstractRange
    {
        // CONSTRUCTOR
        private StaticRange(StaticRangeInit init) {}
    }

    public struct StaticRangeInit
    {
        public Node startContainer;
        public ulong startOffset;
        public Node endContainer;
        public ulong endOffset;
    }
}