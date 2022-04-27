using System.Text;

namespace Luxor.Parser
{
    public class Token {
        private Type _type;

        public Token(Type type) => _type = type;
        public Type Type { get => _type; }

        public void PrintToken()
        {
            StringBuilder ptstr = new StringBuilder(Type.ToString().ToUpper());
            ptstr.Append(this.ToString());
            System.Diagnostics.Debug.WriteLine(ptstr);
        }
    }

    // DOCTYPE
    public class Doctype : Token 
    {
        private StringBuilder _name;
        private StringBuilder? _pub;
        private StringBuilder? _sys;
        private bool forceQuirks = false;

        public Doctype() : base(Type.DOCTYPE) => _name = new StringBuilder();
        public Doctype(string name) : base(Type.DOCTYPE) => _name = new StringBuilder(name);
        public Doctype(Int32 name) : base(Type.DOCTYPE) => _name = new StringBuilder(new String((char) name, 1));

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

        public Tag(Type type) : base(type) => _name = new StringBuilder();
        public Tag(Type type, string name) : base(type) => _name = new StringBuilder(name);
        public Tag(Type type, Int32 name) : base(type) => _name = new StringBuilder(new String((char) name, 1));

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

        public Text(Type type) : base(type) => _data = new StringBuilder();
        public Text(Type type, string data) : base(type) => _data = new StringBuilder(data);
        public Text(Type type, Int32 data) : base(type) => _data = new StringBuilder(new String((char) data, 1));

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

        public EndOfFile(Type type, Int32 bit) : base(type)
        {
            _bit = bit;
        }
    }

    public enum Type {
        DOCTYPE,
        StartTag,
        EndTag,
        Comment,
        Character,
        EndOfFile
    }
}
