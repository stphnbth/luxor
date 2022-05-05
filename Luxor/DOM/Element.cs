using System.Text.RegularExpressions;

using static Data.DataTables;

namespace Luxor.DOM
{
    public class Element : Node 
    {
        private ShadowRoot? _shadowRoot;
        private string? _is = null;
        private CustomElementState _customElementState = CustomElementState.Undefined;

        protected NamedNodeMap? Attributes { get; }
        protected List<string> ClassList { get; }
        protected string ClassName { get; set; }
        protected string ID { get; set; }
        protected string LocalName { get; }
        public string? NamespaceURI { get; }
        protected string? Prefix { get; }
        protected string Slot { get; set; }
        protected string TagName { get; }
        
        public ShadowRoot? ShadowRoot 
        { 
            get
            {
                if (_shadowRoot is null || _shadowRoot.Mode == ShadowRootMode.Closed)
                {
                    return null;
                }

                return _shadowRoot;
            } 
            private set => _shadowRoot = value; 
        }

        public Element(Document? ownerDocument) : base(ownerDocument)
        {

        }


        public ShadowRoot AttachShadow(ShadowRootMode mode, bool delegatesFocus, SlotAssignmentMode slotAssignment)
        {
            // 1
            if (NamespaceURI != HTMLNamespace) { throw new DOMException(DOMError.NotSupported); }

            // 2
            bool validCEN = IsValidCustomElementName(LocalName);
            if (!(validCEN && ValidLocalNames.Contains(LocalName))) { throw new DOMException(DOMError.NotSupported); }

            // 3
            if (validCEN || _is is not null)
            {
                CustomElement? definition = CustomElement.LookUp(OwnerDocument!, NamespaceURI, LocalName, _is);
                if (definition is not null && definition.DisableShadow) { throw new DOMException(DOMError.NotSupported); }
            }

            // 4
            if (IsShadowHost()) { throw new DOMException(DOMError.NotSupported); }

            // 5, 8
            ShadowRoot shadow = new ShadowRoot(OwnerDocument, this, mode, slotAssignment);

            // 6
            shadow.DelegatesFocus = delegatesFocus;

            // 7
            if (_customElementState == CustomElementState.Precustomized || _customElementState == CustomElementState.Custom)
            {
                shadow.AvailableToInternals = true;
            }

            // 9
            ShadowRoot = shadow;

            // 10
            return shadow;
        }
        
        // TODO: CSS Selector Parsing
        // public Element? Closest(string selectors) { throw new NotImplementedException(); }
        
        public string? GetAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        public string? GetAttributeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        public List<String> GetAttributeNames()
        {
            throw new NotImplementedException();
        }
        
        public Attr? GetAttributeNode(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        public Attr? GetAttributeNodeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        public List<Element> GetElementsByTagName(string qualifiedName)
        {
            
        }
        
        public List<Element> GetElementsByTagNameNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        public List<Element> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }
        
        public bool HasAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        public bool HasAttributes()
        {
            throw new NotImplementedException();
        }
        
        public bool HasAttributeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        // TODO: CSS Selector Parsing
        // public bool Matches(string selectors) { throw new NotImplementedException(); }
        
        public void RemoveAttribute(string qualifiedName)
        {
            throw new NotImplementedException();
        }
        
        public void RemoveAttributeNS(string? nspace, string localName)
        {
            throw new NotImplementedException();
        }
        
        public Attr RemoveAttributeNode(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        public void SetAttribute(string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }
        
        public void SetAttributeNS(string? nspace, string qualifiedName, string value)
        {
            throw new NotImplementedException();
        }
        
        public bool ToggleAttribute(string qualifiedName, bool? force)
        {
            throw new NotImplementedException();
        }
        
        public Attr? SetAttributeNode(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        public Attr? SetAttributeNodeNS(Attr attr)
        {
            throw new NotImplementedException();
        }
        
        private bool IsValidCustomElementName(string name)
        {
            string charPattern = @"(?:[\.0-9_a-z\xB7\xC0-\xD6\xD8-\xF6\xF8-\u037D\u037F-\u1FFF\u200C\u200D\u203F\u2040\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD]|[\uD800-\uDB7F][\uDC00-\uDFFF])";
            string namePattern = "^[a-z]" + charPattern + "*-" + charPattern + "*$";
            
            if (!Regex.IsMatch(name, namePattern)) { return false; }

            if (InvalidCustomElementNames.Contains(name)) { return false; }

            return true;
        }

        private bool IsShadowHost()
        {
            if (ShadowRoot is not null) { return true; }
            return false;
        }

        private List<Element> GetElementsByQualifiedName(Node root, string qualifiedName)
        {
            // 1
            if (qualifiedName == "*")
            {

            }
            // 2
            else if (root.OwnerDocument.Type == "html")
            {

            }
            // 3
            else
            {
                
            }
        }

        public enum CustomElementState 
        {
            Undefined,
            Failed,
            Uncustomized,
            Precustomized,
            Custom
        }
    }
}