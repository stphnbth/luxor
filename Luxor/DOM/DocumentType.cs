using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Data.DataTables;

namespace Luxor.DOM
{
    public class DocumentType : Node
    {
        private string _name;
        private string _publicID;
        private string _systemID;

        public string Name { get => _name; }
        public string PublicID { get => _publicID; }
        public string SystemID { get => _systemID; }

        public override string NodeName { get => Name; }

        public DocumentType(Document ownerDocument) : base(ownerDocument) {}
    }

    
}
