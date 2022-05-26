namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/interactive-elements.html#htmldialogelement
    public class HTMLDialogElement : HTMLElement
    {
        // PRIVATE FIELDS
        private bool _open;
        private string _returnValue;

        // PUBLIC PROPERTIES
        // TODO: HTMLDialogElement

        // CONSTRUCTOR
        public HTMLDialogElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public void show()
        {
            throw new NotImplementedException();
        }

        public void showModal()
        {
            throw new NotImplementedException();
        }
        
        public void close(string returnValue = "")
        {
            throw new NotImplementedException();
        }
        
    }
}