namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#comment
    public class Comment : CharacterData
    {
        public override string NodeName { get => "#comment"; }

        public Comment(Document nodeDocument) : base("", nodeDocument) {}

    }
}

