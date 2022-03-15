  
  namespace Luxor.DOM
  {
      public class NodeIterator
      {
        public NodeIterator(Node root)
        {
            Root = root;
            ReferenceNode = root;
        }

        public Node Root { get; }
            public Node ReferenceNode { get; }
            public bool PointerBeforeReferenceNode { get; }
            public int WhatToShow { get; }

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
      }
  }
