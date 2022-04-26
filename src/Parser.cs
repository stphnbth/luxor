using System;
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
        private Mode _currentTemplateMode;
        private Document _document;
        private StreamReader _stream;
        private Tokenizer _tokenizer;

        private Stack<Element> _openElements = new Stack<Element>();
        private Element _firstOpenElement;
        private Element? _currentNode;
        private Element? _adjustedCurrentNode;

        private List<Element> _activeFormattingElements = new List<Element>();

        private Element? _head;
        private Element? _form;
        
        private bool _framsetOK;
        private bool _pause;
        private bool _scripting;
        private int _scriptNestingLevel;
        private int _insertionPoint;

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
        
        protected string Encoding { get; set; }
        protected string Confidence { get; set; }

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

        // TODO: determineEncodings - allow for encodings other than UTF-8
        private (string, string) determineEncoding()
        {
            return ("UTF-8", "irrelevant");            
        }

        // TODO: Parser.prescanByteStream - figure out how to gracefully exit all the searching while loops
        // TODO: Parser.prescanByteStream - update alphanumeric and whitespace searches to something concise and consistent with Tokenizer
        // end condition is automatically 1024 bytes
        private string? prescanByteStream() 
        {
            // 1
            // string? fallbackEncoding = null;

            // 2
            byte[] bytes = _stream.ExposeByteBuffer();
            int position = 0;

            // 3
            ArraySegment<Byte> byteSegment = new ArraySegment<Byte>(bytes);

            byte[] littleEndian = {0x3C, 0x0, 0x3F, 0x0, 0x78, 0x0};
            if (byteSegment.Slice(position, littleEndian.Length).ToArray() == littleEndian)
            {
                return "UTF-16LE";
            }

            byte[] bigEndian = {0x0, 0x3C, 0x0, 0x3F, 0x0, 0x78};
            if (byteSegment.Slice(position, bigEndian.Length).ToArray() == bigEndian)
            {
                return "UTF-16BE";
            }

            // 4
            Loop:
                
                if (bytes[position] == 0x3C)
                {
                    position++;
                    
                    if (byteSegment.Slice(position, 3).ToArray() == new byte[] {0x21, 0x2D, 0x2D})
                    {
                        position++;
                        byte[] sequence = {0x2D, 0x2D, 0x3E};

                        while (byteSegment.Slice(position, sequence.Length) != sequence && position < bytes.Length) { position++; }
                        position += 2;
                    }
                    else if (bytes[position] == 0x4D || bytes[position] == 0x6D)
                    {
                        List<byte> codepoints = new List<byte> {0x09, 0x0A, 0x0C, 0x0D, 0x20, 0x2F}; 
                        if (
                            (bytes[position + 1] == 0x45 || bytes[position + 1] == 0x65) &&
                            (bytes[position + 2] == 0x54 || bytes[position + 2] == 0x74) &&
                            (bytes[position + 3] == 0x41 || bytes[position + 3] == 0x61) &&
                            codepoints.Contains(bytes[position + 4])
                           )
                        {
                            // 4.1.1
                            position += 4;

                            // 4.1.2
                            List<string> attributeList = new List<string>();

                            // 4.1.3
                            bool gotPragma = false;

                            // 4.1.4
                            bool? needPragma = null;

                            // 4.1.5
                            string? charset = null;

                            // 4.1.6 + 4.1.10
                            (string name, string value) attr = getAttribute(bytes, position);
                            while (attr != ("", "")) 
                            {
                                // 4.1.7
                                if (attributeList.Contains(attr.name)) { continue; }

                                // 4.1.8
                                attributeList.Append(attr.name);

                                // 4.1.9
                                if (attr.name == "http-equiv") 
                                { 
                                    gotPragma = true; 
                                }
                                else if (attr.name == "content") 
                                {
                                    // TODO: implement the algorithm for extracting a character encoding from a meta element
                                    charset = new NotImplementedException().Message;
                                    needPragma = true;
                                }
                                else if (attr.name == "charset")
                                {
                                    charset = getEncodingFromLabel(attr.value);
                                    needPragma = true;
                                }
                            }

                            // 4.1.11 + 4.1.12 + 4.1.13
                            if (needPragma is null || (needPragma == true && !gotPragma) || charset is null)
                            {
                                goto NextByte;
                            } 

                            // 4.1.14
                            if (charset == "UTF-16BE/LE")
                            {
                                charset = "UTF-8";
                            }

                            // 4.1.15
                            if (charset == "x-user-defined")
                            {
                                charset = "windows-1252";
                            }

                            return charset;
                        }
                    }
                    // TODO: this is wrong...
                    else if (bytes[position] == 0x2F)
                    {
                        if ((bytes[position + 1] > 0x40 && bytes[position + 1] < 0x5B) || (bytes[position + 1] > 0x60 && bytes[position + 1] < 0x7B))
                        {
                            position += 3;
                            List<byte> codepoints = new List<byte> {0x09, 0x0A, 0x0C, 0x0D, 0x20, 0x3E};

                            // 4.2.1
                            while (!codepoints.Contains(bytes[position]) && position < bytes.Length) { position++; }

                            // 4.2.2
                            (string, string)? attribute = getAttribute(bytes, position);
                            while (attribute != ("", "")) { attribute = getAttribute(bytes, position); }

                            goto NextByte;
                        }
                    }
                    else if (bytes[position] == 0x21 || bytes[position] == 0x2F || bytes[position] == 0x3F)
                    {
                        while (bytes[position] != 0x3E && position < bytes.Length) { position++; }
                    }
                    
                }

            // 5
            NextByte:
                position++;
                goto Loop;

        }

        // TODO: Parser.getAttribute - figure out how to gracefully exit all the searching while loops
        // TODO: Parser.getAttribute - update uppercase alpha searches to something consistent with Tokenizer
        private (string, string) getAttribute(byte[] bytes, int position) 
        {
            // 1
            List<byte> pre = new List<byte> {0x09, 0x0A, 0x0C, 0x0D, 0x20, 0x2F};
            List<byte> wspace = pre.GetRange(0, 5);

            while (pre.Contains(bytes[position]) && position < bytes.Length) { position++; }

            // 2
            if (bytes[position] == 0x3E) { return ("", ""); }

            // 3
            StringBuilder attributeName, attributeValue;
            attributeName = attributeValue = new StringBuilder();

            
            while (position < bytes.Length)
            {
                // 4
                if (bytes[position] == 0x3D && attributeName.Length > 0)
                {
                    goto Value;
                }
                else if (wspace.Contains(bytes[position]))
                {
                    goto Spaces;
                }
                else if (bytes[position] == 0x2F || bytes[position] == 0x3E)
                {
                    return (attributeName.ToString(), "");
                }
                else if (bytes[position] > 0x40 && bytes[position] < 0x5B)
                {
                    attributeName.Append(0x20 + bytes[position]);
                }
                else
                {
                    attributeName.Append(bytes[position]);
                }

                // 5
                position++;
            }

            // 6
            Spaces:
                while (wspace.Contains(bytes[position]) && position < bytes.Length) { position++; }

            // 7
            if (bytes[position] != 0x3D) { return (attributeName.ToString(), ""); }
            
            // 8
            position++;

            // 9
            Value:
                while (wspace.Contains(bytes[position]) && position < bytes.Length) { position++; }

            // 10
            if (bytes[position] == 0x22 || bytes[position] == 0x27)
            {
                // 10.1
                byte b = bytes[position];

                // 10.2
                QuoteLoop:
                    position++;

                // 10.3
                if (b == bytes[position]) 
                { 
                    return (attributeName.ToString(), attributeValue.ToString()); 
                }

                // 10.4
                else if (bytes[position] > 0x40 && bytes[position] < 0x5B)
                {
                    attributeValue.Append(0x20 + bytes[position]);
                }

                // 10.5
                else
                {
                    attributeValue.Append(bytes[position]);
                }
                
                // 10.6
                goto QuoteLoop;
            }
            else if (bytes[position] == 0x3E)
            {
                return (attributeName.ToString(), "");
            }
            else if (bytes[position] > 0x40 && bytes[position] < 0x5B)
            {
                attributeValue.Append(0x20 + bytes[position]);
                position++;
            }
            else
            {
                attributeValue.Append(bytes[position]);
                position++;
            }

            while (position < bytes.Length)
            {
                // 11
                if (wspace.Contains(bytes[position]))
                {
                    return (attributeName.ToString(), attributeValue.ToString());
                }
                else if (bytes[position] > 0x40 && bytes[position] < 0x5B)
                {
                    attributeValue.Append(0x20 + bytes[position]);
                }
                else
                {
                    attributeValue.Append(bytes[position]);
                }

                // 12
                position++;
            }
            
            return (attributeName.ToString(), attributeValue.ToString());
        }

        // TODO: Parser.getXMLEncoding - implement an Encoding class
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
            string? encoding = "UTF-8"; // getEncodingFromLabel(Encoding.UTF8.GetChars(potentialEncoding));
            if (encoding is null) { return null; }

            // 17
            if (encoding == "UTF-16BE/LE") { encoding = "UTF-8"; }

            // 18
            return encoding;

        }

        private string? getEncodingFromLabel(string str) => getEncodingFromLabel(str.ToCharArray());
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

        // 13.2.3.4 Changing the Encoding While Parsing
        private void changeEncoding(string newEncoding) {  
            // 1
            if (Encoding.Equals("UTF-16BE/LE"))
            {
                Confidence = "certain";
                return;
            }

            // 2
            if (newEncoding.Equals("UTF-16BE/LE")) { Encoding = "UTF-8"; }

            // 3
            if (newEncoding.Equals("x-user-defined")) { Encoding = "windows-1252"; }

            // 4
            if (newEncoding.Equals(Encoding)) 
            {
                Confidence = "certain";
                return;
            }

            // 5
            // TODO: Implement a Decoder class

            // 6
        }

        // 13.2.3.5 Preprocessing the Input Stream
        private char preprocess(Int32 next)
        {
            
            if (Char.IsSurrogate((char) next)) { /* surrogate-in-input-stream */ }
            if (((char) next).IsNonCharacter()) { /* noncharacter-in-input-stream */ }
            if (Char.IsControl((char) next) && !((char)next).IsAsciiWhiteSpace() && next != 0x0000) { /* control-character-in-input-stream */ }

            // normalize newlines
            if (next == 0x000D)
            {
                if (_stream.Peek() == 0x000A) { next = _stream.Read(); }
                else { next = 0x000A; }
            }

            return (char) next;
        }

        // 13.2.4.1 The Insertion Mode
        private void resetMode() 
        {
            // 1
            bool last = false;

            // 2
            Node node = _openElements.Peek();

            // 3
            Loop:
                if (node)

            // 4
                // 4.1

                // 4.2

                // 4.3

                // 4.4

                // 4.5

                // 4.6

                // 4.7

                // 4.8


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

        }

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