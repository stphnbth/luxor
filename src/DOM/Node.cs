using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Extensions;
using static Reference.DataTables;

namespace Luxor.DOM
{
    public abstract class Node
    {
        protected NodeType Type { get; set; }
        protected string? Name { get; set; }
        protected bool IsConnected { get; }
        protected Document? Document { get; }
        protected Node? ParentNode { get; }
        protected Element? ParentElement { get; }
        protected List<Node>? ChildNodes { get; set; }
        protected Node? FirstChild { get; set; }
        protected Node? LastChild { get; set; }
        protected Node? PreviousSibling { get; set; }
        protected Node? NextSibling { get; set; }

        private string? _value;
        private string? _textContext;

        public string BaseURI { get => (Document is not null) ? Document.DocumentURI : ""; }

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

        public string LookupPrefix(string nspace)
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

        Node InsertBefore(Node node, Node? child)
        {
            throw new NotImplementedException();
        }

        Node AppendChild(Node node)
        {
            throw new NotImplementedException();
        }

        Node ReplaceChild(Node node, Node child)
        {
            throw new NotImplementedException();
        }

        Node RemoveChild(Node child)
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