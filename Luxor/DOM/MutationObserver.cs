namespace Luxor.DOM
{
    public class MutationObserver
    {
        // CONSTRUCTOR
        MutationObserver(MutationCallback callback) {}

        // PUBLIC METHODS
        public void Observe(Node target, MutationObserverInit? options = null)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public List<MutationRecord> TakeRecords()
        {
            throw new NotImplementedException();
        }
        
    }

    public readonly struct MutationRecord
    {
        public readonly string type;
        public readonly Node target;
        public readonly List<Node> addedNodes;
        public readonly List<Node> removedNodes;
        public readonly Node? previousSibling;
        public readonly Node? nextSibling;
        public readonly string attributeName;
        public readonly string attributeNamespace;
        public readonly string oldValue;
    }

    public struct MutationObserverInit
    {
        public bool childList;
        public bool attributes;
        public bool characterData;
        public bool subtree;
        public bool attributeOldValue;
        public bool characterDataOldValue;
        public List<string> attributeFilter;
    }
}