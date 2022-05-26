using System.Collections;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#nodeiterator
    public class NodeIterator : IEnumerator
    {
        // PRIVATE FIELDS
        private Node[] _nodes;
        private Node _root;
        private Node _referenceNode;
        private bool _pointerBeforeReferenceNode;
        private NodeFilterShow _whatToShow;
        private INodeFilter? _filter;

        // PUBLIC PROPERTIES
        public Node Root { get => _root; }
        public Node ReferenceNode { get => _referenceNode; }
        public bool PointerBeforeReferenceNode { get => _pointerBeforeReferenceNode; }
        public NodeFilterShow WhatToShow { get => _whatToShow; }
        public INodeFilter? Filter { get => _filter; }

        // CONSTRUCTOR
        public NodeIterator() {}

        // PUBLIC METHODS
        public Node? NextNode()
        {
            throw new NotImplementedException();
        }

        public Node? PreviousNode()
        {
            throw new NotImplementedException();
        }

        public void Detach()
        {
            throw new NotImplementedException();
        }

        // IEnumerator Implementation
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Node Current
        {
            get
            {
                return _referenceNode;
            }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
