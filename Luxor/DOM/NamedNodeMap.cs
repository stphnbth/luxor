namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#namednodemap
    public class NamedNodeMap : List<Attr>
    {
        // PUBLIC PROPERTIES
        public ulong Length { get => (ulong) base.Count; }

        // CONSTRUCTOR
        public NamedNodeMap() : base() {}

        // PUBLIC METHODS
        public Attr? Item(ulong index)
        {
            throw new NotImplementedException();
        }

        public Attr? GetNamedItem(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        public Attr? GetNamedItemNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        public Attr? SetNamedItem(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        public Attr? SetNamedItemNS(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        public Attr RemoveNamedItem(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        public Attr RemoveNamedItemNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
    }
}