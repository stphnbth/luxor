using System.Text;

using static Reference.DataTables;
using Extensions;
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

        private static Dictionary<string, string> _encodings = new Dictionary<string, string>{  	
            { "unicode-1-1-utf-8", "UTF-8" },
            { "unicode11utf8", "UTF-8" },
            { "unicode20utf8", "UTF-8" },
            { "utf-8", "UTF-8" },
            { "utf8", "UTF-8" },
            { "x-unicode20utf8", "UTF-8" },
            { "866", "IBM866" },
            { "cp866", "IBM866" },
            { "csibm866", "IBM866" },
            { "ibm866", "IBM866" },
            { "csisolatin2", "ISO-8859-2" },
            { "iso-8859-2", "ISO-8859-2" },
            { "iso-ir-101", "ISO-8859-2" },
            { "iso8859-2", "ISO-8859-2" },
            { "iso88592", "ISO-8859-2" },
            { "iso_8859-2", "ISO-8859-2" },
            { "iso_8859-2:1987", "ISO-8859-2" },
            { "l2", "ISO-8859-2" },
            { "latin2", "ISO-8859-2" },
            { "csisolatin3", "ISO-8859-3" },
            { "iso-8859-3", "ISO-8859-3" },
            { "iso-ir-109", "ISO-8859-3" },
            { "iso8859-3", "ISO-8859-3" },
            { "iso88593", "ISO-8859-3" },
            { "iso_8859-3", "ISO-8859-3" },
            { "iso_8859-3:1988", "ISO-8859-3" },
            { "l3", "ISO-8859-3" },
            { "latin3", "ISO-8859-3" },
            { "csisolatin4", "ISO-8859-4" },
            { "iso-8859-4", "ISO-8859-4" },
            { "iso-ir-110", "ISO-8859-4" },
            { "iso8859-4", "ISO-8859-4" },
            { "iso88594", "ISO-8859-4" },
            { "iso_8859-4", "ISO-8859-4" },
            { "iso_8859-4:1988", "ISO-8859-4" },
            { "l4", "ISO-8859-4" },
            { "latin4", "ISO-8859-4" },
            { "csisolatincyrillic", "ISO-8859-5" },
            { "cyrillic", "ISO-8859-5" },
            { "iso-8859-5", "ISO-8859-5" },
            { "iso-ir-144", "ISO-8859-5" },
            { "iso8859-5", "ISO-8859-5" },
            { "iso88595", "ISO-8859-5" },
            { "iso_8859-5", "ISO-8859-5" },
            { "iso_8859-5:1988", "ISO-8859-5" },
            { "arabic", "ISO-8859-6" },
            { "asmo-708", "ISO-8859-6" },
            { "csiso88596e", "ISO-8859-6" },
            { "csiso88596i", "ISO-8859-6" },
            { "csisolatinarabic", "ISO-8859-6" },
            { "ecma-114", "ISO-8859-6" },
            { "iso-8859-6", "ISO-8859-6" },
            { "iso-8859-6-e", "ISO-8859-6" },
            { "iso-8859-6-i", "ISO-8859-6" },
            { "iso-ir-127", "ISO-8859-6" },
            { "iso8859-6", "ISO-8859-6" },
            { "iso88596", "ISO-8859-6" },
            { "iso_8859-6", "ISO-8859-6" },
            { "iso_8859-6:1987", "ISO-8859-6" },
            { "csisolatingreek", "ISO-8859-7" },
            { "ecma-118", "ISO-8859-7" },
            { "elot_928", "ISO-8859-7" },
            { "greek", "ISO-8859-7" },
            { "greek8", "ISO-8859-7" },
            { "iso-8859-7", "ISO-8859-7" },
            { "iso-ir-126", "ISO-8859-7" },
            { "iso8859-7", "ISO-8859-7" },
            { "iso88597", "ISO-8859-7" },
            { "iso_8859-7", "ISO-8859-7" },
            { "iso_8859-7:1987", "ISO-8859-7" },
            { "sun_eu_greek", "ISO-8859-7" },
            { "csiso88598e", "ISO-8859-8" },
            { "csisolatinhebrew", "ISO-8859-8" },
            { "hebrew", "ISO-8859-8" },
            { "iso-8859-8", "ISO-8859-8" },
            { "iso-8859-8-e", "ISO-8859-8" },
            { "iso-ir-138", "ISO-8859-8" },
            { "iso8859-8", "ISO-8859-8" },
            { "iso88598", "ISO-8859-8" },
            { "iso_8859-8", "ISO-8859-8" },
            { "iso_8859-8:1988", "ISO-8859-8" },
            { "visual", "ISO-8859-8" },
            { "csiso88598i", "ISO-8859-8-I" },
            { "iso-8859-8-i", "ISO-8859-8-I" },
            { "logical", "ISO-8859-8-I" },
            { "csisolatin6", "ISO-8859-10" },
            { "iso-8859-10", "ISO-8859-10" },
            { "iso-ir-157", "ISO-8859-10" },
            { "iso8859-10", "ISO-8859-10" },
            { "iso885910", "ISO-8859-10" },
            { "l6", "ISO-8859-10" },
            { "latin6", "ISO-8859-10" },
            { "iso-8859-13", "ISO-8859-13" },
            { "iso8859-13", "ISO-8859-13" },
            { "iso885913", "ISO-8859-13" },
            { "iso-8859-14", "ISO-8859-14" },
            { "iso8859-14", "ISO-8859-14" },
            { "iso885914", "ISO-8859-14" },
            { "csisolatin9", "ISO-8859-15" },
            { "iso-8859-15", "ISO-8859-15" },
            { "iso8859-15", "ISO-8859-15" },
            { "iso885915", "ISO-8859-15" },
            { "iso_8859-15", "ISO-8859-15" },
            { "l9", "ISO-8859-15" },
            { "iso-8859-16", "ISO-8859-16" },
            { "cskoi8r", "KOI8-R" },
            { "koi", "KOI8-R" },
            { "koi8", "KOI8-R" },
            { "koi8-r", "KOI8-R" },
            { "koi8_r", "KOI8-R" },
            { "koi8-ru", "KOI8-U" },
            { "koi8-u", "KOI8-U" },
            { "csmacintosh", "macintosh" },
            { "mac", "macintosh" },
            { "macintosh", "macintosh" },
            { "x-mac-roman", "macintosh" },
            { "dos-874", "windows-874" },
            { "iso-8859-11", "windows-874" },
            { "iso8859-11", "windows-874" },
            { "iso885911", "windows-874" },
            { "tis-620", "windows-874" },
            { "windows-874", "windows-874" },
            { "cp1250", "windows-1250" },
            { "windows-1250", "windows-1250" },
            { "x-cp1250", "windows-1250" },
            { "cp1251", "windows-1251" },
            { "windows-1251", "windows-1251" },
            { "x-cp1251", "windows-1251" },
            { "ansi_x3.4-1968", "windows-1252" },
            { "ascii", "windows-1252" },
            { "cp1252", "windows-1252" },
            { "cp819", "windows-1252" },
            { "csisolatin1", "windows-1252" },
            { "ibm819", "windows-1252" },
            { "iso-8859-1", "windows-1252" },
            { "iso-ir-100", "windows-1252" },
            { "iso8859-1", "windows-1252" },
            { "iso88591", "windows-1252" },
            { "iso_8859-1", "windows-1252" },
            { "iso_8859-1:1987", "windows-1252" },
            { "l1", "windows-1252" },
            { "latin1", "windows-1252" },
            { "us-ascii", "windows-1252" },
            { "windows-1252", "windows-1252" },
            { "x-cp1252", "windows-1252" },
            { "cp1253", "windows-1253" },
            { "windows-1253", "windows-1253" },
            { "x-cp1253", "windows-1253" },
            { "cp1254", "windows-1254" },
            { "csisolatin5", "windows-1254" },
            { "iso-8859-9", "windows-1254" },
            { "iso-ir-148", "windows-1254" },
            { "iso8859-9", "windows-1254" },
            { "iso88599", "windows-1254" },
            { "iso_8859-9", "windows-1254" },
            { "iso_8859-9:1989", "windows-1254" },
            { "l5", "windows-1254" },
            { "latin5", "windows-1254" },
            { "windows-1254", "windows-1254" },
            { "x-cp1254", "windows-1254" },
            { "cp1255", "windows-1255" },
            { "windows-1255", "windows-1255" },
            { "x-cp1255", "windows-1255" },
            { "cp1256", "windows-1256" },
            { "windows-1256", "windows-1256" },
            { "x-cp1256", "windows-1256" },
            { "cp1257", "windows-1257" },
            { "windows-1257", "windows-1257" },
            { "x-cp1257", "windows-1257" },
            { "cp1258", "windows-1258" },
            { "windows-1258", "windows-1258" },
            { "x-cp1258", "windows-1258" },
            { "x-mac-cyrillic", "x-mac-cyrillic" },
            { "x-mac-ukrainian", "x-mac-cyrillic" },
            { "chinese", "GBK" },
            { "csgb2312", "GBK" },
            { "csiso58gb231280", "GBK" },
            { "gb2312", "GBK" },
            { "gb_2312", "GBK" },
            { "gb_2312-80", "GBK" },
            { "gbk", "GBK" },
            { "iso-ir-58", "GBK" },
            { "x-gbk", "GBK" },
            { "gb18030", "gb18030" },
            { "big5", "Big5" },
            { "big5-hkscs", "Big5" },
            { "cn-big5", "Big5" },
            { "csbig5", "Big5" },
            { "x-x-big5", "Big5" },
            { "cseucpkdfmtjapanese", "EUC-JP" },
            { "euc-jp", "EUC-JP" },
            { "x-euc-jp", "EUC-JP" },
            { "csiso2022jp", "ISO-2022-JP" },
            { "iso-2022-jp", "ISO-2022-JP" },
            { "csshiftjis", "Shift_JIS" },
            { "ms932", "Shift_JIS" },
            { "ms_kanji", "Shift_JIS" },
            { "shift-jis", "Shift_JIS" },
            { "shift_jis", "Shift_JIS" },
            { "sjis", "Shift_JIS" },
            { "windows-31j", "Shift_JIS" },
            { "x-sjis", "Shift_JIS" },
            { "cseuckr", "EUC-KR" },
            { "csksc56011987", "EUC-KR" },
            { "euc-kr", "EUC-KR" },
            { "iso-ir-149", "EUC-KR" },
            { "korean", "EUC-KR" },
            { "ks_c_5601-1987", "EUC-KR" },
            { "ks_c_5601-1989", "EUC-KR" },
            { "ksc5601", "EUC-KR" },
            { "ksc_5601", "EUC-KR" },
            { "windows-949", "EUC-KR" },
            { "csiso2022kr", "replacement" },
            { "hz-gb-2312", "replacement" },
            { "iso-2022-cn", "replacement" },
            { "iso-2022-cn-ext", "replacement" },
            { "iso-2022-kr", "replacement" },
            { "replacement", "replacement" },
            { "unicodefffe", "UTF-16BE" },
            { "utf-16be", "UTF-16BE" },
            { "csunicode", "UTF-16LE" },
            { "iso-10646-ucs-2", "UTF-16LE" },
            { "ucs-2", "UTF-16LE" },
            { "unicode", "UTF-16LE" },
            { "unicodefeff", "UTF-16LE" },
            { "utf-16", "UTF-16LE" },
            { "utf-16le", "UTF-16LE" },
            { "x-user-defined", "x-user-defined" },
        };
        
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
            // TODO: add an encodings class
            return (Encoding.UTF8, "irrelevant");            
        }

        private void prescanByteStream(Int32 size) { throw new NotImplementedException(); }

        private void getAttribute() { throw new NotImplementedException(); }

        public string? getXMLEncoding(byte[] bytes) 
        {
            // 1 
            int encodingPosition = 0;

            // 2
            ArraySegment<Byte> byteSegment = new ArraySegment<Byte>(bytes);
            byte[] xmlSequence = {0x3C, 0x3F, 0x78, 0x6D, 0x6C};
            
            if (byteSegment.Slice(encodingPosition, xmlSequence.Length).ToArray() != xmlSequence) { return null; }
            encodingPosition += xmlSequence.Length;

            // 3
            int? xmlDeclarationEnd = null;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3E)
                {
                    xmlDeclarationEnd = i;
                    break;
                }
            }

            if (xmlDeclarationEnd is null) { return null; }
               
            // 4 + 5
            byte[] encodingSequence = {0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67};
            bool encodingFlag = false;
            for (encodingPosition += 0; encodingPosition < bytes.Length - encodingSequence.Length; encodingPosition++)
            {
                byte[] byteSlice = byteSegment.Slice(encodingPosition, encodingSequence.Length).ToArray(); 
                if (byteSlice == encodingSequence)
                {
                    encodingFlag = true;
                    encodingPosition -= encodingSequence.Length - 1;

                    break;
                }
            }

            if (!encodingFlag) { return null; }

            // 6
            while (bytes[encodingPosition] <= 0x20) { encodingPosition += 1; }

            // 7
            if (bytes[encodingPosition] != 0x3D) { return null; }

            // 8
            encodingPosition += 1;

            // 9
             while (bytes[encodingPosition] <= 0x20) { encodingPosition += 1; }

            // 10
            byte quoteMark = bytes[encodingPosition];

            // 11
            if (quoteMark != 0x22 || quoteMark != 0x27) { return null; }

            // 12
            encodingPosition += 1;

            // 13
            int encodingEndPosition = encodingPosition;
            while (bytes[encodingEndPosition] != quoteMark)
            {
                if (encodingEndPosition == bytes.Length - 1) { return null; }
                encodingEndPosition += 1;
            }

            // 14
            byte[] potentialEncoding = byteSegment.Slice(encodingPosition, encodingEndPosition - encodingPosition).ToArray();

            // 15
            List<byte> ranges = new List<int>(Enumerable.Range(0x00, 0x20)).Select(x => (byte) x).ToList();
            for (int i = 0; i < potentialEncoding.Length; i++)
            {
                if (ranges.Contains(potentialEncoding[i])) { return null; }
            }

            // 16
            string? encoding = getEncodingFromLabel(Encoding.UTF8.GetChars(potentialEncoding));
            if (encoding is null) { return null; }

            // 17
            if (encoding == "UTF-16BE/LE") { encoding = "UTF-8"; }

            // 18
            return encoding;

        }

        private string? getEncodingFromLabel(char[] chars)
        {
            // 1
            string label = string.Concat((new string(chars)).Where(c => !c.IsAsciiWhiteSpace()));

            // 2
            string? encoding;
            try { encoding = _encodings[label]; }
            catch (KeyNotFoundException) { encoding = null; }
            
            return encoding;
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