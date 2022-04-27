using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using static Reference.DataTables;

namespace Luxor.DOM
{
    public class DocumentType // : Node
    {
        protected string? PubID { get; }
        protected string? SysID { get; }

        public DocumentType() { }

        public DocumentType(string name, string pubID = "", string sysID = "")
        {
            /*
            Type = NodeType.DocumentType;
            Name = name;
            */
            
            if (!pubID.Equals(""))
                PubID = pubID;

            if (!sysID.Equals(""))
                SysID = sysID;
        }
    }

    
}
