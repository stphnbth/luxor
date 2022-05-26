namespace Luxor.DOM.XPath
{
    //https://dom.spec.whatwg.org/#xpathevaluator
    public class XPathEvaluator : IXPathEvaluator
    {
        // CONSTRUCTOR
        public XPathEvaluator() {}
    }

    // https://dom.spec.whatwg.org/#xpathevaluatorbase
    public interface IXPathEvaluator
    {
        public XPathExpression CreateExpression(string expression, XPathNSResolver? resolver = null)
        {
            throw new NotImplementedException();
        }

        public XPathNSResolver CreateNSResolver(Node nodeResolver)
        {
            throw new NotImplementedException();
        }
        
        public XPathResult Evaluate(string expression, Node contextNode, XPathNSResolver? resolver = null, XPathType? type = XPathType.Any, XPathResult? result = null)
        {
            throw new NotImplementedException();
        }

    }
}