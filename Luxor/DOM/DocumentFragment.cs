using System.Diagnostics;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#documentfragment
    public class DocumentFragment : Node, INonElementParentNode, IParentNode
    {
        // PUBLIC OVERRIDES
        public override string NodeName { get => "#document-fragment"; }
        public override string? TextContent { get => GetDescendantText(); set => StringReplaceAll(value); }

        // CONSTRUCTOR
        public DocumentFragment(Document nodeDocument) : base(nodeDocument) {}
    }
}