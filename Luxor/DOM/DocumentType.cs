using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Data.DataTables;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#documenttype
    public class DocumentType : Node, IChildNode
    {
        // PRIVATE FIELDS
        private string _name;
        private string _publicID;
        private string _systemID;

        // PUBLIC PROPERTIES
        public string Name { get => _name; }
        public string PublicID { get => _publicID; }
        public string SystemID { get => _systemID; }

        public override string NodeName { get => Name; }

        // CONSTRUCTOR
        public DocumentType(Document ownerDocument) : base(ownerDocument) {}
    }

    
}
