using Luxor.Parser;

namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/forms.html#htmlformelement
    public class HTMLFormElement : HTMLElement
    {
        // PRIVATE FIELDS
        private string _acceptCharset;
        private string _action;
        private string _autocomplete;
        private string _enctype;
        private string _encoding;
        private string _method;
        private string _name;
        private bool _noValidate;
        private string _target;
        private string _rel;
        private List<string> _relList;
        private List<Element> _elements;
        private ulong _length;

        // PUBLIC PROPERTIES
        public string AcceptCharset { get => _acceptCharset; set => _acceptCharset = value; }
        public string Action { get => _action; set => _action = value; }
        public string Autocomplete { get => _autocomplete; set => _autocomplete = value; }
        public string Enctype { get => _enctype; set => _enctype = value; }
        public string Encoding { get => _encoding; set => _encoding = value; }
        public string Method { get => _method; set => _method = value; }
        public string Name { get => _name; set => _name = value; }
        public bool NoValidate { get => _noValidate; set => _noValidate = value; }
        public string Target { get => _target; set => _target = value; }
        public string Rel { get => _rel; }
        public List<string> RelList { get => _relList; }
        public List<Element> Elements { get => _elements; }
        public ulong Length { get => _length; }

        // CONSTRUCTOR
        public HTMLFormElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public void submit()
        {
            throw new NotImplementedException();
        }

        public void requestSubmit(HTMLElement? submitter = null) 
        {
            throw new NotImplementedException();
        }
        
        public void reset() 
        {
            throw new NotImplementedException();
        }
        
        public bool checkValidity() 
        { 
            throw new NotImplementedException(); 
        }
        
        public bool reportValidity() 
        { 
            throw new NotImplementedException(); 
        }
    }
}