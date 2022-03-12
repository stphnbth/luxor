using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

using System.Diagnostics;

using Extensions;
using static Reference.DataTables;

namespace Luxor 
{
    public class Tokenizer
    {        
        private StreamReader _stream;
        private State _state;
        private State _return;
        private Queue<char> _buffer;
        // private InsertionMode _insertionMode;
        private int _charRefCode;
        private bool _reconsume;

        private Doctype? _doctype;
        private Tag? _currentTag;
        private Tag? _lastEmittedStartTag;
        private Text? _comment;

        private static Text ExclamationToken = new Text(Type.Character, 0x0021);
        private static EndOfFile EndOfFileToken = new EndOfFile(Type.EndOfFile, 0xFFFF);
        private static Text GreaterThanToken = new Text(Type.Character, 0x003E);
        private static Text HyphenMinusToken = new Text(Type.Character, 0x002D);
        private static Text LessThanToken = new Text(Type.Character, 0x003C);
        private static Text ReplacementToken = new Text(Type.Character, 0xFFFD);
        private static Text RightSqBracketToken = new Text(Type.Character, 0x005D);
        private static Text SolidusToken = new Text(Type.Character, 0x002F);
    

        public Tokenizer (StreamReader sr) 
        {            
            _stream = sr;
            _state = State.Data;
            _buffer = new Queue<char>();
            // _insertionMode = InsertionMode.Initial;
            _reconsume = false;
        }

        public void run() 
        {
            Int32 next = _stream.Read();
            Int32 current = 0x0000;

            // STATES THAT DON'T HAVE EOF BRANCHES HAVE TO RECONSUME 

            while (next > 0 || _reconsume) 
            {
                switch (_state) 
                {
                    case State.Data:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x0026)
                        {
                            _return = State.Data;
                            _state = State.CharRef;
                        }

                        else if (current == 0x003C)
                            _state = State.TagOpen;

                        else if (current == 0x0000)
                            emit(new Text(Type.Character, current));

                        else 
                            emit(new Text(Type.Character, current));
                                            
                        break;
                    case State.RCDATA:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x0026)
                        {
                            _return = State.Data;
                            _state = State.CharRef;
                        }

                        else if (current == 0x0003C)
                            _state = State.RCDATALT;

                        else if (current == 0x0000)
                            emit(ReplacementToken);

                        else
                            emit(new Text(Type.Character, current));

                        break;
                    case State.RAWTEXT:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x003C)
                            _state = State.RAWTEXTLT;

                        else if (current == 0x0000)
                            emit(ReplacementToken);

                        else
                            emit(new Text(Type.Character, current));
    
                        break;
                    case State.ScriptData:
                        (current, next) = consume(current, next);

                        if (current == 0x003C)
                            _state = State.ScriptDataLT;

                        else if (current == 0x0000)
                            emit(ReplacementToken);

                        else
                            emit(new Text(Type.Character, current));
    
                        break;
                    case State.PLAINTEXT:
                        (current, next) = consume(current, next);                        

                        if (current == 0x0000)
                            emit(ReplacementToken);

                        else
                            emit(new Text(Type.Character, current));
    
                        break;
                    case State.TagOpen:
                        (current, next) = consume(current, next);                        

                        if (current == 0x0021)
                            _state = State.MarkupDeclarationOpen;
                        
                        else if (current == 0x002F)
                            _state = State.EndTagOpen;

                        else if (asciiAlpha.Contains(current))
                        {
                            _currentTag = new Tag(Type.StartTag);
                            _reconsume = true;
                            _state = State.TagName;
                        }

                        else if (current == 0x003F)
                        {
                            _comment = new Text(Type.Comment);
                            _reconsume = true;
                            _state = State.BogusComment;
                        }

                        else 
                        {
                            emit(LessThanToken);
                            _reconsume = true;
                            _state = State.Data;
                        }

                        break;
                    case State.EndTagOpen:
                        (current, next) = consume(current, next);                        

                        if (asciiAlpha.Contains(current))
                        {
                            _currentTag = new Tag(Type.EndTag);
                            _reconsume = true;
                            _state = State.TagName;
                        }

                        else if (current == 0x003E) 
                            _state = State.Data;
                        
                        else 
                        {
                            _comment = new Text(Type.Comment);
                            _reconsume = true;
                            _state = State.BogusComment;
                        }

                        break;
                    case State.TagName:
                        (current, next) = consume(current, next);                        

                        if (commonWhitespace.Contains(current))
                            _state = State.BeforeAttributeName;

                        else if (current == 0x002F)
                            _state = State.SelfClosingStartTag; 
                        
                        else if (current == 0x003E)
                            _state = State.Data;

                        else if (asciiAlphaUpper.Contains(current))
                            _currentTag!.Name.Append((char) (current = 0x0020));

                        else if (current == 0x0000)
                            _currentTag!.Name.Append((char) 0xFFFD);
                        
                        else
                            _currentTag!.Name.Append((char) current);

                        break;
                    case State.RCDATALT:
                        (current, next) = consume(current, next);                        

