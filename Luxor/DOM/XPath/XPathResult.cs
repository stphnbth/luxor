namespace Luxor.DOM.XPath
{
    // https://dom.spec.whatwg.org/#xpathresult
    public class XPathResult
    {
        // PRIVATE FIELDS
        private XPathType _resultType;
        private double _numberValue;
        private string _stringValue;
        private bool _booleanValue;
        private Node? _singleNodeValue;
        private bool _invalidIteratorState;
        private ulong _snapshotLength;

        // PUBLIC PROPERTIES
        public XPathType ResultType { get => _resultType; set => _resultType = value; }
        public double NumberValue { get => _numberValue; set => _numberValue = value; }
        public string StringValue { get => _stringValue; set => _stringValue = value; }
        public bool BooleanValue { get => _booleanValue; set => _booleanValue = value; }
        public Node? SingleNodeValue { get => _singleNodeValue; set => _singleNodeValue = value; }
        public bool InvalidIteratorState { get => _invalidIteratorState; set => _invalidIteratorState = value; }
        public ulong SnapshotLength { get => _snapshotLength; set => _snapshotLength = value; }

        // CONSTRUCTOR
        private XPathResult() {}

        // PUBLIC METHODS
        public Node? interateNext()
        {
            throw new NotImplementedException();
        }

        public Node? SnapshotItem(ulong index)
        {
            throw new NotImplementedException();
        }
    }

    public enum XPathType
    {
        Any,
        Number,
        String,
        Boolean,
        UnorderedNodeIterator,
        OrderedNodeIterator,
        UnorderedNodeSnapshot,
        OrderedNodeSnapshot,
        AnyUnorderedNode,
        FirstOrderedNode
    }
}