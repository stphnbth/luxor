using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Data.DataTables;

namespace Luxor.DOM
{
    public class DOMImplementation
    {
        Document CreateDocument(string namespaceURI, string qualifiedName, DocumentType doctype)
        {
            throw new NotImplementedException();
        }
        
        DocumentType CreateDocumentType(string qualifiedName, string pubID, string sysID)
        {
            throw new NotImplementedException();
        }   
    }
}