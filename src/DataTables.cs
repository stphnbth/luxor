using System.Text;
using System.Text.Json;

namespace Reference
{
    public class DataTables
    {
        public static readonly Dictionary<string, (List<Int32>, string)> entities;
        public static readonly Dictionary<Int32, string> codes;

        public static readonly List<Int32> asciiWhitespace;
        public static readonly List<Int32> asciiDigit;
        public static readonly List<Int32> asciiAlphaUpper;
        public static readonly List<State> DoctypeEOFs;
        public static readonly List<State> CommentEOFs;

        public static readonly List<Int32> commonWhitespace;
        public static readonly List<Int32> asciiAlphaLower;
        public static readonly List<Int32> asciiAlpha;
        public static readonly List<Int32> asciiAlphanumeric;
        public static readonly List<Int32> asciiUpperHexDigit;
        public static readonly List<Int32> asciiLowerHexDigit;
        public static readonly List<Int32> asciiHexDigit;

        static DataTables()
        {
            byte[] bytes;
            Utf8JsonReader reader;

            string file, path;
            string dir = "src/data";
               
            // ENTITIES
            file = "entities.json";
            path = Path.Combine(dir, file);

            entities = new Dictionary<string, (List<Int32>, string)>();
            
            bytes = File.ReadAllBytes(path);
            reader = new Utf8JsonReader(bytes);
            reader.Read();
            reader.Read();

            List<Int32> codepoints = new List<Int32>();
            string? literal, chr;
            
            while (true)
            {            
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                // PROPERTY NAME
                literal = reader.GetString();
                reader.Read();

                // START OBJECT
                reader.Read();

                // PROPERTY NAME -> CODEPOINTS
                reader.Read();

                // START ARRAY
                reader.Read();

                // NUMBER
                while (reader.TokenType == JsonTokenType.Number)
                {
                    codepoints.Add(reader.GetInt32());
                    reader.Read();
                }

                // END ARRAY
                reader.Read();

                // PROPERTY NAME -> CHARACTERS
                reader.Read(); 

                // STRING
                chr = reader.GetString();
                reader.Read();

                // END OBJECT
                reader.Read();

                entities.Add(literal!, (codepoints, chr)!);
            }           

            // END STATE CODES
            file = "codes.json";
            path = Path.Combine(dir, file);

            codes = new Dictionary<int, string>();

            bytes = File.ReadAllBytes(path);
            reader = new Utf8JsonReader(bytes);
            reader.Read();
            reader.Read();

            int codepoint;            
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                // PROPERTY NAME
                codepoint = Int32.Parse(reader.GetString()!);
                reader.Read();

                // STRING
                chr = reader.GetString();
                reader.Read();

                codes.Add(codepoint, chr!);
            }

            // ASCIIS
            file = "asciis.json";
            path = Path.Combine(dir, file);

            bytes = File.ReadAllBytes(path);
            reader = new Utf8JsonReader(bytes);
            reader.Read();
            reader.Read();

            int count = 0;
            List<Int32>[] asciis = { new List<Int32>(), new List<Int32>(), new List<Int32>() };
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                // PROPERTY NAME 
                reader.Read();
                reader.Read();

                // NUMBER
                while (reader.TokenType == JsonTokenType.Number)
                {
                    asciis[count].Add(reader.GetInt32());
                    reader.Read();
                }
                
                reader.Read();
                count++;
            }

            asciiWhitespace = asciis[0];
            asciiAlphaUpper = asciis[1];
            asciiDigit = asciis[2];

            commonWhitespace = asciiWhitespace.Where(x => x != 13).ToList<Int32>();
            asciiAlphaLower = asciiAlphaUpper.Select(x => x + 32).ToList<Int32>();
            asciiAlpha = asciiAlphaUpper.Concat(asciiAlphaLower).ToList<Int32>();
            asciiAlphanumeric = asciiAlpha.Concat(asciiDigit!).ToList<Int32>();
            asciiUpperHexDigit = asciiAlphaUpper.Where(x => x < 71).ToList<Int32>();
            asciiLowerHexDigit = asciiAlphaLower.Where(x => x < 103).ToList<Int32>();
            asciiHexDigit = asciiUpperHexDigit.Concat(asciiLowerHexDigit).ToList<Int32>();
            
            // EOFS
            file = "eofs.json";
            path = Path.Combine(dir, file);

            bytes = File.ReadAllBytes(path);
            reader = new Utf8JsonReader(bytes);
            reader.Read();
            reader.Read();

            count = 0;
            List<State>[] eofs = { new List<State>(), new List<State>() };
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                // PROPERTY NAME
                reader.Read();
                reader.Read();

                // NUMBER
                while (reader.TokenType == JsonTokenType.String)
                {
                    eofs[count].Add(Enum.Parse<State>(reader.GetString()!));
                    reader.Read();
                }
                
