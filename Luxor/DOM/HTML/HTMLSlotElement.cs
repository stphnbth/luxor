namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/scripting.html#htmlslotelement
    public class HTMLSlotElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _name;


        // PUBLIC PROPERTIES
        // TODO: HTMLSlotElement

        // CONSTRUCTOR
        public HTMLSlotElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public IEnumerator<Node> assignedNodes(AssignedNodesOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Element> assignedElements(AssignedNodesOptions? options = null)
        {
            throw new NotImplementedException();
        }
        
    }

    public struct AssignedNodesOptions
    {
        public bool flatten = false;
        public AssignedNodesOptions() {}
    }
}