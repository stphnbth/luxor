using System.Globalization;
using System.Text;

using static Data.DataTables;

namespace Luxor.Parser
{
    public class Tokenizer
    {                
        private State _state;
        private State _return;
        private Queue<char> _buffer;
        private int _charRefCode;
        private bool _reconsume;
        private string _toConsume;

        private Token? _emitted;        

        private Doctype? _doctype;
        private Tag? _currentTag;
        private Tag? _lastEmittedStartTag;
        private Text? _comment;

        private static Text ExclamationToken = new Text(TokenType.Character, 0x0021);
        private static EndOfFile EndOfFileToken = new EndOfFile(TokenType.EndOfFile, 0xFFFF);
        private static Text GreaterThanToken = new Text(TokenType.Character, 0x003E);
        private static Text HyphenMinusToken = new Text(TokenType.Character, 0x002D);
        private static Text LessThanToken = new Text(TokenType.Character, 0x003C);
        private static Text ReplacementToken = new Text(TokenType.Character, 0xFFFD);
        private static Text RightSqBracketToken = new Text(TokenType.Character, 0x005D);
        private static Text SolidusToken = new Text(TokenType.Character, 0x002F);

        internal State State { get => _state; set => _state = value; }
        internal bool Reconsume { get => _reconsume; set => _reconsume = value; }
        internal Token? Emitted { get => _emitted; set => _emitted = value; }
        internal string ToConsume { get => _toConsume; set => _toConsume = value; }

        public Tokenizer () 
        {            
            State = State.Data;
            _buffer = new Queue<char>();

            Reconsume = false;
            ToConsume = String.Empty;
            Emitted = null;
        }

        internal void step(char current, char next, string? lookAhead = null) 
        { 
            switch (State) 
            {
                case State.Data:
                    if (current == 0x0026)
                    {
                        _return = State.Data;
                        State = State.CharRef;
                    }

                    else if (current == 0x003C)
                        State = State.TagOpen;

                    else if (current == 0x0000)
                        emit(new Text(TokenType.Character, current));

                    else 
                        emit(new Text(TokenType.Character, current));
                                        
                    break;
                case State.RCDATA:
                    if (current == 0x0026)
                    {
                        _return = State.Data;
                        State = State.CharRef;
                    }

                    else if (current == 0x0003C)
                        State = State.RCDATALT;

                    else if (current == 0x0000)
                        emit(ReplacementToken);

                    else
                        emit(new Text(TokenType.Character, current));

                    break;
                case State.RAWTEXT:
                    if (current == 0x003C)
                        State = State.RAWTEXTLT;

                    else if (current == 0x0000)
                        emit(ReplacementToken);

                    else
                        emit(new Text(TokenType.Character, current));

                    break;
                case State.ScriptData:
                    if (current == 0x003C)
                        State = State.ScriptDataLT;

                    else if (current == 0x0000)
                        emit(ReplacementToken);

                    else
                        emit(new Text(TokenType.Character, current));

                    break;
                case State.PLAINTEXT:
                    if (current == 0x0000)
                        emit(ReplacementToken);

                    else
                        emit(new Text(TokenType.Character, current));

                    break;
                case State.TagOpen:
                    if (current == 0x0021)
                        State = State.MarkupDeclarationOpen;
                    
                    else if (current == 0x002F)
                        State = State.EndTagOpen;

                    else if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _currentTag = new Tag(TokenType.StartTag);
                        Reconsume = true;
                        State = State.TagName;
                    }

                    else if (current == 0x003F)
                    {
                        _comment = new Text(TokenType.Comment);
                        Reconsume = true;
                        State = State.BogusComment;
                    }

                    else 
                    {
                        emit(LessThanToken);
                        Reconsume = true;
                        State = State.Data;
                    }

                    break;
                case State.EndTagOpen:
                    if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _currentTag = new Tag(TokenType.EndTag);
                        Reconsume = true;
                        State = State.TagName;
                    }

                    else if (current == 0x003E) 
                        State = State.Data;
                    
                    else 
                    {
                        _comment = new Text(TokenType.Comment);
                        Reconsume = true;
                        State = State.BogusComment;
                    }

                    break;
                case State.TagName:
                    if (commonWhitespace.Contains(current))
                        State = State.BeforeAttributeName;

                    else if (current == 0x002F)
                        State = State.SelfClosingStartTag; 
                    
                    else if (current == 0x003E)
                        State = State.Data;

                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                        _currentTag!.Name.Append(Char.ToLower(current));

                    else if (current == 0x0000)
                        _currentTag!.Name.Append(0xFFFD);
                    
                    else
                        _currentTag!.Name.Append(current);

                    break;
                case State.RCDATALT:
                    if (current == 0x002F)
                    {
                        _buffer.Clear();
                        State = State.RCDATA;
                    }

                    else 
                    {
                        emit(LessThanToken);
                        Reconsume = true;
                        State = State.RCDATA;
                    }  

                    break;
                case State.RCDATAEndTagOpen:
                    if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _currentTag = new Tag(TokenType.EndTag);
                        Reconsume = true;
                        State = State.RCDATAEndTagName;
                    } 

