namespace Luxor.DOM
{
    public class IDOMImplementation
    {
        DocumentType CreateDocumentType(string qualifiedName, string publicID, string systemID)
        {
            throw new NotImplementedException();
        }

        Document CreateDocument(string? nspace, string qualifiedName, DocumentType? doctype = null)
        {
            throw new NotImplementedException();
        }

        Document CreateHTMLDocument(string title)
        {
            throw new NotImplementedException();
        }
    }
}