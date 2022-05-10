using System.Text.RegularExpressions;

using static Data.DataTables;

namespace Luxor.DOM
{
    public class Element : Node 
    {
        private NamedNodeMap? _attributes;
        private List<string> _classList;
        private string _className;
        private string _id;
        private string _localName;
        private string? _namespace;
        private string? _namespacePrefix;
        private string _slot;

        private string? _is = null;
        private CustomElementState _customElementState;
        private CustomElementDefinition? _customElementDefinition;
        private ShadowRoot? _shadowRoot;

        private string QualifiedName
        {
            get
            {
                if (_namespacePrefix is null) { return _localName; }

                return _namespacePrefix + ":" + _localName;
            }
        }

        internal string? Is { get => _is; set => _is = value; }
        internal CustomElementState CEState { get => _customElementState; set => _customElementState = value; }
        internal CustomElementDefinition? CEDefinition { get => _customElementDefinition; }

        public Element(Document ownerDocument, NamedNodeMap attributes, string? nspace, string? prefix, string localName, CustomElementState ceState, CustomElementDefinition? ceDefinition, string? options) : base(ownerDocument)
        {
            _attributes = attributes;
            _namespace = nspace;
            _namespacePrefix = prefix;
            _localName = localName;
            _customElementState = ceState;
            _customElementDefinition = ceDefinition;
            _is = options;

            _classList = new List<string>();
        }

        public NamedNodeMap? Attributes { get => _attributes; set => _attributes = value; }
        public List<string> ClassList { get => _classList; }
        public string ClassName { get => _className; set => _className = value; }
        public string Id { get => _id; set => _id = value; }
        public string LocalName { get => _localName; }
        public string? NamespaceURI { get => _namespace; }
        public string? Prefix { get => _namespacePrefix; internal set => _namespacePrefix = value; }
        public string Slot { get => _slot; set => _slot = value; }

        public string TagName
        {
            get
            {
                string qn = QualifiedName;

                if (_namespace == HTMLNamespace && OwnerDocument.Type == "html")
                {
                    qn = qn.ToUpper();
                }

                return qn;
            }
        }

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
                CustomElementDefinition? definition = CustomElementDefinition.LookUp(OwnerDocument!, NamespaceURI, LocalName, _is);
                if (definition is not null && definition.DisableShadow) { throw new DOMException(DOMError.NotSupported); }
            }

            // 4
            if (IsShadowHost()) { throw new DOMException(DOMError.NotSupported); }

            // 5, 8CustomElementState1
            ShadowRoot shadow = new ShadowRoot(OwnerDocument, this, mode, slotAssignment);

            // 6
            shadow.DelegatesFocus = delegatesFocus;

            // 7
            if (CEState == CustomElementState.Precustomized || CEState == CustomElementState.Custom)
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
            return GetElementsByQualifiedName(this, qualifiedName);
        }

        public List<Element> GetElementsByTagNameNS(string? nspace, string localName)
        {
            return GetElementsByNamespaceAndLocalName(this, nspace, localName);
        }

        public List<Element> GetElementsByClassName(string classNames)
        {
            return new List<Element>();
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
        
        internal static bool IsValidCustomElementName(string name)
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

        private static List<Element> GetElementsByQualifiedName(Node root, string qualifiedName)
        {
            // 1
            if (qualifiedName == "*")
            {
                return FilterDescendants(root);
            }
            // 2
            else if (root.OwnerDocument.Type == "html")
            {
                return FilterDescendants(root, filter);
            }
            // 3
            else
            {
                return FilterDescendants(root, filter);
            }
        }

        public static List<Element> FilterDescendants(Node root, List<Predicate<Element>> filter)
        {
            List<Element> descendants = root.ChildNodes;
            return descendants => filter.All(predicate => predicate(descendants))
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