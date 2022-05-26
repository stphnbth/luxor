namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#cdatasection
    public class CDATASection : Text
    {
        public override string NodeName { get => "#cdata-section"; }

        public CDATASection(Document nodeDocument) : base("", nodeDocument) {}
    } 
}

