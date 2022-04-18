using System.IO;
using System.Text;

using static Reference.DataTables;
using Luxor.DOM;

namespace Luxor
{
    public class Parser
    {
        private Mode _mode;
        private Mode _orignalMode;
        private Stack<Mode> _templateModes;
        private Document _document;
        private StreamReader _stream;
        private Tokenizer _tokenizer;

        private Stack<Element> _openElements = new Stack<Element>();
        private Element? _currentNode;
        private Element? _adjustedCurrentNode;

        private List<Element> _activeFormattingElements = new List<Element>();

        private Element? _head;
        private Element? _form;
        
        private bool _framsetOK;
        private bool _pause;
        private bool _scripting;
        private int _scriptNestingLevel;
        
        protected Encoding Encoding { get; }
        protected string Confidence { get; }

        public Parser (StreamReader sr)
        {
            _mode = Mode.Initial;
            _document  = new Document();
            _stream = sr;
            
            _framsetOK = false;
            _pause = false;
            _scripting = false;
            _scriptNestingLevel = 0;

            (Encoding, Confidence) = determineEncoding();
        }

        // 13.2.3.1 Parsing with a Known Character Encoding
        // 13.2.3.2 Determining the Character Encoding
        private (Encoding, string) determineEncoding()
        {
            // TODO: add option for other encodings
            return (Encoding.UTF8, "irrelevant");            
        }

        private void prescanByteStream(Int32 size) { throw new NotImplementedException(); }

        private void getAttribute() { throw new NotImplementedException(); }

        private string? getXMLEncoding(Stream stream) 
        {
            // 1 encodingPosition = stream.Postion
            // 2
            Span<byte> buffer = new byte[5];
            byte[] xmlSequence = {0x3C, 0x3F, 0x78, 0x6D, 0x6C};

            stream.Read(buffer);
            if (buffer.Length < 5 || buffer.ToArray() != xmlSequence) { return null; }

            // 3
               
            // 4
            byte[] encodingSequence = {0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67};
            buffer = new byte[7];

            while (buffer != encodingSequence)
            {
                if (!stream.CanRead) { return null; }

                // buffer[buffer.Length] = stream.ReadByte();

            }


            // 5    

            // 6    
            // 7
            // 8    
            // 9   
            // 10   
            // 11    
            // 12    
            // 13    
            // 14    
            // 15    
            // 16    
            // 17
            // 18 

            return "";

        }

        // 12.2.3.4 Changing the Encoding While Parsing
        private void changeEncoding() { throw new NotImplementedException(); }

        // 13.2.3.5 Preprocessing the Input Stream
        private char preprocess(Int32 next)
        {
            // TODO: check for surrogate, non-char and control char parse errors
            
            /*
            if (surrogates.contains(next)) { surrogate-in-input-stream }
            if (nonchars.contains(next)) { noncharacter-in-input-stream }
            if (controls.contains(next)) { control-character-in-input-stream }
            */

            // normalize newlines
            if (next == 0x000D)
            {
                if (_stream.Peek() == 0x000A) { next = _stream.Read(); }
                else { next = 0x000A; }
            }

            return (char) next;
        }

        // 13.2.4.1 The Insertion Mode
        private void resetMode() { throw new NotImplementedException(); }

        // 13.2.4.2 The Stack of Open Elements
        private void getScope() { throw new NotImplementedException(); }

        // 13.2.4.3 The List of Active Formatting Elements
        private void pushToActiveFormattingElements() { throw new NotImplementedException(); }
        private void reconstructActiveFormattingElements() { throw new NotImplementedException(); }
        private void clearToLastMarker() { throw new NotImplementedException(); }

        // 13.2.6 Tree Construction
        private void dispatch(Token token)
        {
            // if the stack of open elements is empty
            if (_openElements.Count == 0)
            {

            }

            // if the adjusted current node is an element in the HTML namespace
            if (_adjustedCurrentNode.NamespaceURI == "http://www.w3.org/1999/xhtml")
            {

            }

            // if the adjusted current node is a MathML text integration point and the token is a start tag whose tag name is neither "mglyph" nor "malignmark"
            // if the adjusted current node is a MathML text integration point and the token is a character token
            // if the adjusted current node is a MathML annotation-xml element and the token is a start tag whose tag name is "svg"
            // if the adjusted current node is an HTML integration point and the token is a start tag
            // if the adjusted current node is an HTML integration point and the token is a character token
            // if the token is an end-of-file token
    
        }

        // 13.2.6.1 Creating and Inserting Nodes
        private void insert() { throw new NotImplementedException(); }
        private Element createElementForToken(string nspace, Element intendedParent) { throw new NotImplementedException(); }
        private void insertForeignElement(string nspace, Token token) { throw new NotImplementedException(); }

        private void insertHTMLElement(Token token) {
            insertForeignElement("http://www.w3.org/1999/xhtml", token);
        }

        private void adjustMathMLAttribute(Token token) { throw new NotImplementedException(); }
        private void adjustSVGAttribute(Token token) { throw new NotImplementedException(); }
        private void adjustForeignAttribute(Token token) { throw new NotImplementedException(); }
        private void insertCharacter() { throw new NotImplementedException(); }
        private void insertComment() { throw new NotImplementedException(); }

        // 13.2.6.2 Parsing Elements That Contain Only Text
        private void insertText(Token token, State nextState)
        {
            // 1
            insertHTMLElement(token);

            // 2
            _tokenizer.State = nextState;
            
            // 3
            _orignalMode = _mode;

            // 4
            _mode = Mode.Text;
        } 

        // 13.2.6.3 Closing Elements That Have Implied End Tags

        // 13.2.6.4 The Rules for Parsing Tokens in HTML Content
        // 13.2.6.5 The Rules for Parsing Tokens in Foreign Content        
        public void run()
        {
            while (!_stream.EndOfStream)
            {
                Token token =  new Token(Type.DOCTYPE); // _tokenizer.GetToken(preprocess(_stream.Read()));

                switch (_mode)
                {
                    case Mode.Initial:
                        // TODO: check for whitespace tokens
                        if (token.Type == Type.Character)
                        {
                            
                        }

                        else if (token.Type == Type.Comment) { /* insertComment(token); */ }

                        break;
                    default:
                        break;
                }
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