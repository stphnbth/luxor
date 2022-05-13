using Luxor.Parser;

namespace Luxor.DOM.HTML
{
    public class HTMLScriptElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _src;
        private string _type;
        private bool _noModule;
        private bool _async;
        private bool _defer;
        private string? _crossOrigin;
        private string _text;
        private string _integrity;
        private string _referrerPolicy;
        private List<Token> _blocking;

        // PUBLIC PROPERTIES
        public string Src { get => _src; set => _src = value; }
        public string Type { get => _type; set => _type = value; }
        public bool NoModule { get => _noModule; set => _noModule = value; }
        public bool Async { get => _async; set => _async = value; }
        public bool Defer { get => _defer; set => _defer = value; }
        public string? CrossOrigin { get => _crossOrigin; set => _crossOrigin = value; }
        public string Text { get => _text; set => _text = value; }
        public string Integrity { get => _integrity; set => _integrity = value; }
        public string ReferrerPolicy { get => _referrerPolicy; set => _referrerPolicy = value; }
        public List<Token> Blocking { get => _blocking; }

        // CONSTRUCTOR
        public HTMLScriptElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        static bool supports(string type)
        {
            throw new NotImplementedException();
        }
    }
}
