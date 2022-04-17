namespace Luxor.DOM
{
    public class ElementInternals : Element 
    {
        readonly ShadowRoot? shadowRoot;
        readonly bool willValidate;
        readonly string validationMessage;
        readonly List<Node> labels;
    }
}