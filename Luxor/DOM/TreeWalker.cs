
namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#treewalker
    public class TreeWalker
    {
        // PRIVATE FIELDS
        private Node _root;
        private NodeFilterShow _whatToShow;
        private INodeFilter? _filter;
        private Node _currentNode;

        // PUBLIC PROPERTIES
        public Node Root { get => _root; set => _root = value; }
        public NodeFilterShow WhatToShow { get => _whatToShow; set => _whatToShow = value; }
        public INodeFilter? Filter { get => _filter; set => _filter = value; }
        public Node CurrentNode { get => _currentNode; set => _currentNode = value; }

        // CONSTRUCTOR
        private TreeWalker() {}

        public Node? ParentNode()
        {
            throw new NotImplementedException();
        }

        public Node? FristChild()
        {
            throw new NotImplementedException();
        }

        public Node? LastChild()
        {
            throw new NotImplementedException();
        }

        public Node? PreviousSibling()
        {
            throw new NotImplementedException();
        }

        public Node? NextSibling()
        {
            throw new NotImplementedException();
        }

        public Node? PreviousNode()
        {
            throw new NotImplementedException();
        }

        public Node? NextNode()
        {
            throw new NotImplementedException();
        }
    }
}