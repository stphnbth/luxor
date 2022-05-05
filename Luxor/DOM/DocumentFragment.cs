using System.Diagnostics;

namespace Luxor.DOM
{
    public class DocumentFragment : Node
    {
        protected Element Host { get; set; }

        public DocumentFragment(Document? ownerDocument, Element host) : base(ownerDocument) 
        {
            Host = host;
        }
    }
}