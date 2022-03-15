using System.Diagnostics;

namespace Luxor.DOM
{
    public class Text : CharacterData
    {        
        public Text() : base () {}

        public string WholeText { get => throw new NotImplementedException(); }

        public Text SplitText(int offset)
        {
            throw new NotImplementedException();
        }
    } 
}

