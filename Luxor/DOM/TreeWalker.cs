
namespace Luxor.DOM
{
    public class TreeWalker
    {
        public TreeWalker(Node root)
        {
            Root = root;
            CurrentNode = root;
        }

        public Node Root { get; }
        public int WhatToShow { get; }
        public Node CurrentNode { get; set; }

        public Node? parentNode()
        {
            throw new NotImplementedException();
        }

        public Node? fristChild()
        {
            throw new NotImplementedException();
        }

        public Node? lastChild()
        {
            throw new NotImplementedException();
        }

        public Node? previousSibling()
        {
            throw new NotImplementedException();
        }

        public Node? nextSibling()
        {
            throw new NotImplementedException();
        }

        public Node? previousNode()
        {
            throw new NotImplementedException();
        }

        public Node? nextNode()
        {
            throw new NotImplementedException();
        }
    }
}