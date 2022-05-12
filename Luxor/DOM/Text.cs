using System.Diagnostics;

namespace Luxor.DOM
{
    public class Text : CharacterData
    {
        // PUBLIC METHODS
        public string WholeText { get => throw new NotImplementedException(); }

        // PUBLIC OVERRIDES
        public override string NodeName { get => "#text";}

        // CONSTRUCTOR
        public Text(string str, Document nodeDocument) : base(str, nodeDocument) {}

        // PUBLIC METHODS
        public Text SplitText(int offset)
        {
            throw new NotImplementedException();
        }
    } 
}