                reader.Read();
                count++;
            }

            DoctypeEOFs = eofs[0];
            CommentEOFs = eofs[1];
        }

        public enum State {
            Data, 
            RCDATA,
            RAWTEXT,
            ScriptData,
            PLAINTEXT,
            TagOpen,
            EndTagOpen,
            TagName,
            RCDATALT,
            RCDATAEndTagOpen,
            RCDATAEndTagName,
            RAWTEXTLT,
            RAWTEXTEndTagOpen,
            RAWTEXTEndTagName,
            ScriptDataLT,
            ScriptDataEndTagOpen,
            ScriptDataEndTagName,
            ScriptDataEscapeStart,
            ScriptDataEscapeStartDash,
            ScriptDataEscaped,
            ScriptDataEscapedDash,
            ScriptDataEscapedDashDash,
            ScriptDataEscapedLT,
            ScriptDataEscapedEndTagOpen,
            ScriptDataEscapedEndTagName,
            ScriptDataDoubleEscapeStart,
            ScriptDataDoubleEscaped,
            ScriptDataDoubleEscapedDash,
            ScriptDataDoubleEscapedDashDash,
            ScriptDataDoubleEscapedLT,
            ScriptDataDoubleEscapeEnd,
            BeforeAttributeName,
            AttributeName,
            AfterAttributeName,
            BeforeAttributeValue,
            AttributeValueDQ,
            AttributeValueSQ,
            AttributeValueUQ,
            AfterAttributeValueQ,
            SelfClosingStartTag,
            BogusComment,
            MarkupDeclarationOpen,
            CommentStart,
            CommentStartDash,
            Comment,
            CommentLT,
            CommentLTBang,
            CommentLTBangDash,
            CommentLTBangDashDash,
            CommentEndDash,
            CommentEnd,
            CommentEndBang,
            DOCTYPE,
            BeforeDOCTYPEName,
            DOCTYPEName,
            AfterDOCTYPEName,
            AfterDOCTYPEPubKey,
            BeforeDOCTYPEPubId,
            DOCTYPEPubIdDQ,
            DOCTYPEPubIdSQ,
            AfterDOCTYPEPubId,
            BetweenDOCTYPEPubSysIds,
            AfterDOCTYPESysKey,
            BeforeDOCTYPESysId,
            DOCTYPESysIdDQ,
            DOCTYPESysIdSQ,
            AfterDOCTYPESysId,
            BogusDOCTYPE,
            CDATASection,
            CDATASectionBracket,
            CDATASectionEnd,
            CharRef,
            NamedCharRef,
            AmbiguousAmpersand,
            NumCharRef,
            HexCharRefStart,
            DecCharRefStart,
            HexCharRef,
            DecCharRef,
            NumCharRefEnd
        }

        public enum Mode {
            Initial,
            BeforeHTML,
            BeforeHead,
            Head,
            HeadNoScript,
            AfterHead,
            Body,
            Text,
            Table,
            TableText,
            Caption,
            ColumnGroup,
            TableBody,
            Row,
            Cell,
            Select,
            SelectInTable,
            Template,
            AfterBody,
            Frameset,
            AfterFrameset,
            AfterAfterBody,
            AfterAfterFrameset
        }

        public enum Errors {
            AbruptClosingEmptyComment,
            AbruptDOCTYPEPubId,
            AbruptDOCTYPESysId,
            AbsenceDigitsNumCharRef,
            CDATAHTMLContent,
            CharRefOutsideUnicodeRange,
            ControlCharInputStream,
            ControlCharRef,
            EndTagWithAttributes,
            DuplicateAttribute,
            EndTagTrailingSolidus,
            EOFBeforeTagName,
            EOFInCDATA,
            EOFInComment,
            EOFInDoctype,
            EOFInScriptHTMLCommentLikeText,
            EOFInTag,
            IncorrectlyClosedComment,
            IncorrectlyOpenedComment,
            InvalidCharSeqAfterDOCTYPEName,
            InvalidFirstCharTagName,
            MissingAttributeValue,
            MissingDOCTYPEName,
            MissingDOCTYPEPubId,
            MissingDOCTYPESysId,
            MissingEndTagName,
            MissingQuoteBeforeDOCTYPEPubId,
            MissingQuoteBeforeDOCTYPESysId,
            MissingSemicolonAfterCharRef,
            MissingWhitespaceAfterDOCTYPEPubKey,
            MissingWhitespaceAfterDOCTYPESysKey,
            MissingWhitespaceBeforeDOCTYPEName,
            MissingWhitespaceBetweenAttributes,
            MissingWhitespaceBetweenDOCTYPEPubSysIds,
            NestedComment,
            NonCharCharRef,
            NonCharInputStream,
            NonVoidHTMLElementStartTagTrailingSolidus,
            NullCharRef,
            SurrogateCharRef,
            SurrogateInputStream,
            UnexpectedCharAfterDOCTYPESysId,
            UnexpectedCharInAttributeName,
            UnexpectedCharInUQAttributeValue,
            UnexpectedEqualsSignBeforeAttributeName,
            UnexpectedNullChar,
            UnexpectedSolidusTag,
            UnknownNamedCharRef
        }
    }
}
