using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Extensions;
using static Reference.DataTables;

namespace Luxor.DOM
{
    public class Document : Node
    {
        private string? _origin = null;
        private string _type = "html";
        private string _mode = "no-quirks";
        
        protected DOMImplementation Implementation { get; }
        protected string URL { get; }
        protected string CompatMode { get => _mode == "quirks" ? "BackCompat" : "CSS1Compat"; }
        protected string CharacterSet { get; }
        protected string ContentType { get; }
        protected DocumentType? Doctype { get; }
        protected Element? DocumentElement { get; }

        public string DocumentURI { get => URL; } 

        public Document()
        {
            Type = NodeType.Document;
            Name = "#document";

            Implementation = new DOMImplementation();
            CharacterSet = "UTF8";
            ContentType = "application/xml";
            URL = "about:blank";
        }
        
        List<Element> GetElementsyTagName(string qualifiedName) 
        {
            throw new NotImplementedException();
        }

        List<Element> GetElementsByTagNameNS(string nspace, string localName)
        {
            throw new NotImplementedException();
        }

        List<Element> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        Node ImportNode(Node node, bool deep=false)
        {
            throw new NotImplementedException();
        }

        Node AdoptNode(Node node)
        {
            throw new NotImplementedException();
        }        
    }
}
