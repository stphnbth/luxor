
namespace Luxor.DOM
{
    public interface INodeFilter
    {
        protected NodeFilter Filter { get; }

        public static ushort AcceptNode(Node node)
        {
            throw new NotImplementedException();
        }
    }

    public enum NodeFilter
    {
        Accept,
        Reject,
        Skip
    }

    public enum NodeFilterShow
    {
        All,
        Element,
        Attribute,
        Text,
        CDATA,
        EntityReference,
        Entity,
        ProcessingInstruction,
        Comment,
        Document,
        DocumentType,
        DocumentFragment,
        Notation
    }
}