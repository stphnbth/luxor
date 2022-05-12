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
        // PRIVATE FIELDS
        private Document _document;

        private NodeType _type;
        private string _name;
        private string _baseURI;
        private bool _isConnected;
        private Node? _parentNode;
        private Element? _parentElement;
        private List<Node>? _children;
        private Node? _firstChild;
        private Node? _lastChild;
        private Node? _previousSibling;
        private Node? _nextSibling;

        // PROTECTED PROPERTIES
        protected Document NodeDocument { get => _document; set => _document = value; }

        // PUBLIC PROPERTIES
        public NodeType NodeType { get => _type; }
        public string? BaseURI { get => NodeDocument.BaseURL; } // TODO: serialize URL's

        public bool IsConnected
        {
            get
            {
                Node root = GetRoot();

                throw new NotImplementedException();
            }
        }

        public Document? OwnerDocument { get => (NodeType == NodeType.Document) ? null : _document; }
        public Node? ParentNode { get => _parentNode; }
        public Element? ParentElement { get => _parentElement; }

        // TODO: node children are parsed and filtered, I think....
        public List<Node>? ChildNodes { get => _children; }

        public Node? FirstChild
        {
            get
            {
                if (_children is null) { return null; }
                return _children[0];
            }
        }

        public Node? LastChild
        {
            get
            {
                if (_children is null) { return null; }
                return _children[_children.Count - 1];
            }
        }

        public Node? PreviousSibling { get => _previousSibling; }
        public Node? NextSibling { get => _nextSibling; }

        // VIRTUAL PROPERTIES
        public virtual string NodeName { get => _name; }

        public virtual string? NodeValue
        {
            get { return null; }
            set {}
        }

        public virtual string? TextContent
        {
            get { return null; }
            set {}
        }

        // CONSTRUCTOR
        public Node(Document document)
        {
            _document = document;
        }

        // HELPER METHODS
        internal string GetDescendantText()
        {
            throw new NotImplementedException();
        }

        internal Node GetRoot()
        {
            Node result = this;

            while (result.ParentNode is not null) { result = result.ParentNode; }

            return result;
        }

        // https://dom.spec.whatwg.org/#ref-for-string-replace-all
        internal void StringReplaceAll(string? str)
        {
            // 1
            Node? node = null;

            // 2
            if (!String.IsNullOrEmpty(str)) { node = new Text(str, NodeDocument); }

            // 3
            // TODO: MUTATION METHODS
        }

        // PUBLIC METHODS
        public Node GetRootNode(bool composed)
        {
            throw new NotImplementedException();
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