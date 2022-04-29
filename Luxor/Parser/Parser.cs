using Extensions;
using Luxor.DOM;

namespace Luxor.Parser
{
    public class Parser
    {
        private StreamReader _stream;
        private Tokenizer _tokenizer;
        private TreeBuilder _treeBuilder;

        private char _current;
        private char _next;
        
        protected Encoding.Encoding Encoding { get; set; }

        public Parser (StreamReader sr)
        {
            _stream = sr;

            Encoding = new Encoding.Encoding("UTF-8", "irrelevant");
            consume();


            _tokenizer = new Tokenizer();
            _treeBuilder = new TreeBuilder();            

            
        }

        // 13.2.3.5 Preprocessing the Input Stream
        private void consume()
        {
            if (_tokenizer.Reconsume) { return; }

            // if (_next is not null) { _current = _next; }
   
            _next = (char) _stream.Read();
            
            if (Char.IsSurrogate(_next)) { /* surrogate-in-input-stream */ }
            if ((_next).IsNonCharacter()) { /* noncharacter-in-input-stream */ }
            if (Char.IsControl(_next) && !(_next).IsAsciiWhiteSpace() && _next != 0x0000) { /* control-character-in-input-stream */ }

            // normalize newlines
            if (_next == 0x000D)
            {
                if (_stream.Peek() == 0x000A) { _next = (char) _stream.Read(); }
                else { _next = (char) 0x000A; }
            }
        }

        
        
        
        
        
        
        
        
        
        public void run()
        {
            while (!_stream.EndOfStream)
            {
                
                /*
                switch (_mode)
                {
                    case Mode.Initial:
                        // TODO: check for whitespace tokens
                        if (token.Type == Type.Character)
                        {
                            
                        }

                        else if (token.Type == Type.Comment) { insertComment(token); }

                        break;
                    default:
                        break;
                }
                */
            }
        }

        // 13.2.7 The End        
        private void finish() { throw new NotImplementedException(); }
        private void abort() { throw new NotImplementedException(); }

        // 13.2.8 Speculative HTML Parsing
        // TODO: implement speculative parsing

        // TODO: implement error handling?? 
        // 13.2.10 Error Handling and Strange Cases 
        // 13.2.10.1 Misnested Tags: <b><i></b></i>
        // 13.2.10.2 Misnested Tags: <b><p></b></p>
        // 13.2.10.3 Unexpected Markup in Tables
        // 13.2.10.4 Scripts That Modify the Page as It Is Being Parsed
        // 13.2.10.5 The Execution of Scripts That Are Moving Across Multiple Documents
        // 13.2.10.6 Unclosed Formatting Elements

        // 13.3 Serializing HTML Fragments
        private string serializeHTMLFragment(Node node) { throw new NotImplementedException(); }
        private string escapeString(string str) { throw new NotImplementedException(); }
        
        // 13.4 Parsing HTML Fragments
        private List<Node> parseHTMLFragment(Element context, string input) { throw new NotImplementedException(); }


    }
}