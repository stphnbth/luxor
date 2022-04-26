namespace Luxor.DOM
{
    internal class HTMLTemplateElement : HTMLElement
    {
        protected DocumentFragment Content { get; set; }

        public HTMLTemplateElement(DocumentFragment content)
        {
            Content = content;
        }
    }
}