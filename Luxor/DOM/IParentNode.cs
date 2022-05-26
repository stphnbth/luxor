using Luxor.DOM.HTML;

namespace Luxor.DOM
{
    public interface IParentNode
    {
        // PUBLIC PROPERTIES
        public List<HTMLElement> Children { get; }
        public Element? FirstElementChild { get; }
        public Element? LastElementChild { get; }
        public ulong ChildElementCount { get; }

        // PUBLIC METHODS
        public void Prepend(IList<Node> nodes)
        {
            throw new NotImplementedException();
        }

        public void Append(IList<Node> nodes)
        {
            throw new NotImplementedException();
        }

        public void ReplaceChildren(IList<Node> nodes)
        {
            throw new NotImplementedException();
        }
        
        public Element? QuerySelector(string selectors)
        {
            throw new NotImplementedException();
        }
        
        public List<Node> QuerySelectorAll(string selectors)
        {
            throw new NotImplementedException();
        }
    }
}