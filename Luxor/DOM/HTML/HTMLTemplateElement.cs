namespace Luxor.DOM.HTML
{
    internal class HTMLTemplateElement : HTMLElement
    {
        // PRIVATE FIELDS
        private DocumentFragment _content;

        // PUBLIC PROPERTIES
        public DocumentFragment Content { get => _content; }

        // CONSTUCTOR
        public HTMLTemplateElement(Document nodeDocument, DocumentFragment content) : base(nodeDocument)
        {
            _content = content;
        }
    }
}