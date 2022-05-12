using System.Diagnostics;

namespace Luxor.DOM
{
    public class CDATASection : Text
    {
        public override string NodeName { get => "#cdata-section"; }

        public CDATASection() : base () {}
    } 
}

