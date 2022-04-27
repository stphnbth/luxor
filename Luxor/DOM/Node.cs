using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Extensions;
using static Reference.DataTables;

namespace Luxor.DOM
{
    public interface Node
    {
        protected string? BaseURI { get; }
        protected List<Node>? ChildNodes { get; }
        protected Node? FirstChild { get; }
        protected bool IsConnected { get; }
        protected Node? LastChild { get; }
        protected Node? NextSibling { get; }
        protected string? NodeName { get; }
        protected NodeType NodeType { get; }
        protected string? NodeValue { get; set; }
        protected Document? OwnerDocument { get; }
        protected Node? ParentNode { get; }
        protected Element? ParentElement { get; }
        protected Node? PreviousSibling { get; }
        protected string? TextContent { get; set; }

        Node GetRootNode(bool composed)
        {
            if (composed) {}    // TODO: RETURN SHADOW INCLUDING ROOT

            Node result = this;

            while (result.ParentNode is not null)
                result = result.ParentNode;

            return result;
        }

        bool HasChildNodes() => ChildNodes is not null && ChildNodes.Count > 0 ? true : false;


        void Normalize()
        {
            throw new NotImplementedException();
        }

        Node CloneNode(bool deep)
        {
            throw new NotImplementedException();
        }

        bool IsEqualNode(Node otherNode)
        {
            throw new NotImplementedException();
        }

        ushort CompareDocumentPosition(Node other)
        {
            throw new NotImplementedException();
        }

        bool Contains(Node? other)
        {
            throw new NotImplementedException();
        }

        string LookupPrefix(string nspace)
        {
            throw new NotImplementedException();
        }

        string LookupNamespaceURI(string prefix)
        {
            throw new NotImplementedException();
        }

        string IsDefaultNamepsace(string nspace)
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