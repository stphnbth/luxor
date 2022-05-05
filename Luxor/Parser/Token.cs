using System.Text;

namespace Luxor.Parser
{
    public class Token {
        private TokenType _tokenType;

        public Token(TokenType tokenType) => _tokenType = tokenType;
        public TokenType TokenType { get => _tokenType; }
    }

    // DOCTYPE
    public class Doctype : Token 
    {
        private StringBuilder _name;
        private StringBuilder? _pub;
        private StringBuilder? _sys;
        private bool forceQuirks = false;

        public Doctype() : base(TokenType.DOCTYPE) => _name = new StringBuilder();
        public Doctype(string name) : base(TokenType.DOCTYPE) => _name = new StringBuilder(name);
        public Doctype(Int32 name) : base(TokenType.DOCTYPE) => _name = new StringBuilder(new String((char) name, 1));

        public StringBuilder Name { get => _name; }
        public StringBuilder Pub { get => _pub!; set => _pub = value; }
        public StringBuilder Sys { get => _sys!; set => _sys = value; }
        public bool ForceQuirks { get => forceQuirks; set => forceQuirks = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\tName: ");
            return sb.Append(Name.ToString()).ToString();
        }
    }
        
    // START TAG & END TAG
    public class Tag : Token
    {
        private bool _selfClosing = false;
        private StringBuilder _name;
        List<(StringBuilder name, StringBuilder value)> _attributes = new List<(StringBuilder, StringBuilder)>();

        public Tag(TokenType type) : base(type) => _name = new StringBuilder();
        public Tag(TokenType type, string name) : base(type) => _name = new StringBuilder(name);
        public Tag(TokenType type, Int32 name) : base(type) => _name = new StringBuilder(new String((char) name, 1));

        public bool SelfClosing { get => _selfClosing; set => _selfClosing = value; }
        public StringBuilder Name { get => _name; }
        public List<(StringBuilder name, StringBuilder value)> Attributes { get => _attributes; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\tName: ");
            return sb.Append(Name.ToString()).ToString();
        }
    }

    // CHARACTER & COMMENT
    public class Text : Token
    {
        private StringBuilder _data;

        public Text(TokenType type) : base(type) => _data = new StringBuilder();
        public Text(TokenType type, string data) : base(type) => _data = new StringBuilder(data);
        public Text(TokenType type, Int32 data) : base(type) => _data = new StringBuilder(new String((char) data, 1));

        public StringBuilder Data { get => _data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\tData: ");
            return sb.Append(Data.ToString()).ToString();
        }
    }

    // EOF
    public class EndOfFile : Token
    {
        private Int32 _bit;

        public EndOfFile(TokenType type, Int32 bit) : base(type)
        {
            _bit = bit;
        }
    }

    public enum TokenType {
        DOCTYPE,
        StartTag,
        EndTag,
        Comment,
        Character,
        EndOfFile
    }
}
