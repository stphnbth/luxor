using Luxor.DOM.HTML.Scripting;
using Luxor.Security;

namespace Luxor.DOM.HTML.Browser
{
    public struct PolicyContainer
    {
        public List<string> CSPList = new List<string>();
        public EmbedderPolicy embedderPolicy = new EmbedderPolicy();
        public ReferrerPolicy referrerPolicy = ReferrerPolicy.StrictOriginWhenCrossOrigin;
        public PolicyContainer() {}
    }
}