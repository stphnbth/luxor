using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Extensions;
using static Data.DataTables;

namespace Luxor.DOM
{
    public abstract class Node
    {
        protected Node(Document ownerDocument)
        {
            OwnerDocument = ownerDocument;
        }

        protected internal string? BaseURI { get; }
        protected internal List<Node>? ChildNodes { get; }
        protected internal Node? FirstChild { get; }
        protected internal bool IsConnected { get; }
        protected internal Node? LastChild { get; }
        protected internal Node? NextSibling { get; }
        protected internal string? NodeName { get; }
        protected internal NodeType NodeType { get; }
        protected internal string? NodeValue { get; set; }
        protected internal Document OwnerDocument { get; }
        protected internal Node? ParentNode { get; }
        protected internal Element? ParentElement { get; }
        protected internal Node? PreviousSibling { get; }
        protected internal string? TextContent { get; set; }

        public Node GetRootNode(bool composed)
        {
            if (composed) {}    // TODO: RETURN SHADOW INCLUDING ROOT

            Node result = this;

            while (result.ParentNode is not null)
                result = result.ParentNode;

            return result;
        }

        public bool HasChildNodes() => ChildNodes is not null && ChildNodes.Count > 0 ? true : false;


        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public Node CloneNode(bool deep)
        {
            throw new NotImplementedException();
        }

        public bool IsEqualNode(Node otherNode)
        {
            throw new NotImplementedException();
        }

        public ushort CompareDocumentPosition(Node other)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Node? other)
        {
            throw new NotImplementedException();
        }

        public string? LookupPrefix(string nspace)
        {
            throw new NotImplementedException();
        }

        public string LookupNamespaceURI(string prefix)
        {
            throw new NotImplementedException();
        }

        public string IsDefaultNamepsace(string nspace)
        {
            throw new NotImplementedException();
        }

        public Node InsertBefore(Node node, Node? child)
        {
            throw new NotImplementedException();
        }

        public Node AppendChild(Node node)
        {
            throw new NotImplementedException();
        }

        public Node ReplaceChild(Node node, Node child)
        {
            throw new NotImplementedException();
        }

        public Node RemoveChild(Node child)
        {
            throw new NotImplementedException();
        }
    }

    public enum NodeType
    {
        Element,
        Attribute,
        Text,
        CDATASection,
        EntityRef,
        Entity,
        ProcessingInstr,
        Comment,
        Document,
        DocumentType,
        DocumentFragment,
        Notation,
    }
}