using Extensions;
using Luxor.DOM;

using static Data.DataTables;

namespace Luxor.Parser
{
    public class Parser
    {
        private StreamReader _stream;
        private Tokenizer _tokenizer = new Tokenizer();
        private TreeBuilder _treeBuilder = new TreeBuilder();

        private char _current;
        private char _next;
        
        protected Encoding.Encoding Encoding { get; set; }

        public Parser (StreamReader sr)
        {
            _stream = sr;
            Encoding = new Encoding.Encoding("UTF-8", "irrelevant");

            _current = (char) normalize(_stream.Read());
            _next = (char) normalize(_stream.Read());
        }

        // 13.2.3.5 Preprocessing the Input Stream
        private void consume(string toConsume)
        {
            if (_tokenizer.Reconsume) { return; }

            _current = _next;

            if (!String.IsNullOrEmpty(toConsume))
            {
                for (int i = 0; i < toConsume.Length - 1; i++)
                    _current = (char) normalize(_stream.Read());

                _tokenizer.ToConsume = String.Empty;
            }
            
            _next = (char) normalize(_stream.Read());
            
            /*
            // TODO: error catching during preprocessing??
            if (Char.IsSurrogate(_next)) {  surrogate-in-input-stream  }
            if ((_next).IsNonCharacter()) {  noncharacter-in-input-stream  }
            if (Char.IsControl(_next) && !(_next).IsAsciiWhiteSpace() && _next != 0x0000) {  control-character-in-input-stream  }
            */            
        }

        private Int32 normalize(Int32 cp)
        {
            if (cp == 0x000D)
            {
                if (_stream.Peek() == 0x000A) { cp = _stream.Read(); }
                else { cp = 0x000A; }
            }

            return cp;
        }

        public void run()
        {
            while (!_stream.EndOfStream)
            {
    
                if (_tokenizer.State == State.MarkupDeclarationOpen || _tokenizer.State == State.AfterDOCTYPEName)
                {
                    string lookAhead = new String(_stream.ExposeCharBuffer(8));
                    _tokenizer.step(_current, _next, lookAhead);
                }
                else 
                {
                    _tokenizer.step(_current, _next);
                }

                if (_tokenizer.Emitted is not null)
                {
                    _treeBuilder.dispatch(_tokenizer.Emitted);
                    _tokenizer.Emitted = null;
                }

                consume(_tokenizer.ToConsume);
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