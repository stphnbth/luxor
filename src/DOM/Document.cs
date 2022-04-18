namespace Luxor.DOM
{
    public class Document // : Node
    {
        private string? _origin = null;
        private string _type = "html";
        private string _mode = "no-quirks";
        
        protected DOMImplementation Implementation { get; }
        protected string URL { get; }
        protected string CompatMode { get => _mode == "quirks" ? "BackCompat" : "CSS1Compat"; }
        protected string ContentType { get; }
        protected DocumentType? Doctype { get; }
        protected Element? DocumentElement { get; }

        internal string CharacterSet { get; }

        public string DocumentURI { get => URL; } 

        private enum DocumentReadyState
        {
            Loading,
            Interactive,
            Complete
        }

        private enum DocumentVisibilityState
        {
            Visible,
            Hidden
        }

        private readonly Location? location;
        private readonly DocumentReadyState readyState;

        private readonly string referrer;
        private readonly string lastModified;
        private string domain;
        private string cookie;

        private string title;
        private string dir;
        private HTMLElement? body;
        private readonly HTMLHeadElement? head;
        private readonly List<Element> images;
        private readonly List<Element> embeds;
        private readonly List<Element> plugins;
        private readonly List<Element> links;
        private readonly List<Element> forms;
        private readonly List<Element> scripts;
        
        private readonly HTMLScriptElement? currentScript;
        private readonly bool hidden;
        private readonly DocumentVisibilityState visibilityState;
        private string designMode;

        // private readonly WindowProxy? defaultView;        
        // private EventHandler onreadystatechange;
        // private EventHandler onvisibilitychange;

        public Document()
        {
            /*
            Type = NodeType.Document;
            Name = "#document";
            */
            
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

        List<Node> getElementsByName(string elementName)
        {
            throw new NotImplementedException();
        }

        Document open(string unused1, string unused2)
        {
            throw new NotImplementedException();
        }

        void close()
        {
            throw new NotImplementedException();
        }

        void write(string text)
        {
            throw new NotImplementedException();
        }

        void writeln(string text)
        {
            throw new NotImplementedException();
        }

        bool hasFocus()
        {
            throw new NotImplementedException();
        }

        bool execCommand(string commandId, bool showUI = false, string value = "")
        {
            throw new NotImplementedException();
        }

        bool queryCommandEnabled(string commandId)
        {
            throw new NotImplementedException();
        }

        bool queryCommandIndeterm(string commandId)
        {
            throw new NotImplementedException();
        }

        bool queryCommandState(string commandId)
        {
            throw new NotImplementedException();
        }

        bool queryCommandSupported(string commandId)
        {
            throw new NotImplementedException();
        }

        string queryCommandValue(string commandId)
        {
            throw new NotImplementedException();
        }


        // WindowProxy? open(string url, string name, string features)     
    }
}
