using Luxor.DOM;

using static Data.DataTables;

namespace Luxor.Parser
{
    public class TreeBuilder
    {
        private Mode _mode;
        private Mode _orignalMode;
        private Stack<Mode> _templateModes;
        private Mode _currentTemplateMode;
        private Document _document;

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

        // 13.2.4.1 The Insertion Mode
        private void resetMode() 
        {
            /*
            // 1
            bool last = false;

            // 2
            Node node = _openElements.Peek();

            // 3
            Loop:
                if (node == _firstOpenElement) { last = true; }
                // TODO: HTML fragment parsing algorithm

            // 4
            if (node.GetType().Equals(typeof(HTMLSelectElement)))
            {
                // 4.1
                if (last) { goto: Done; }

                // 4.2
                var ancestor = node;

                // 4.3
                InternalLoop:
                    if (ancestor == _firstOpenElement) { goto: Done; }

                // 4.4


                // 4.5
                if (ancestor.GetType().Equals(typeof(HTMLTemplateElement))) { goto: Done; }

                // 4.6
                if (ancestor.GetType().Equals(typeof(HTMLTableElement)))
                {
                    _mode = Mode.SelectInTable;
                    return;
                }

                // 4.7
                goto: InternalLoop;

                // 4.8
                Done:
                    _mode = Mode.Select;
                    return;

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
            */

        }

        // 13.2.4.2 The Stack of Open Elements
        private void getScope() { throw new NotImplementedException(); }

        // 13.2.4.3 The List of Active Formatting Elements
        private void pushToActiveFormattingElements() { throw new NotImplementedException(); }
        private void reconstructActiveFormattingElements() { throw new NotImplementedException(); }
        private void clearToLastMarker() { throw new NotImplementedException(); }

        
        // 13.2.6 Tree Construction
        internal void dispatch(Token token)
        {
            // if the stack of open elements is empty
            if (_openElements.Count == 0 || adjCheck(token) || token.TokenType == TokenType.EndOfFile)
            {

            }

            
        }

        private bool adjCheck(Token token)
        {
            // if the adjusted current node is an element in the HTML namespace
            if (_adjustedCurrentNode!.NamespaceURI == HTMLNamespace) { return true; }

            // if the adjusted current node is a MathML text integration point and the token is a start tag whose tag name is neither "mglyph" nor "malignmark"
            // if the adjusted current node is a MathML text integration point and the token is a character token
            if (_adjustedCurrentNodeContains!)
            // if the adjusted current node is a MathML annotation-xml element and the token is a start tag whose tag name is "svg"
            if (_adjustedCurrentNodeContains!)
            
            
            // if the adjusted current node is an HTML integration point and the token is a start tag
            // if the adjusted current node is an HTML integration point and the token is a character token
            if (_adjustedCurrentNodeContains!)
            
            


            return false;
    
        }

        // 13.2.6.1 Creating and Inserting Nodes
        private void insert() { throw new NotImplementedException(); }
        private Element createElementForToken(string nspace, Element intendedParent) { throw new NotImplementedException(); }
        private void insertForeignElement(string nspace, Token token) { throw new NotImplementedException(); }

        private void insertHTMLElement(Token token) { insertForeignElement(HTMLNamespace, token); }

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
            // _tokenizer.State = nextState;
            
            // 3
            _orignalMode = _mode;

            // 4
            _mode = Mode.Text;
        } 

        // 13.2.6.3 Closing Elements That Have Implied End Tags

        // 13.2.6.4 The Rules for Parsing Tokens in HTML Content
        // 13.2.6.5 The Rules for Parsing Tokens in Foreign Content        
    }
}