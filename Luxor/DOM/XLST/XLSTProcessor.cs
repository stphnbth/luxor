namespace Luxor.DOM.XSLT
{
    public class XSLTProcessor
    {
        // CONSTRUCTOR
        public XSLTProcessor() {}

        // PUBLIC METHODS
        void importStylesheet(Node style)
        {
            throw new NotImplementedException();
        }

        DocumentFragment transformToFragment(Node source, Document output)
        {
            throw new NotImplementedException();
        }

        Document transformToDocument(Node source)
        {
            throw new NotImplementedException();
        }
        
        void setParameter(string namespaceURI, string localName, object value)
        {
            throw new NotImplementedException();
        }
        
        object getParameter(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }
        
        void removeParameter(string namespaceURI, string localName)
        {
            throw new NotImplementedException();
        }
        
        void clearParameters()
        {
            throw new NotImplementedException();
        }
        
        void reset()
        {
            throw new NotImplementedException();
        }
    }
}