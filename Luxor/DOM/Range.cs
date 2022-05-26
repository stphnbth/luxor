using System.Diagnostics;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#range
    public class Range : AbstractRange
    {
        // PRIVATE FIELDS
        private Node _commonAncestorContainer;

        // PUBLIC PROPERTIES
        public Node CommonAncestorContainer { get => _commonAncestorContainer; }

        // CONSTRUCTOR
        public Range() {}

        // PUBLIC METHODS
        public void SetStart(Node node, ulong offset)
        {
            throw new NotImplementedException();
        }

        public void SetEnd(Node node, ulong offset)
        {
            throw new NotImplementedException();
        }

        public void SetStartBefore(Node node)
        {
            throw new NotImplementedException();
        }

        public void SetStartAfter(Node node)
        {
            throw new NotImplementedException();
        }

        public void SetEndBefore(Node node)
        {
            throw new NotImplementedException();
        }

        public void SetEndAfter(Node node)
        {
            throw new NotImplementedException();
        }

        public void Collapse(bool toStart = false)
        {
            throw new NotImplementedException();
        }

        public void SelectNode(Node node)
        {
            throw new NotImplementedException();
        }

        public void SelectNodeContents(Node node)
        {
            throw new NotImplementedException();
        }

        public short CompareBoundaryPoints(ushort how, Range sourceRange)
        {
            throw new NotImplementedException();
        }
        
        public void DeleteContents()
        {
            throw new NotImplementedException();
        }
        
        public DocumentFragment ExtractContents()
        {
            throw new NotImplementedException();
        }
        
        public DocumentFragment CloneContents()
        {
            throw new NotImplementedException();
        }
        
        public void InsertNode(Node node)
        {
            throw new NotImplementedException();
        }

        public void SurroundContents(Node newParent)
        {
            throw new NotImplementedException();
        }
        
        public Range CloneRange()
        {
            throw new NotImplementedException();
        }
        
        public void Detach()
        {
            throw new NotImplementedException();
        }
        
        public bool IsPointInRange(Node node, ulong offset)
        {
            throw new NotImplementedException();
        }
        
        public short ComparePoint(Node node, ulong offest)
        {
            throw new NotImplementedException();
        }
        
        public bool InstersectsNode(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
