namespace Luxor.DOM.XPath
{
    // https://dom.spec.whatwg.org/#xpathexpression
    public class XPathExpression
    {
        // CONSTRUCTOR
        internal XPathExpression() {}

        // PUBLIC METHODS
        public XPathResult Evaluate(Node contextNode, XPathType? type = XPathType.Any, XPathResult? result = null)
        {
            throw new NotImplementedException();
        }
    }
}