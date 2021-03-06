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
        private List<Node> _children;
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
        public List<Node> ChildNodes { get => _children; }

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
            ReplaceAll(this, this.ParentNode);
        }

        // MUTATION ALGORITHMS //
        public void Append(Node node)
        {
            throw new NotImplementedException();
        }

        // https://dom.spec.whatwg.org/#concept-node-replace-all
        public void ReplaceAll(Node node, Node parent)
        {
            /*
            // 1
            List<Node> removedNodes = parent.ChildNodes;

            // 2
            List<Node> addedNodes = new List<Node>();

            // 3
            if (node is DocumentFragment)
            {
                addedNodes = node.ChildNodes;
            }

            // 4
            else
            {

            }

            // 5

            // 6

            // 7
            */

            throw new NotImplementedException();
        }

        // PUBLIC METHODS
        public Node GetRootNode(GetRootNodeOptions? options = null)
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

    // https://dom.spec.whatwg.org/#nonelementparentnode
    public interface INonElementParentNode
    {
        public Element? GetElementById(string elementId)
        {
            throw new NotImplementedException();
        }
    }

    public struct GetRootNodeOptions
    {
        public bool composed = false;
        public GetRootNodeOptions() {}
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