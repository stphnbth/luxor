namespace Luxor.DOM
{
    public class Attr : Node 
    {
        public Attr(Document ownerDocument) : base(ownerDocument) {}

        protected string LocalName { get; }
        protected string Name { get; }
        protected string? NamespaceURI { get; }
        protected Element? OwnerElement { get; }
        protected string? Prefix { get; }
        protected string Value { get; }

        /*
        public override bool Equals(object? obj)
        {         
            if (obj is not null && obj is Attr)
                return Equals((Attr) obj);
            
            return false;
        }

        public bool Equals(Attr otherAttr)
        {
            if (otherAttr is null)
                return false;
 
            return this.Name!.Equals(otherAttr.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value);
        }
        */
    }

}