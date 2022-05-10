namespace Luxor.DOM
{
    public class HTMLElement // : Element 
    {
        
        private string _accessKey;
        private string _accessKeyLabel;
        private string _autocapitalize;
        private string _dir;
        private bool _draggable;
        private bool _hidden;
        private bool _inert;
        private string _innerText;
        private string _lang;
        private string _outerText;
        private bool _spellcheck;
        private string _title;        
        private bool _translate;

        public string AccessKey { get => _accessKey; set => _accessKey = value; }
        public string AccessKeyLabel { get => _accessKeyLabel; set => _accessKeyLabel = value; }
        public string Autocapitalize { get => _autocapitalize; set => _autocapitalize = value; }
        public string Dir { get => _dir; set => _dir = value; }
        public bool Draggable { get => _draggable; set => _draggable = value; }
        public bool Hidden { get => _hidden; set => _hidden = value; }
        public bool Inert { get => _inert; set => _inert = value; }
        public string InnerText { get => _innerText; set => _innerText = value; }
        public string Lang { get => _lang; set => _lang = value; }
        public string OuterText { get => _outerText; set => _outerText = value; }
        public bool Spellcheck { get => _spellcheck; set => _spellcheck = value; }
        public string Title { get => _title; set => _title = value; }
        public bool Translate { get => _translate; set => _translate = value; }

        public ElementInternals attachInternals()
        {
            throw new NotImplementedException();
        }
    }

    public class HTMLUnknownElement : HTMLElement
    {
        
    }
}
