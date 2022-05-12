using System.Diagnostics;

namespace Luxor.DOM
{
    public class DocumentFragment : Node
    {
        // PRIVATE FIELDS
        private Element _host;

        // INTERNAL PROPERTIES
        internal Element Host { get => _host; set => _host = value; }

        // PUBLIC OVERRIDES
        public override string NodeName { get => "#document-fragment"; }
        public override string? TextContent { get => GetDescendantText(); set => StringReplaceAll(value); }

        // CONSTRUCTOR
        public DocumentFragment(Document nodeDocument, Element host) : base(nodeDocument)
        {
            _host = host;
        }
    }
}