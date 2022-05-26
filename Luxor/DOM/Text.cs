namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#text
    public class Text : CharacterData, ISlottable
    {
        // PUBLIC PROPERTIES
        public string WholeText { get => throw new NotImplementedException(); }

        // PUBLIC OVERRIDES
        public override string NodeName { get => "#text";}

        // CONSTRUCTOR
        public Text(string data = "", Document nodeDocument) : base(data, nodeDocument) {}

        // PUBLIC METHODS
        public Text SplitText(int offset)
        {
            throw new NotImplementedException();
        }
    } 
}