                        if (current == 0x002F)
                        {
                            _buffer.Clear();
                            _state = State.RCDATA;
                        }

                        else 
                        {
                            emit(LessThanToken);
                            _reconsume = true;
                            _state = State.RCDATA;
                        }  

                        break;
                    case State.RCDATAEndTagOpen:
                        (current, next) = consume(current, next);                        

                        if (asciiAlpha.Contains(current))
                        {
                            _currentTag = new Tag(Type.EndTag);
                            _reconsume = true;
                            _state = State.RCDATAEndTagName;
                        } 

                        else 
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);
                            _reconsume = true;
                            _state = State.RCDATA;
                        } 

                        break;
                    case State.RCDATAEndTagName:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                            _state = State.BeforeAttributeName;

                        else if (current == 0x002F & isAppropriateEndTag())
                            _state = State.SelfClosingStartTag;

                        else if (current == 0x003E & isAppropriateEndTag())
                        { 
                            _state = State.Data;
                            emit(_currentTag!);
                        }
                        
                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _currentTag!.Name.Append((char) (current + 0x0020));
                            _buffer.Enqueue((char) current);
                        }

                        else if (asciiAlphaLower.Contains(current))
                        {
                            _currentTag!.Name.Append((char) current);
                            _buffer.Enqueue((char) current);
                        }

                        else 
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);
                            
                            while (_buffer.Count > 0)
                                emit(new Text(Type.Character, _buffer.Dequeue()));
                            
                            _reconsume = true;
                            _state = State.RCDATA;   
                        }

                        break;
                    case State.RAWTEXTLT:
                        (current, next) = consume(current, next);

                        if (current == 0x002F)
                        {
                            _buffer.Clear();
                            _state = State.RAWTEXTEndTagOpen;
                        }

                        else 
                        {
                            emit(LessThanToken);
                            _reconsume = true;
                            _state = State.RAWTEXT;
                        }

                        break;
                    case State.RAWTEXTEndTagOpen:
                        (current, next) = consume(current, next);
                        
                        if (asciiAlpha.Contains(current))
                        {
                            _currentTag = new Tag(Type.EndTag);
                            _reconsume = true;
                            _state = State.RAWTEXTEndTagName;
                        }

                        else 
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);
                            _reconsume = true;
                            _state = State.RAWTEXT;
                        }

                        break;
                    case State.RAWTEXTEndTagName:
                        (current, next) = consume(current, next);
                        
                        if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                            _state = State.BeforeAttributeName;

                        else if (current == 0x002F & isAppropriateEndTag())
                            _state = State.SelfClosingStartTag;
                        
                        else if (current == 0x003E & isAppropriateEndTag())
                        {
                            _state = State.Data;
                            emit(_currentTag!);
                        }
                            
                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _currentTag!.Name.Append((char) (current + 0x0020));
                            _buffer.Enqueue((char) current);

                        }

                        else if (asciiAlphaLower.Contains(current))
                        {
                            _currentTag!.Name.Append((char) current);
                            _buffer.Enqueue((char) current);

                        }

                        else
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);

                            while (_buffer.Count > 0)
                                emit(new Text(Type.Character, _buffer.Dequeue()));

                            _reconsume = true;
                            _state = State.RAWTEXT;
                        }

                        break;
                    case State.ScriptDataLT:
                        (current, next) = consume(current, next);

                        if (current == 0x002F)
                        {
                            _buffer.Clear();
                            _state = State.ScriptDataEndTagOpen;
                        }

                        if (current == 0x0021)
                        {
                            _state = State.ScriptDataEscapeStart;
                            emit(LessThanToken);
                            emit(ExclamationToken);
                        }

                        else 
                        {
                            emit(LessThanToken);
                            _reconsume = true;
                            _state = State.Data;
                        }
                        
                        break;
                    case State.ScriptDataEndTagOpen:
                        (current, next) = consume(current, next);

                        if (asciiAlpha.Contains(current))
                        {
                            _currentTag = new Tag(Type.EndTag);
                            _reconsume = true;
                            _state = State.ScriptDataEndTagName;
                        }

                        else
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);
                            _reconsume = true;
                            _state = State.ScriptData;
                        }
                        
                        break;
                    case State.ScriptDataEndTagName:
                        (current, next) = consume(current, next);
                        
                        if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                            _state = State.BeforeAttributeName;

                        else if (current == 0x002F & isAppropriateEndTag())
                            _state = State.SelfClosingStartTag;
                        
                        else if (current == 0x003E & isAppropriateEndTag())
                        {
                            _state = State.Data;
                            emit(_currentTag!);
                        }
                            
                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _currentTag!.Name.Append((char) (current + 0x0020));
                            _buffer.Enqueue((char) current);

                        }

                        else if (asciiAlphaLower.Contains(current))
                        {
                            _currentTag!.Name.Append((char) current);
                            _buffer.Enqueue((char) current);

                        }

                        else
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);

                            while (_buffer.Count > 0)
                                emit(new Text(Type.Character, _buffer.Dequeue()));

                            _reconsume = true;
                            _state = State.ScriptData;
                        }
                        
                        break;
                    case State.ScriptDataEscapeStart:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                        {
                            _state = State.ScriptDataEscapeStartDash;
                            emit(HyphenMinusToken);
                        }

                        else 
                        {
                            _reconsume = true;
                            _state = State.ScriptData;
                        }
                        
                        break;
                    case State.ScriptDataEscapeStartDash:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x002D)
                        {
                            _state = State.ScriptDataEscapedDashDash;
                            emit(HyphenMinusToken);
                        }

                        else 
                        {
                            _reconsume = true;
                            _state = State.ScriptData;
                        }

                        break;
                    case State.ScriptDataEscaped:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                        {
                            _state = State.ScriptDataEscapedDash;
                            emit(HyphenMinusToken);
                        }

                        else if (current == 0x003C)
                            _state = State.ScriptDataEscapedLT;

                        else if (current == 0x0000)
                            emit(ReplacementToken);

                        else
                            emit(new Text(Type.Character, current));
                        
                        break;
                    case State.ScriptDataEscapedDash:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                        {
                            _state = State.ScriptDataEscapedDashDash;
                            emit(HyphenMinusToken);
                        }

                        else if (current == 0x003C)
                            _state = State.ScriptDataEscapedLT;

                        else if (current == 0x0000)
                        {
                            _state = State.ScriptDataEscaped;
                            emit(ReplacementToken);
                        }

                        else
                        {
                            _state = State.ScriptDataEscaped;
                            emit(new Text(Type.Character, current));
                        }
                        
                        break;
                    case State.ScriptDataEscapedDashDash:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                            emit(HyphenMinusToken);

                        else if (current == 0x003C)
                            _state = State.ScriptDataEscapedLT;

                        else if (current == 0x003E)
                        {
                            _state = State.ScriptData;
                            emit(GreaterThanToken);
                        }

                        else if (current == 0x0000)
                        {
                            _state = State.ScriptDataEscaped;
                            emit(ReplacementToken);
                        }

                        else
                        {
                            _state = State.ScriptDataEscaped;
                            emit(new Text(Type.Character, current));
                        }
                        break;
                    case State.ScriptDataEscapedLT:
                        (current, next) = consume(current, next);

                        if (current == 0x002F)
                        {
                            _buffer.Clear();
                            _state = State.ScriptDataEscapedEndTagOpen;
                        }

                        if (asciiAlpha.Contains(current))
                        {
                            _buffer.Clear();
                            emit(LessThanToken);
                            _reconsume = true;
                            _state = State.ScriptDataDoubleEscapeStart;
                        }

                        else
                        {
                            emit(LessThanToken);
                            _reconsume = true;
                            _state = State.ScriptDataEscaped;

                        }

                        break;
                    case State.ScriptDataEscapedEndTagOpen:
                        (current, next) = consume(current, next);

                        if (asciiAlpha.Contains(current))
                        {
                            _currentTag = new Tag(Type.EndTag);
                            _reconsume = true;
                            _state = State.ScriptDataEscapedEndTagName;
                        }

                        else
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);
                            _reconsume = true;
                            _state = State.ScriptDataEscaped;
                        }
                        
                        break;
                    case State.ScriptDataEscapedEndTagName:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current) & isAppropriateEndTag())
                            _state = State.BeforeAttributeName;

                        else if (current == 0x002F & isAppropriateEndTag())
                            _state = State.SelfClosingStartTag;
                        
                        else if (current == 0x003E & isAppropriateEndTag())
                        {
                            _state = State.Data;
                            emit(_currentTag!);
                        }
                            
                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _currentTag!.Name.Append((char) (current + 0x0020));
                            _buffer.Enqueue((char) current);
                        }

                        else if (asciiAlphaLower.Contains(current))
                        {
                            _currentTag!.Name.Append((char) current);
                            _buffer.Enqueue((char) current);

                        }

                        else
                        {
                            emit(LessThanToken);
                            emit(SolidusToken);

                            while (_buffer.Count > 0)
                                emit(new Text(Type.Character, _buffer.Dequeue()));

                            _reconsume = true;
                            _state = State.ScriptDataEscaped;
                        }
                        
                        break;
                    case State.ScriptDataDoubleEscapeStart:
                        (current, next) = consume(current, next);

                        if ((commonWhitespace.Contains(current) & isAppropriateEndTag()) || current == 0x002F || current == 0x003E)
                        {
                            StringBuilder bufferString = new StringBuilder(_buffer.Count);

                            foreach (Int32 item in _buffer)
                                bufferString.Append(item);

                            if (bufferString.Equals("script"))
                                _state = State.ScriptDataDoubleEscaped;
                            else
                            {
                                _state = State.ScriptDataEscaped;
                                emit(new Text(Type.Character, current));
                            }
                        }

                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _buffer.Enqueue((char) (current + 0x0020));
                            emit(new Text(Type.Character, current));
                        }

                        else if (asciiAlphaLower.Contains(current))
                        {
                            _buffer.Enqueue((char) current);
                            emit(new Text(Type.Character, current));
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.ScriptDataEscaped;
                        }

                        break;
                    case State.ScriptDataDoubleEscaped:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x002D)
                        {
                            _state = State.ScriptDataDoubleEscapedDash;
                            emit(HyphenMinusToken);
                        }

                        else if (current == 0x003C)
                        {
                            _state = State.ScriptDataDoubleEscapedLT;
                            emit(LessThanToken);
                        }

                        else if (current == 0x0000)
                            emit(ReplacementToken);

                        else
                            emit(new Text(Type.Character, current));

                        break;
                    case State.ScriptDataDoubleEscapedDash:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x002D)
                        {
                            _state = State.ScriptDataDoubleEscapedDashDash;
                            emit(HyphenMinusToken);
                        }

                        else if (current == 0x003C)
                        {
                            _state = State.ScriptDataDoubleEscapedLT;
                            emit(LessThanToken);
                        }

                        else if (current == 0x0000)
                        {
                            _state = State.ScriptDataDoubleEscaped;
                            emit(ReplacementToken);
                        }

                        else
                        {
                            _state = State.ScriptDataDoubleEscaped;
                            emit(new Text(Type.Character, current));
                        }

                        break;
                    case State.ScriptDataDoubleEscapedDashDash:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x002D)
                            emit(HyphenMinusToken);

                        else if (current == 0x003C)
                        {
                            _state = State.ScriptDataDoubleEscapedLT;
                            emit(LessThanToken);
                        }

                        else if (current == 0x003E)
                        {
                            _state = State.ScriptData;
                            emit(GreaterThanToken);
                        }

                        else if (current == 0x0000)
                        {
                            _state = State.ScriptDataDoubleEscaped;
                            emit(ReplacementToken);
                        }

                        else
                        {
                            _state = State.ScriptDataDoubleEscaped;
                            emit(new Text(Type.Character, current));
                        }

                        break;
                    case State.ScriptDataDoubleEscapedLT:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x002F)
                        {
                            _buffer.Clear();
                            _state = State.ScriptDataDoubleEscapeEnd;
                            emit(SolidusToken);
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.ScriptDataDoubleEscaped;
                        }

                        break;
                    case State.ScriptDataDoubleEscapeEnd:
                        (current, next) = consume(current, next);
                        
                        if ((commonWhitespace.Contains(current) & isAppropriateEndTag()) || current == 0x002F || current == 0x003E)
                        {
                            StringBuilder bufferString = new StringBuilder(_buffer.Count);

                            foreach (Int32 item in _buffer)
                                bufferString.Append(item);

                            if (bufferString.Equals("script"))
                                _state = State.ScriptDataDoubleEscaped;
                            else
                            {
                                _state = State.ScriptDataEscaped;
                                emit(new Text(Type.Character, current));
                            }
                        }

                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _buffer.Enqueue((char) (current + 0x0020));
                            emit(new Text(Type.Character, current));
                        }

                        else if (asciiAlphaLower.Contains(current))
                        {
                            _buffer.Enqueue((char) current);
                            emit(new Text(Type.Character, current));
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.ScriptDataDoubleEscaped;
                        }

                        break;
                    case State.BeforeAttributeName:
                        (current, next) = consume(current, next);
                    
                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x002F || current == 0x003E || current == 0x00FF)
                        {
                            _reconsume = true;
                            _state = State.AfterAttributeName;
                        }

                        else if (current == 0x003D)
                        {
                            _currentTag!.Attributes.Add(((new StringBuilder(current)), new StringBuilder()));
                            _state = State.AttributeName;
                        }

                        else
                        {
                            _currentTag!.Attributes.Add((new StringBuilder(), new StringBuilder()));
                            _reconsume = true;
                            _state = State.AttributeName;
                        }

                        break;
                    case State.AttributeName:
                        (current, next) = consume(current, next);

                        if ((commonWhitespace.Contains(current) & isAppropriateEndTag()) || current == 0x002F || current == 0x003E)
                        {
                            _reconsume = true;
                            _state = State.AfterAttributeName;
                        }

                        else if (current == 0x003D)
                            _state = State.BeforeAttributeValue;
                        
                        else if (asciiAlphaUpper.Contains(current))
                            appendCharToAttributeName((current += 0x0020));

                        else if (current == 0x0000)
                            appendCharToAttributeName(0xFFFD);
                            
                        else
                        {
                            if (current == 0x0022 || current == 0x0027 || current == 0x003C) {}
                            appendCharToAttributeName(current);
                        }
                            
                      
                        break;
                    case State.AfterAttributeName:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x002F)
                            _state = State.SelfClosingStartTag;

                        else if (current == 0x003D)
                            _state = State.BeforeAttributeValue;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_currentTag!);
                        }

                        else
                        {
                            _currentTag!.Attributes.Add((new StringBuilder(), new StringBuilder()));
                            _reconsume = true;
                            _state = State.AttributeName;
                        }
                        
                        break;
                    case State.BeforeAttributeValue:
                        (current, next) = consume(current, next);
                        
                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x0022)
                            _state = State.AttributeValueDQ;
                        
                        else if (current == 0x0027)
                            _state = State.AttributeValueSQ;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_currentTag!);
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.AttributeValueUQ;
                        }

                        break;
                    case State.AttributeValueDQ:
                        (current, next) = consume(current, next);

                        if (current == 0x0022)
                            _state = State.AfterAttributeValueQ;

                        else if (current == 0x0026)
                        {
                            _return = State.AttributeValueDQ;
                            _state = State.CharRef;
                        }                          

                        else if (current == 0x0000)
                            appendCharToAttributeValue(0xFFFD);  

                        else
                            appendCharToAttributeValue(current);
                        
                        break;
                    case State.AttributeValueSQ:
                        (current, next) = consume(current, next);

                        if (current == 0x0027)
                            _state = State.AfterAttributeValueQ;

                        else if (current == 0x0026)
                        {
                            _return = State.AttributeValueSQ;
                            _state = State.CharRef;
                        }

                        else if (current == 0x0000)
                            appendCharToAttributeValue(0xFFFD);  

                        else
                            appendCharToAttributeValue(current);
                        
                        break;
                    case State.AttributeValueUQ:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            _state = State.BeforeAttributeName;

                        else if (current == 0x0026)
                        {
                            _return = State.AttributeValueUQ;
                            _state = State.CharRef;
                        }
                        
                        else if (current == 0x003E)
                        {
                            _state = State.Data;
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
                        (current, next) = consume(current, next);
                        
                        if (commonWhitespace.Contains(current))
                            _state = State.BeforeAttributeName;
                        
                        else if (current == 0x002F)
                            _state = State.SelfClosingStartTag;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_currentTag!);
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.BeforeAttributeName;
                        }

                        break;
                    case State.SelfClosingStartTag:
                        (current, next) = consume(current, next);

                        if (current == 0x003E)
                        {
                            _currentTag!.SelfClosing = true;
                            _state = State.Data;
                            emit(_currentTag!);
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.BeforeAttributeName;
                        }

                        break;
                    case State.BogusComment:
                        (current, next) = consume(current, next);

                        if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_comment!);
                        }

                        else if (current == 0x0000)
                            _comment!.Data.Append((char) 0xFFFD);

                        else
                            _comment!.Data.Append((char) current);

                        break;
                    case State.MarkupDeclarationOpen:
                        if (lookAhead("--", StringComparison.Ordinal)) 
                        {
                            (current, next) = consumeCharacters("--", current, next);
                            _comment = new Text(Type.Comment);
                            _state = State.CommentStart;
                        }

                        else if (lookAhead("DOCTYPE", StringComparison.OrdinalIgnoreCase))
                        {
                            (current, next) = consumeCharacters("DOCTYPE", current, next);
                            _state = State.DOCTYPE;
                        }

                        else if (lookAhead("[CDATA[", StringComparison.Ordinal))
                        {
                            (current, next) = consumeCharacters("[CDARA[", current, next);

                            /*
                            var node = adjustCurrentNode();
                            if (node & !_htmlNamespace.Contains(node)) 
                                _state = State.CDATASection;
                            
                            else 
                            {
                                _comment!.Data = "[CDATA[";
                                _state = State.BogusComment;
                            }
                            */

                            _state = State.CDATASection;
                        }

                        else
                        {
                            _comment = new Text(Type.Comment);
                            _state = State.BogusComment;
                        }

                        break;
                    case State.CommentStart:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                            _state = State.CommentStartDash;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_comment!);
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.CommentStartDash:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                            _state = State.CommentEnd;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_comment!);
                        }

                        else
                        {
                            _comment!.Data.Append(0x002D);
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.Comment:
                        (current, next) = consume(current, next);

                        if (current == 0x003C)
                        {
                            _comment!.Data.Append((char) current);
                            _state = State.CommentLT;
                        }

                        else if (current == 0x002D)
                            _state = State.CommentEndDash;

                        else if (current == 0x0000)
                            _comment!.Data.Append((char) 0xFFFD);

                        else
                            _comment!.Data.Append((char) current);

                        break;
                    case State.CommentLT:
                        (current, next) = consume(current, next);

                        if (current == 0x0021)
                        {
                            _comment!.Data.Append((char) current);
                            _state = State.CommentLTBang;
                        }

                        else if (current == 0x003C)
                            _comment!.Data.Append((char) current);

                        else
                        {
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.CommentLTBang:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                            _state = State.CommentLTBangDash;

                        else
                        {
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.CommentLTBangDash:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                            _state = State.CommentLTBangDashDash;

                        else
                        {
                            _reconsume = true;
                            _state = State.CommentEndDash;
                        }

                        break;
                    case State.CommentLTBangDashDash:
                        (current, next) = consume(current, next);

                        if (current == 0x003E)
                        {
                            _reconsume = true;
                            _state = State.CommentEnd;
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.CommentEnd;
                        }

                        break;
                    case State.CommentEndDash:
                        (current, next) = consume(current, next);

                        if (current == 0x002D)
                            _state = State.CommentEnd;

                        else
                        {
                            _comment!.Data.Append(0x002D);
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.CommentEnd:
                        (current, next) = consume(current, next);

                        if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_comment!);
                        }

                        else if (current == 0x0021)
                            _state = State.CommentEndBang;

                        else if (current == 0x002D)
                            _comment!.Data.Append(0x002D);

                        else
                        {
                            _comment!.Data.Append(new[] {(char) 0x002D, (char) 0x002D});
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.CommentEndBang:
                        (current, next) = consume(current, next);

                        if(current == 0x002D)
                        {
                            _comment!.Data.Append("--!");
                            _state = State.CommentEndDash;
                        }

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_comment!);
                        }

                        else
                        {
                            _comment!.Data.Append("--!");
                            _reconsume = true;
                            _state = State.Comment;
                        }

                        break;
                    case State.DOCTYPE:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            _state = State.BeforeDOCTYPEName;

                        else if (current == 0x003E)
                        {
                            _reconsume = true;
                            _state = State.BeforeDOCTYPEName;
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.BeforeDOCTYPEName;    
                        }

                        break;
                    case State.BeforeDOCTYPEName:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            break;

                        else if (asciiAlphaUpper.Contains(current))
                        {
                            _doctype = new Doctype(current + 0x0020);
                            _state = State.DOCTYPEName;
                        }

                        else if (current == 0x0000)
                        {
                            _doctype = new Doctype(0xFFFD);
                            _state = State.DOCTYPEName;
                        }
                        
                        else if (current == 0x003E)
                        {
                            _doctype = new Doctype();
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_currentTag!);
                        }

                        else
                        {
                            _doctype = new Doctype(current);
                            _state = State.DOCTYPEName;
                        }

                        break;
                    case State.DOCTYPEName:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            _state = State.DOCTYPEName;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else if (asciiAlphaUpper.Contains(current))
                            _doctype!.Name.Append((char) current);

                        else if (current == 0x0000)
                            _doctype!.Name.Append((char) 0xFFFD);

                        else
                            _doctype!.Name.Append((char) current);

                        break;
                    case State.AfterDOCTYPEName:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                        {
                            if (lookAhead("PUBLIC", StringComparison.OrdinalIgnoreCase))
                            {
                                (current, next) = consumeCharacters("PUBLIC", current, next);  
                                _state = State.AfterDOCTYPEPubKey;
                            }

                            else if (lookAhead("SYSTEM", StringComparison.Ordinal))
                            {
                                (current, next) = consumeCharacters("SYSTEM", current, next);  
                                _state = State.AfterDOCTYPESysKey;
                            }

                            else
                            {
                                _doctype!.ForceQuirks = true;
                                _reconsume = true;
                                _state = State.BogusDOCTYPE;
                            }
                        }

                        break;
                    case State.AfterDOCTYPEPubKey:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            _state = State.BeforeDOCTYPEPubId;

                        else if (current == 0x0022)
                        {
                            _doctype!.Pub = new StringBuilder();
                            _state = State.DOCTYPEPubIdDQ;
                        }

                        else if (current == 0x0027)
                        {
                            _doctype!.Pub = new StringBuilder();
                            _state = State.DOCTYPEPubIdSQ;
                        }

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;
                        }

                        break;
                    case State.BeforeDOCTYPEPubId:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x0022)
                        {
                            _doctype!.Pub = new StringBuilder();
                            _state = State.DOCTYPEPubIdDQ;
                        }

                        else if (current == 0x0027)
                        {
                            _doctype!.Pub = new StringBuilder();
                            _state = State.DOCTYPEPubIdSQ;
                        }

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;
                        }

                        break;
                    case State.DOCTYPEPubIdDQ:
                        (current, next) = consume(current, next);
                        
                        if (current == 0x0022)
                            _state = State.AfterDOCTYPEPubId;

                        else if (current == 0x0000)
                            _doctype!.Pub.Append((char) 0xFFFD);

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                            _doctype!.Pub.Append((char) current);

                        break;
                    case State.DOCTYPEPubIdSQ:
                        (current, next) = consume(current, next);

                        if (current == 0x0027)
                            _state = State.AfterDOCTYPEPubId;

                        else if (current == 0x0000)
                            _doctype!.Pub.Append((char) 0xFFFD);

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                            _doctype!.Pub.Append((char) current);

                        break;
                    case State.AfterDOCTYPEPubId:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            _state = State.BetweenDOCTYPEPubSysIds;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else if (current == 0x0022)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdDQ;
                        }

                        else if (current == 0x0027)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdSQ;
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;
                        }

                        break;
                    case State.BetweenDOCTYPEPubSysIds:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else if (current == 0x0022)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdDQ;
                        }

                        else if (current == 0x0027)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdSQ;
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;
                        }

                        break;
                    case State.AfterDOCTYPESysKey:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            _state = State.BeforeDOCTYPESysId;

                        else if (current == 0x0022)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdDQ;
                        }

                        else if (current == 0x0027)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdSQ;
                        }

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;
                        }

                        break;
                    case State.BeforeDOCTYPESysId:
                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x0022)
                        {                            
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdDQ;
                        }

                        else if (current == 0x0027)
                        {
                            _doctype!.Sys = new StringBuilder();
                            _state = State.DOCTYPESysIdSQ;
                        }

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                        {
                            _doctype!.ForceQuirks = true;
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;    
                        }

                        break;
                    case State.DOCTYPESysIdDQ:
                        (current, next) = consume(current, next);

                        if (current == 0x0022)
                            _state = State.AfterDOCTYPESysId;
                        
                        else if (current == 0x0000)
                            _doctype!.Sys.Append((char) 0xFFFD);

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                            _doctype!.Sys.Append((char) current);

                        break;
                    case State.DOCTYPESysIdSQ:
                        (current, next) = consume(current, next);

                        if (current == 0x0027)
                            _state = State.AfterDOCTYPESysId;
                        
                        else if (current == 0x0000)
                            _doctype!.Sys.Append((char) 0xFFFD);

                        else if (current == 0x003E)
                        {
                            _doctype!.ForceQuirks = true;
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                            _doctype!.Sys.Append((char) current);

                        break;
                    case State.AfterDOCTYPESysId:
                        (current, next) = consume(current, next);

                        if (commonWhitespace.Contains(current))
                            break;

                        else if (current == 0x003E)
                        {
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.BogusDOCTYPE;
                        }

                        break;
                    case State.BogusDOCTYPE:
                        (current, next) = consume(current, next);

                        if (current == 0x003e)
                        {
                            _state = State.Data;
                            emit(_doctype!);
                        }

                        else if (current == 0x0000)
                            break;

                        break;                        
                    case State.CDATASection:
                        (current, next) = consume(current, next);

                        if (current == 0x005D)
                            _state = State.CDATASectionBracket;

                        else
                            emit(new Text(Type.Character, current));

                        break;
                    case State.CDATASectionBracket:
                        (current, next) = consume(current, next);

                        if (current == 0x005D)
                            _state = State.CDATASectionEnd;

                        else
                        {
                            emit(RightSqBracketToken);
                            _reconsume = true;
                            _state = State.CDATASection;
                        }

                        break;
                    case State.CDATASectionEnd:
                        (current, next) = consume(current, next);

                        if (current == 0x005D)
                            emit(RightSqBracketToken);

                        else if (current == 0x003E)
                            _state = State.Data;

                        else
                        {
                            emit(RightSqBracketToken);
                            emit(RightSqBracketToken);
                            _reconsume = true;
                            _state = State.CDATASection;
                        }

                        break;
                    case State.CharRef:
                        _buffer.Clear();
                        _buffer.Enqueue((char) 0x0026);

                        (current, next) = consume(current, next); 

                        if (asciiAlphanumeric.Contains(current))
                        {
                            _reconsume = true;
                            _state = State.NamedCharRef;
                        }

                        else if (current == 0x0023)
                        {
                            _buffer.Enqueue((char) current);
                            _state = State.NumCharRef;
                        }

                        else
                        {
                            flushCodePoints();            
                            _reconsume = true;
                            _state = _return;
                        }

                        break;
                    case State.NamedCharRef:
                        // always enter from State.CharRef when _reconsume is true
                        (current, next) = consume(current, next);

                        // filter down to a smaller and smaller collection because it's easier to implement
                        char[] carr = new char[_buffer.Count + 1];
                        carr[_buffer.Count] = (char) current;
                        _buffer.CopyTo(carr, 0);

                        StringBuilder toMatch = new StringBuilder(new String(carr));

                        List<string> filtered = entities.Keys.ToList().Where(x => x.StartsWith(toMatch.ToString())).ToList();
                        string? match = null;

                        while(filtered.Count > 0)
                        {
                            toMatch.Append((char) next);
                            
                            filtered = filtered.Where(x => x.StartsWith(toMatch.ToString())).ToList();
                            if (filtered.Count > 0)
                            {
                                _buffer.Enqueue((char) current);
                                (current, next) = consume(current, next);
                                match = filtered[0];
                            }                            
                        }

                        if (match is not null)
                        {
                            bool asAttribute = _return == State.AttributeValueDQ || _return == State.AttributeValueSQ || _return == State.AttributeValueUQ;
                            if (asAttribute && !match.EndsWith(';') && (next == 0x003D || asciiAlphanumeric.Contains(next)))
                            {
                                flushCodePoints();
                                _state = _return;
                            }
                            else
                            {
                                if (!match.EndsWith(';')) {}

                                _buffer.Clear();

                                foreach (var ch in entities[match].Item2)
                                    _buffer.Enqueue(ch);

                                /*
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
                                */

                                flushCodePoints();
                                _state = _return;
                            }
                        }
                        else
                        {
                            flushCodePoints();
                            _state = State.AmbiguousAmpersand;
                        }
                        
                        break;
                    case State.AmbiguousAmpersand:
                        (current, next) = consume(current, next); 

                        if (asciiAlphanumeric.Contains(current))
                        {
                            if (_return == State.AttributeValueDQ || _return == State.AttributeValueSQ || _return == State.AttributeValueUQ)
                                appendCharToAttributeValue(current);

                            else 
                                emit(new Text(Type.Character, current));
                        }

                        else if (current == 0x003B)
                        {
                            _reconsume = true;
                            _state = _return;
                        }

                        else 
                        {
                            _reconsume = true;
                            _state = _return;
                        }
                        
                        break;
                    case State.NumCharRef:
                        _charRefCode = 0;
                        (current, next) = consume(current, next); 

                        if (current == 0x0078 || current == 0x0058)
                        {
                            _buffer.Enqueue((char) current);
                            _state = State.HexCharRefStart;
                        }

                        else
                        {
                            _reconsume = true;
                            _state = State.DecCharRefStart;
                        }

                        break;
                    case State.HexCharRefStart:
                        (current, next) = consume(current, next);

                        if (asciiHexDigit.Contains(current))
                        {
                            _reconsume = true;
                            _state = State.HexCharRef;
                        }

                        else
                        {
                            flushCodePoints();
                            _reconsume = true;
                            _state = _return;
                        }                      

                        break;
                    case State.DecCharRefStart:
                        (current, next) = consume(current, next); 

                        if (asciiDigit.Contains(current))
                        {
                            _reconsume = true;
                            _state = State.DecCharRef;
                        }

                        else
                        {
                            flushCodePoints();
                            _reconsume = true;
                            _state = _return;
                        }

                        break;
                    case State.HexCharRef:
                        (current, next) = consume(current, next); 

                        if (asciiDigit.Contains(current))
                            _charRefCode = (_charRefCode * 16) + (current - 0x0030);
    
                        else if (asciiUpperHexDigit.Contains(current))
                            _charRefCode = (_charRefCode * 16) + (current - 0x0037);

                        else if (asciiLowerHexDigit.Contains(current))
                            _charRefCode = (_charRefCode * 16) + (current - 0x0057);

                        else if (current == 0x003B)
                            _state = State.NumCharRefEnd;

                        else 
                        {
                            _reconsume = true;
                            _state = State.NumCharRefEnd;
                        }

                        break;
                    case State.DecCharRef:
                        (current, next) = consume(current, next); 

                        if (asciiDigit.Contains(current))
                            _charRefCode = (_charRefCode * 10) + (current - 0x0030);
    
                        else if (current == 0x003B)
                            _state = State.NumCharRefEnd;

                        else 
                        {
                            _reconsume = true;
                            _state = State.NumCharRefEnd;
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
                        _state = _return;

                        break;
                    default:
                        break;

                }    
            }

            runEOF();
        }


        // TODO: IMPLEMENT SPANS 
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

        private (Int32, Int32) consume(Int32 current, Int32 next) 
        {
            if (_reconsume)
            {
                _reconsume = false;
                return (current, next);
            }

            return (next, _stream.Read());
        }

        private (Int32, Int32) consumeCharacters(string match, Int32 current, Int32 next)
        {
            current = next;

            for (int i = 0; i < match.Length - 1; i++)
                current = _stream.Read();
            
            return (current, _stream.Read());
        }

        private void emit(Token token) 
        {
            if (token.Type == Type.StartTag)
                _lastEmittedStartTag = (Tag) token;

            token.PrintToken();
        }

        private void flushCodePoints()
        {
            bool asAttribute = _return == State.AttributeValueDQ || _return == State.AttributeValueSQ || _return == State.AttributeValueUQ;
            while (_buffer.Count > 0)
            {
                if (asAttribute)
                    appendCharToAttributeValue(_buffer.Dequeue());
                else
                    emit(new Text(Type.Character, _buffer.Dequeue()));
            }
        }

        private bool isAppropriateEndTag()
        {
            if (_lastEmittedStartTag is not null && (_lastEmittedStartTag.Name == _currentTag!.Name))
                return true;

            return false;
        }
        
        private bool lookAhead(string toMatch, StringComparison comparisonType)
        {
            StringBuilder match = new StringBuilder(new String(_stream.ExposeCharBuffer(toMatch.Length)));
            return toMatch.Equals(match.ToString(), comparisonType);
        }

        private void runEOF()
        {
            if (_state == State.TagOpen)
            {
                emit(LessThanToken);
                emit(EndOfFileToken);
            }
            
            else if (_state == State.EndTagOpen)
            {
                emit(LessThanToken);
                emit(SolidusToken);
                emit(EndOfFileToken);
            }
            
            else if (CommentEOFs.Contains(_state))
                emit(_comment!);
            
            else if (_state == State.DOCTYPE || _state == State.BeforeDOCTYPEName)
            {
                _doctype = new Doctype();
                _doctype!.ForceQuirks = true;
                emit(_currentTag!);
            }

            else if (DoctypeEOFs.Contains(_state))
            {
                _doctype!.ForceQuirks = true;
                emit(_doctype!);
            }

            else if (_state == State.BogusDOCTYPE)
                emit(_doctype!);

            emit(EndOfFileToken);
        }
    }
}