                    else 
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);
                        Reconsume = true;
                        State = State.RCDATA;
                    } 

                    break;
                case State.RCDATAEndTagName:
                    if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                        State = State.BeforeAttributeName;

                    else if (current == 0x002F & isAppropriateEndTag())
                        State = State.SelfClosingStartTag;

                    else if (current == 0x003E & isAppropriateEndTag())
                    { 
                        State = State.Data;
                        emit(_currentTag!);
                    }
                    
                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _currentTag!.Name.Append(Char.ToLower(current));
                        _buffer.Enqueue(current);
                    }

                    else if (Char.IsAscii(current) && Char.IsLower(current))
                    {
                        _currentTag!.Name.Append(current);
                        _buffer.Enqueue(current);
                    }

                    else 
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);
                        
                        while (_buffer.Count > 0)
                            emit(new Text(TokenType.Character, _buffer.Dequeue()));
                        
                        Reconsume = true;
                        State = State.RCDATA;   
                    }

                    break;
                case State.RAWTEXTLT:
                    if (current == 0x002F)
                    {
                        _buffer.Clear();
                        State = State.RAWTEXTEndTagOpen;
                    }

                    else 
                    {
                        emit(LessThanToken);
                        Reconsume = true;
                        State = State.RAWTEXT;
                    }

                    break;
                case State.RAWTEXTEndTagOpen:
                    if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _currentTag = new Tag(TokenType.EndTag);
                        Reconsume = true;
                        State = State.RAWTEXTEndTagName;
                    }

                    else 
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);
                        Reconsume = true;
                        State = State.RAWTEXT;
                    }

                    break;
                case State.RAWTEXTEndTagName:
                    if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                        State = State.BeforeAttributeName;

                    else if (current == 0x002F & isAppropriateEndTag())
                        State = State.SelfClosingStartTag;
                    
                    else if (current == 0x003E & isAppropriateEndTag())
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }
                        
                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _currentTag!.Name.Append(Char.ToLower(current));
                        _buffer.Enqueue(current);

                    }

                    else if (Char.IsAscii(current) && Char.IsLower(current))
                    {
                        _currentTag!.Name.Append(current);
                        _buffer.Enqueue(current);

                    }

                    else
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);

                        while (_buffer.Count > 0)
                            emit(new Text(TokenType.Character, _buffer.Dequeue()));

                        Reconsume = true;
                        State = State.RAWTEXT;
                    }

                    break;
                case State.ScriptDataLT:
                    if (current == 0x002F)
                    {
                        _buffer.Clear();
                        State = State.ScriptDataEndTagOpen;
                    }

                    if (current == 0x0021)
                    {
                        State = State.ScriptDataEscapeStart;
                        emit(LessThanToken);
                        emit(ExclamationToken);
                    }

                    else 
                    {
                        emit(LessThanToken);
                        Reconsume = true;
                        State = State.Data;
                    }
                    
                    break;
                case State.ScriptDataEndTagOpen:
                    if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _currentTag = new Tag(TokenType.EndTag);
                        Reconsume = true;
                        State = State.ScriptDataEndTagName;
                    }

                    else
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);
                        Reconsume = true;
                        State = State.ScriptData;
                    }
                    
                    break;
                case State.ScriptDataEndTagName:
                    if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                        State = State.BeforeAttributeName;

                    else if (current == 0x002F & isAppropriateEndTag())
                        State = State.SelfClosingStartTag;
                    
                    else if (current == 0x003E & isAppropriateEndTag())
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }
                        
                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _currentTag!.Name.Append(Char.ToLower(current));
                        _buffer.Enqueue(current);

                    }

                    else if (Char.IsAscii(current) && Char.IsLower(current))
                    {
                        _currentTag!.Name.Append(current);
                        _buffer.Enqueue(current);

                    }

                    else
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);

                        while (_buffer.Count > 0)
                            emit(new Text(TokenType.Character, _buffer.Dequeue()));

                        Reconsume = true;
                        State = State.ScriptData;
                    }
                    
                    break;
                case State.ScriptDataEscapeStart:
                    if (current == 0x002D)
                    {
                        State = State.ScriptDataEscapeStartDash;
                        emit(HyphenMinusToken);
                    }

                    else 
                    {
                        Reconsume = true;
                        State = State.ScriptData;
                    }
                    
                    break;
                case State.ScriptDataEscapeStartDash:
                    if (current == 0x002D)
                    {
                        State = State.ScriptDataEscapedDashDash;
                        emit(HyphenMinusToken);
                    }

                    else 
                    {
                        Reconsume = true;
                        State = State.ScriptData;
                    }

                    break;
                case State.ScriptDataEscaped:
                    if (current == 0x002D)
                    {
                        State = State.ScriptDataEscapedDash;
                        emit(HyphenMinusToken);
                    }

                    else if (current == 0x003C)
                        State = State.ScriptDataEscapedLT;

                    else if (current == 0x0000)
                        emit(ReplacementToken);

                    else
                        emit(new Text(TokenType.Character, current));
                    
                    break;
                case State.ScriptDataEscapedDash:
                    if (current == 0x002D)
                    {
                        State = State.ScriptDataEscapedDashDash;
                        emit(HyphenMinusToken);
                    }

                    else if (current == 0x003C)
                        State = State.ScriptDataEscapedLT;

                    else if (current == 0x0000)
                    {
                        State = State.ScriptDataEscaped;
                        emit(ReplacementToken);
                    }

                    else
                    {
                        State = State.ScriptDataEscaped;
                        emit(new Text(TokenType.Character, current));
                    }
                    
                    break;
                case State.ScriptDataEscapedDashDash:
                    if (current == 0x002D)
                        emit(HyphenMinusToken);

                    else if (current == 0x003C)
                        State = State.ScriptDataEscapedLT;

                    else if (current == 0x003E)
                    {
                        State = State.ScriptData;
                        emit(GreaterThanToken);
                    }

                    else if (current == 0x0000)
                    {
                        State = State.ScriptDataEscaped;
                        emit(ReplacementToken);
                    }

                    else
                    {
                        State = State.ScriptDataEscaped;
                        emit(new Text(TokenType.Character, current));
                    }
                    break;
                case State.ScriptDataEscapedLT:
                    if (current == 0x002F)
                    {
                        _buffer.Clear();
                        State = State.ScriptDataEscapedEndTagOpen;
                    }

                    if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _buffer.Clear();
                        emit(LessThanToken);
                        Reconsume = true;
                        State = State.ScriptDataDoubleEscapeStart;
                    }

                    else
                    {
                        emit(LessThanToken);
                        Reconsume = true;
                        State = State.ScriptDataEscaped;

                    }

                    break;
                case State.ScriptDataEscapedEndTagOpen:
                    if (Char.IsAscii(current) && Char.IsLetter(current))
                    {
                        _currentTag = new Tag(TokenType.EndTag);
                        Reconsume = true;
                        State = State.ScriptDataEscapedEndTagName;
                    }

                    else
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);
                        Reconsume = true;
                        State = State.ScriptDataEscaped;
                    }
                    
                    break;
                case State.ScriptDataEscapedEndTagName:
                    if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                        State = State.BeforeAttributeName;

                    else if (current == 0x002F & isAppropriateEndTag())
                        State = State.SelfClosingStartTag;
                    
                    else if (current == 0x003E & isAppropriateEndTag())
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }
                        
                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _currentTag!.Name.Append(Char.ToLower(current));
                        _buffer.Enqueue(current);
                    }

                    else if (Char.IsAscii(current) && Char.IsLower(current))
                    {
                        _currentTag!.Name.Append(current);
                        _buffer.Enqueue(current);

                    }

                    else
                    {
                        emit(LessThanToken);
                        emit(SolidusToken);

                        while (_buffer.Count > 0)
                            emit(new Text(TokenType.Character, _buffer.Dequeue()));

                        Reconsume = true;
                        State = State.ScriptDataEscaped;
                    }
                    
                    break;
                case State.ScriptDataDoubleEscapeStart:
                    if ((commonWhitespace.Contains(current) & isAppropriateEndTag()) || current == 0x002F || current == 0x003E)
                    {
                        StringBuilder bufferString = new StringBuilder(_buffer.Count);

                        foreach (Int32 item in _buffer)
                            bufferString.Append(item);

                        if (bufferString.Equals("script"))
                            State = State.ScriptDataDoubleEscaped;
                        else
                        {
                            State = State.ScriptDataEscaped;
                            emit(new Text(TokenType.Character, current));
                        }
                    }

                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _buffer.Enqueue(Char.ToLower(current));
                        emit(new Text(TokenType.Character, current));
                    }

                    else if (Char.IsAscii(current) && Char.IsLower(current))
                    {
                        _buffer.Enqueue(current);
                        emit(new Text(TokenType.Character, current));
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.ScriptDataEscaped;
                    }

                    break;
                case State.ScriptDataDoubleEscaped:
                    if (current == 0x002D)
                    {
                        State = State.ScriptDataDoubleEscapedDash;
                        emit(HyphenMinusToken);
                    }

                    else if (current == 0x003C)
                    {
                        State = State.ScriptDataDoubleEscapedLT;
                        emit(LessThanToken);
                    }

                    else if (current == 0x0000)
                        emit(ReplacementToken);

                    else
                        emit(new Text(TokenType.Character, current));

                    break;
                case State.ScriptDataDoubleEscapedDash:
                    if (current == 0x002D)
                    {
                        State = State.ScriptDataDoubleEscapedDashDash;
                        emit(HyphenMinusToken);
                    }

                    else if (current == 0x003C)
                    {
                        State = State.ScriptDataDoubleEscapedLT;
                        emit(LessThanToken);
                    }

                    else if (current == 0x0000)
                    {
                        State = State.ScriptDataDoubleEscaped;
                        emit(ReplacementToken);
                    }

                    else
                    {
                        State = State.ScriptDataDoubleEscaped;
                        emit(new Text(TokenType.Character, current));
                    }

                    break;
                case State.ScriptDataDoubleEscapedDashDash:
                    if (current == 0x002D)
                        emit(HyphenMinusToken);

                    else if (current == 0x003C)
                    {
                        State = State.ScriptDataDoubleEscapedLT;
                        emit(LessThanToken);
                    }

                    else if (current == 0x003E)
                    {
                        State = State.ScriptData;
                        emit(GreaterThanToken);
                    }

                    else if (current == 0x0000)
                    {
                        State = State.ScriptDataDoubleEscaped;
                        emit(ReplacementToken);
                    }

                    else
                    {
                        State = State.ScriptDataDoubleEscaped;
                        emit(new Text(TokenType.Character, current));
                    }

                    break;
                case State.ScriptDataDoubleEscapedLT:
                    if (current == 0x002F)
                    {
                        _buffer.Clear();
                        State = State.ScriptDataDoubleEscapeEnd;
                        emit(SolidusToken);
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.ScriptDataDoubleEscaped;
                    }

                    break;
                case State.ScriptDataDoubleEscapeEnd:
                    if ((commonWhitespace.Contains(current) & isAppropriateEndTag()) || current == 0x002F || current == 0x003E)
                    {
                        StringBuilder bufferString = new StringBuilder(_buffer.Count);

                        foreach (Int32 item in _buffer)
                            bufferString.Append(item);

                        if (bufferString.Equals("script"))
                            State = State.ScriptDataDoubleEscaped;
                        else
                        {
                            State = State.ScriptDataEscaped;
                            emit(new Text(TokenType.Character, current));
                        }
                    }

                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _buffer.Enqueue(Char.ToLower(current));
                        emit(new Text(TokenType.Character, current));
                    }

                    else if (Char.IsAscii(current) && Char.IsLower(current))
                    {
                        _buffer.Enqueue(current);
                        emit(new Text(TokenType.Character, current));
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.ScriptDataDoubleEscaped;
                    }

                    break;
                case State.BeforeAttributeName:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x002F || current == 0x003E || current == 0x00FF)
                    {
                        Reconsume = true;
                        State = State.AfterAttributeName;
                    }

                    else if (current == 0x003D)
                    {
                        _currentTag!.Attributes.Add(((new StringBuilder(current)), new StringBuilder()));
                        State = State.AttributeName;
                    }

                    else
                    {
                        _currentTag!.Attributes.Add((new StringBuilder(), new StringBuilder()));
                        Reconsume = true;
                        State = State.AttributeName;
                    }

                    break;
                case State.AttributeName:
                    if ((commonWhitespace.Contains(current) & isAppropriateEndTag()) || current == 0x002F || current == 0x003E)
                    {
                        Reconsume = true;
                        State = State.AfterAttributeName;
                    }

                    else if (current == 0x003D)
                        State = State.BeforeAttributeValue;
                    
                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                        appendCharToAttributeName(Char.ToLower(current));

                    else if (current == 0x0000)
                        appendCharToAttributeName(0xFFFD);
                        
                    else
                    {
                        if (current == 0x0022 || current == 0x0027 || current == 0x003C) {}
                        appendCharToAttributeName(current);
                    }
                        
                    
                    break;
                case State.AfterAttributeName:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x002F)
                        State = State.SelfClosingStartTag;

                    else if (current == 0x003D)
                        State = State.BeforeAttributeValue;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }

                    else
                    {
                        _currentTag!.Attributes.Add((new StringBuilder(), new StringBuilder()));
                        Reconsume = true;
                        State = State.AttributeName;
                    }
                    
                    break;
                case State.BeforeAttributeValue:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x0022)
                        State = State.AttributeValueDQ;
                    
                    else if (current == 0x0027)
                        State = State.AttributeValueSQ;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.AttributeValueUQ;
                    }

                    break;
                case State.AttributeValueDQ:
                    if (current == 0x0022)
                        State = State.AfterAttributeValueQ;

                    else if (current == 0x0026)
                    {
                        _return = State.AttributeValueDQ;
                        State = State.CharRef;
                    }                          

                    else if (current == 0x0000)
                        appendCharToAttributeValue(0xFFFD);  

                    else
                        appendCharToAttributeValue(current);
                    
                    break;
                case State.AttributeValueSQ:
                    if (current == 0x0027)
                        State = State.AfterAttributeValueQ;

                    else if (current == 0x0026)
                    {
                        _return = State.AttributeValueSQ;
                        State = State.CharRef;
                    }

                    else if (current == 0x0000)
                        appendCharToAttributeValue(0xFFFD);  

                    else
                        appendCharToAttributeValue(current);
                    
                    break;
                case State.AttributeValueUQ:
                    if (commonWhitespace.Contains(current))
                        State = State.BeforeAttributeName;

                    else if (current == 0x0026)
                    {
                        _return = State.AttributeValueUQ;
                        State = State.CharRef;
                    }
                    
                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }
                    
                    else if (current == 0x0000)
                        appendCharToAttributeValue(0xFFFD);  

                    else
                    {
                        if (current == 0x0022 || current == 0x0027 || current == 0x003C || current == 0x003D || current == 0x0060) {}
                        appendCharToAttributeValue(current);
                    }
                    
                    break;
                case State.AfterAttributeValueQ:
                    if (commonWhitespace.Contains(current))
                        State = State.BeforeAttributeName;
                    
                    else if (current == 0x002F)
                        State = State.SelfClosingStartTag;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_currentTag!);
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.BeforeAttributeName;
                    }

                    break;
                case State.SelfClosingStartTag:
                    if (current == 0x003E)
                    {
                        _currentTag!.SelfClosing = true;
                        State = State.Data;
                        emit(_currentTag!);
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.BeforeAttributeName;
                    }

                    break;
                case State.BogusComment:
                    if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_comment!);
                    }

                    else if (current == 0x0000)
                        _comment!.Data.Append(0xFFFD);

                    else
                        _comment!.Data.Append(current);

                    break;
                case State.MarkupDeclarationOpen:
                    if (lookAhead!.Substring(0, 2).Equals("--", StringComparison.Ordinal)) 
                    {
                        ToConsume = "--";
                        _comment = new Text(TokenType.Comment);
                        State = State.CommentStart;
                    }

                    else if (lookAhead!.Substring(0, 7).Equals("DOCTYPE", StringComparison.OrdinalIgnoreCase))
                    {
                        ToConsume = "DOCTYPE";
                        State = State.DOCTYPE;
                    }

                    else if (lookAhead!.Substring(0, 7).Equals("[CDATA[", StringComparison.Ordinal))
                    {
                        ToConsume = "[CDATA[";
                        
                        /* 
                        // TODO - CDATA LookAhead
                        var node = adjustCurrentNode();
                        if (node & !_htmlNamespace.Contains(node)) 
                            _state = State.CDATASection;
                        
                        else 
                        {
                            _comment!.Data = "[CDATA[";
                            _state = State.BogusComment;
                        }
                        

                        State = State.CDATASection;
                        */
                    }

                    else
                    {
                        _comment = new Text(TokenType.Comment);
                        State = State.BogusComment;
                    }

                    break;
                case State.CommentStart:
                    if (current == 0x002D)
                        State = State.CommentStartDash;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_comment!);
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.CommentStartDash:
                    if (current == 0x002D)
                        State = State.CommentEnd;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_comment!);
                    }

                    else
                    {
                        _comment!.Data.Append(0x002D);
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.Comment:
                    if (current == 0x003C)
                    {
                        _comment!.Data.Append(current);
                        State = State.CommentLT;
                    }

                    else if (current == 0x002D)
                        State = State.CommentEndDash;

                    else if (current == 0x0000)
                        _comment!.Data.Append(0xFFFD);

                    else
                        _comment!.Data.Append(current);

                    break;
                case State.CommentLT:
                    if (current == 0x0021)
                    {
                        _comment!.Data.Append(current);
                        State = State.CommentLTBang;
                    }

                    else if (current == 0x003C)
                        _comment!.Data.Append(current);

                    else
                    {
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.CommentLTBang:
                    if (current == 0x002D)
                        State = State.CommentLTBangDash;

                    else
                    {
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.CommentLTBangDash:
                    if (current == 0x002D)
                        State = State.CommentLTBangDashDash;

                    else
                    {
                        Reconsume = true;
                        State = State.CommentEndDash;
                    }

                    break;
                case State.CommentLTBangDashDash:
                    if (current == 0x003E)
                    {
                        Reconsume = true;
                        State = State.CommentEnd;
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.CommentEnd;
                    }

                    break;
                case State.CommentEndDash:
                    if (current == 0x002D)
                        State = State.CommentEnd;

                    else
                    {
                        _comment!.Data.Append(0x002D);
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.CommentEnd:
                    if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_comment!);
                    }

                    else if (current == 0x0021)
                        State = State.CommentEndBang;

                    else if (current == 0x002D)
                        _comment!.Data.Append(0x002D);

                    else
                    {
                        _comment!.Data.Append(new[] {0x002D, 0x002D});
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.CommentEndBang:
                    if(current == 0x002D)
                    {
                        _comment!.Data.Append("--!");
                        State = State.CommentEndDash;
                    }

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_comment!);
                    }

                    else
                    {
                        _comment!.Data.Append("--!");
                        Reconsume = true;
                        State = State.Comment;
                    }

                    break;
                case State.DOCTYPE:
                    if (commonWhitespace.Contains(current))
                        State = State.BeforeDOCTYPEName;

                    else if (current == 0x003E)
                    {
                        Reconsume = true;
                        State = State.BeforeDOCTYPEName;
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.BeforeDOCTYPEName;    
                    }

                    break;
                case State.BeforeDOCTYPEName:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                    {
                        _doctype = new Doctype(Char.ToLower(current));
                        State = State.DOCTYPEName;
                    }

                    else if (current == 0x0000)
                    {
                        _doctype = new Doctype(0xFFFD);
                        State = State.DOCTYPEName;
                    }
                    
                    else if (current == 0x003E)
                    {
                        _doctype = new Doctype();
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_currentTag!);
                    }

                    else
                    {
                        _doctype = new Doctype(current);
                        State = State.DOCTYPEName;
                    }

                    break;
                case State.DOCTYPEName:
                    if (commonWhitespace.Contains(current))
                        State = State.DOCTYPEName;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else if (Char.IsAscii(current) && Char.IsUpper(current))
                        _doctype!.Name.Append(current);

                    else if (current == 0x0000)
                        _doctype!.Name.Append(0xFFFD);

                    else
                        _doctype!.Name.Append(current);

                    break;
                case State.AfterDOCTYPEName:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                    {
                        if (lookAhead!.Substring(0, 6).Equals("PUBLIC", StringComparison.OrdinalIgnoreCase))
                        {
                            ToConsume = "PUBLIC";  
                            State = State.AfterDOCTYPEPubKey;
                        }

                        else if (lookAhead!.Substring(0, 6).Equals("SYSTEM", StringComparison.Ordinal))
                        {
                            ToConsume = "SYSTEM";  
                            State = State.AfterDOCTYPESysKey;
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            Reconsume = true;
                            State = State.BogusDOCTYPE;
                        }
                    }

                    break;
                case State.AfterDOCTYPEPubKey:
                    if (commonWhitespace.Contains(current))
                        State = State.BeforeDOCTYPEPubId;

                    else if (current == 0x0022)
                    {
                        _doctype!.Pub = new StringBuilder();
                        State = State.DOCTYPEPubIdDQ;
                    }

                    else if (current == 0x0027)
                    {
                        _doctype!.Pub = new StringBuilder();
                        State = State.DOCTYPEPubIdSQ;
                    }

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                    {
                        _doctype!.ForceQuirks = true;
                        Reconsume = true;
                        State = State.BogusDOCTYPE;
                    }

                    break;
                case State.BeforeDOCTYPEPubId:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x0022)
                    {
                        _doctype!.Pub = new StringBuilder();
                        State = State.DOCTYPEPubIdDQ;
                    }

                    else if (current == 0x0027)
                    {
                        _doctype!.Pub = new StringBuilder();
                        State = State.DOCTYPEPubIdSQ;
                    }

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                    {
                        _doctype!.ForceQuirks = true;
                        Reconsume = true;
                        State = State.BogusDOCTYPE;
                    }

                    break;
                case State.DOCTYPEPubIdDQ:
                    if (current == 0x0022)
                        State = State.AfterDOCTYPEPubId;

                    else if (current == 0x0000)
                        _doctype!.Pub.Append(0xFFFD);

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                        _doctype!.Pub.Append(current);

                    break;
                case State.DOCTYPEPubIdSQ:
                    if (current == 0x0027)
                        State = State.AfterDOCTYPEPubId;

                    else if (current == 0x0000)
                        _doctype!.Pub.Append(0xFFFD);

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                        _doctype!.Pub.Append(current);

                    break;
                case State.AfterDOCTYPEPubId:
                    if (commonWhitespace.Contains(current))
                        State = State.BetweenDOCTYPEPubSysIds;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else if (current == 0x0022)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdDQ;
                    }

                    else if (current == 0x0027)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdSQ;
                    }

                    else
                    {
                        _doctype!.ForceQuirks = true;
                        Reconsume = true;
                        State = State.BogusDOCTYPE;
                    }

                    break;
                case State.BetweenDOCTYPEPubSysIds:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else if (current == 0x0022)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdDQ;
                    }

                    else if (current == 0x0027)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdSQ;
                    }

                    else
                    {
                        _doctype!.ForceQuirks = true;
                        Reconsume = true;
                        State = State.BogusDOCTYPE;
                    }

                    break;
                case State.AfterDOCTYPESysKey:
                    if (commonWhitespace.Contains(current))
                        State = State.BeforeDOCTYPESysId;

                    else if (current == 0x0022)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdDQ;
                    }

                    else if (current == 0x0027)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdSQ;
                    }

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                    {
                        _doctype!.ForceQuirks = true;
                        Reconsume = true;
                        State = State.BogusDOCTYPE;
                    }

                    break;
                case State.BeforeDOCTYPESysId:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x0022)
                    {                            
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdDQ;
                    }

                    else if (current == 0x0027)
                    {
                        _doctype!.Sys = new StringBuilder();
                        State = State.DOCTYPESysIdSQ;
                    }

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                    {
                        _doctype!.ForceQuirks = true;
                        Reconsume = true;
                        State = State.BogusDOCTYPE;    
                    }

                    break;
                case State.DOCTYPESysIdDQ:
                    if (current == 0x0022)
                        State = State.AfterDOCTYPESysId;
                    
                    else if (current == 0x0000)
                        _doctype!.Sys.Append(0xFFFD);

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                        _doctype!.Sys.Append(current);

                    break;
                case State.DOCTYPESysIdSQ:
                    if (current == 0x0027)
                        State = State.AfterDOCTYPESysId;
                    
                    else if (current == 0x0000)
                        _doctype!.Sys.Append(0xFFFD);

                    else if (current == 0x003E)
                    {
                        _doctype!.ForceQuirks = true;
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                        _doctype!.Sys.Append(current);

                    break;
                case State.AfterDOCTYPESysId:
                    if (commonWhitespace.Contains(current))
                        break;

                    else if (current == 0x003E)
                    {
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.BogusDOCTYPE;
                    }

                    break;
                case State.BogusDOCTYPE:
                    if (current == 0x003e)
                    {
                        State = State.Data;
                        emit(_doctype!);
                    }

                    else if (current == 0x0000)
                        break;

                    break;                        
                case State.CDATASection:
                    if (current == 0x005D)
                        State = State.CDATASectionBracket;

                    else
                        emit(new Text(TokenType.Character, current));

                    break;
                case State.CDATASectionBracket:
                    if (current == 0x005D)
                        State = State.CDATASectionEnd;

                    else
                    {
                        emit(RightSqBracketToken);
                        Reconsume = true;
                        State = State.CDATASection;
                    }

                    break;
                case State.CDATASectionEnd:
                    if (current == 0x005D)
                        emit(RightSqBracketToken);

                    else if (current == 0x003E)
                        State = State.Data;

                    else
                    {
                        emit(RightSqBracketToken);
                        emit(RightSqBracketToken);
                        Reconsume = true;
                        State = State.CDATASection;
                    }

                    break;
                case State.CharRef:
                    _buffer.Clear();
                    _buffer.Enqueue((char) 0x0026);

                    if (Char.IsAscii(current) && Char.IsLetterOrDigit(current))
                    {
                        Reconsume = true;
                        State = State.NamedCharRef;
                    }

                    else if (current == 0x0023)
                    {
                        _buffer.Enqueue(current);
                        State = State.NumCharRef;
                    }

                    else
                    {
                        flushCodePoints();            
                        Reconsume = true;
                        State = _return;
                    }

                    break;
                case State.NamedCharRef:
                    // TODO: TOKENIZER - NamedCharRef!!
                    // always enter from State.CharRef when _reconsume is true
                    // filter down to a smaller and smaller collection because it's easier to implement
                    char[] carr = new char[_buffer.Count + 1];
                    carr[_buffer.Count] = current;
                    _buffer.CopyTo(carr, 0);

                    StringBuilder toMatch = new StringBuilder(new String(carr));

                    List<string> filtered = entities.Keys.ToList().Where(x => x.StartsWith(toMatch.ToString())).ToList();
                    string? match = null;

                    while(filtered.Count > 0)
                    {
                        toMatch.Append(next);
                        
                        filtered = filtered.Where(x => x.StartsWith(toMatch.ToString())).ToList();
                        if (filtered.Count > 0)
                        {
                            _buffer.Enqueue(current);
                            match = filtered[0];
                        }                            
                    }

                    if (match is not null)
                    {
                        bool asAttribute = _return == State.AttributeValueDQ || _return == State.AttributeValueSQ || _return == State.AttributeValueUQ;
                        if (asAttribute && !match.EndsWith(';') && (next == 0x003D || (Char.IsAscii(next) && Char.IsLetterOrDigit(next))))
                        {
                            flushCodePoints();
                            State = _return;
                        }
                        else
                        {
                            if (!match.EndsWith(';')) {}

                            _buffer.Clear();

                            foreach (var ch in entities[match].Item2)
                            {
                                _buffer.Enqueue(ch);

                                StringBuilder hexstring = new StringBuilder();                                   
                                {
                                    if (ch == '\\')
                                        if (hexstring.Length > 0)
                                        {
                                            _buffer.Enqueue((char) Int32.Parse(hexstring.ToString(), NumberStyles.AllowHexSpecifier));
                                            hexstring.Clear();
                                        }
                                    else if (ch == 'u') {}
                                    else
                                        hexstring.Append(ch);
                                }
            
                                _buffer.Enqueue((char) Int32.Parse(hexstring.ToString(), NumberStyles.AllowHexSpecifier));
                            }

                            flushCodePoints();
                            State = _return;
                        }
                    }
                    else
                    {
                        flushCodePoints();
                        State = State.AmbiguousAmpersand;
                    }
                    
                    break;
                case State.AmbiguousAmpersand:
                    if (Char.IsAscii(current) && Char.IsLetterOrDigit(current))
                    {
                        if (_return == State.AttributeValueDQ || _return == State.AttributeValueSQ || _return == State.AttributeValueUQ)
                            appendCharToAttributeValue(current);

                        else 
                            emit(new Text(TokenType.Character, current));
                    }

                    else if (current == 0x003B)
                    {
                        Reconsume = true;
                        State = _return;
                    }

                    else 
                    {
                        Reconsume = true;
                        State = _return;
                    }
                    
                    break;
                case State.NumCharRef:
                    _charRefCode = 0;
                    if (current == 0x0078 || current == 0x0058)
                    {
                        _buffer.Enqueue(current);
                        State = State.HexCharRefStart;
                    }

                    else
                    {
                        Reconsume = true;
                        State = State.DecCharRefStart;
                    }

                    break;
                case State.HexCharRefStart:
                    if (asciiHexDigit.Contains(current))
                    {
                        Reconsume = true;
                        State = State.HexCharRef;
                    }

                    else
                    {
                        flushCodePoints();
                        Reconsume = true;
                        State = _return;
                    }                      

                    break;
                case State.DecCharRefStart:
                    if (Char.IsAscii(current) && Char.IsDigit(current))
                    {
                        Reconsume = true;
                        State = State.DecCharRef;
                    }

                    else
                    {
                        flushCodePoints();
                        Reconsume = true;
                        State = _return;
                    }

                    break;
                case State.HexCharRef:
                    if (Char.IsAscii(current) && Char.IsDigit(current))
                        _charRefCode = (_charRefCode * 16) + (current - 0x0030);

                    else if (asciiUpperHexDigit.Contains(current))
                        _charRefCode = (_charRefCode * 16) + (current - 0x0037);

                    else if (asciiLowerHexDigit.Contains(current))
                        _charRefCode = (_charRefCode * 16) + (current - 0x0057);

                    else if (current == 0x003B)
                        State = State.NumCharRefEnd;

                    else 
                    {
                        Reconsume = true;
                        State = State.NumCharRefEnd;
                    }

                    break;
                case State.DecCharRef:
                    if (Char.IsAscii(current) && Char.IsDigit(current))
                        _charRefCode = (_charRefCode * 10) + (current - 0x0030);

                    else if (current == 0x003B)
                        State = State.NumCharRefEnd;

                    else 
                    {
                        Reconsume = true;
                        State = State.NumCharRefEnd;
                    }
                    break;
                case State.NumCharRefEnd:
                    if (_charRefCode == 0x00)
                        _charRefCode = 0xFFFD;
                    
                    else if (_charRefCode > 0x10FFFF)
                        _charRefCode = 0xFFFD;

                    else if (_charRefCode >= 0xD800 & _charRefCode <= 0xDFFF)
                        _charRefCode = 0xFFFD;

                    else if (_charRefCode >= 0xFDD0 & _charRefCode <= 0xFDEF) {}

                    else if (_charRefCode == 0x0D || (((_charRefCode >= 0x0000 & _charRefCode <= 0x001F) || (_charRefCode >= 0x007F & _charRefCode <= 009F)) & !asciiWhitespace.Contains(_charRefCode))) {}
                    
                    else if (codes.ContainsKey(_charRefCode))
                        _charRefCode = Int32.Parse(codes[_charRefCode]);
                    
                    _buffer.Clear();
                    _buffer.Enqueue((char)_charRefCode);
                    flushCodePoints();
                    State = _return;

                    break;
                default:
                    break;

            }    
        }

        // TODO: IMPLEMENT SPANS FOR MEMORY 
        private void appendCharToAttributeName(Int32 character)
        {
            int length = _currentTag!.Attributes.Count;
            _currentTag.Attributes[length - 1].name.Append((char) character);
        }

        private void appendCharToAttributeValue(Int32 character)
        {
            int length = _currentTag!.Attributes.Count;            
            _currentTag.Attributes[length - 1].value.Append((char) character);
        }        

        private void emit(Token token) 
        {
            if (token.TokenType == TokenType.StartTag)
                _lastEmittedStartTag = (Tag) token;

            Emitted = token;
        }

        private void flushCodePoints()
        {
            bool asAttribute = _return == State.AttributeValueDQ || _return == State.AttributeValueSQ || _return == State.AttributeValueUQ;
            while (_buffer.Count > 0)
            {
                if (asAttribute)
                    appendCharToAttributeValue(_buffer.Dequeue());
                else
                    emit(new Text(TokenType.Character, _buffer.Dequeue()));
            }
        }

        private bool isAppropriateEndTag()
        {
            if (_lastEmittedStartTag is not null && (_lastEmittedStartTag.Name == _currentTag!.Name))
                return true;

            return false;
        }
        
        /*
        private void runEOF()
        {
            if (State == State.TagOpen)
            {
                emit(LessThanToken);
                emit(EndOfFileToken);
            }
            
            else if (State == State.EndTagOpen)
            {
                emit(LessThanToken);
                emit(SolidusToken);
                emit(EndOfFileToken);
            }
            
            else if (CommentEOFs.Contains(State))
                emit(_comment!);
            
            else if (State == State.DOCTYPE || State == State.BeforeDOCTYPEName)
            {
                _doctype = new Doctype();
                _doctype!.ForceQuirks = true;
                emit(_currentTag!);
            }

            else if (DoctypeEOFs.Contains(State))
            {
                _doctype!.ForceQuirks = true;
                emit(_doctype!);
            }

            else if (State == State.BogusDOCTYPE)
                emit(_doctype!);

            emit(EndOfFileToken);
        }
        */
    }
}
