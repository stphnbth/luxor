using System.Diagnostics;
using System.Text.RegularExpressions;

using Luxor.DOM.HTML;
using Luxor.DOM.XPath;
using static Data.DataTables;

namespace Luxor.DOM
{
    public class Document : Node, INonElementParentNode, IDocumentOrShadowRoot, IParentNode,
        IXPathEvaluator
    {
        // https://dom.spec.whatwg.org/#interface-document

        // PRIVATE FIELDS
        private string _encoding = "utf-8";
        private string _contentType = "application/xml";
        private string? _origin = null;
        private string _type = "html";
        private string _mode = "no-quirks";
        private string _url = "about:blank";

        private string? _baseURL;

        // INTERNAL PROPERTIES
        internal string? BaseURL { get => _baseURL; set => _baseURL = value; }
        internal string Type { get => _type; set => _type = value; }

        // PUBLIC PROPERTIES
        public IDOMImplementation Implementation { get; }
        public string URL { get => _url; }
        public string DocumentURI { get => _url; }
        public string CompatMode { get => _mode == "quirks" ? "BackCompat" : "CSS1Compat"; }
        public string CharacterSet { get => _encoding; }
        public string ContentType { get => _contentType; }
        public DocumentType? Doctype { get; }
        public Element? DocumentElement { get; }

        // PUBLIC OVERRIDES
        public override string NodeName { get => "#document"; }

        // https://html.spec.whatwg.org/#the-document-object

        // PRIVATE FIELDS
        /*
        private string _policyContainer;
        private string _permissionsPolicy;
        private string _moduleMap;
        private string _coOpenerPolicy;

        private string _loadTimingInfo;
        private string _previousDocumentUnloadTiming;

            // RENDER BLOCKING MECHANISM //
        private HashSet<Element> _renderBlockingElements = new HashSet<Element>();
        private bool _allowAddingRenderBlockingElements;
        private bool _renderBlocked;
        */

        private bool _isInitial;
        private string? _browsingContext = null;
        private string? _navigationID = null;
        private string? _currentDocumentReadiness = "complete";
        private bool _activeParser = true;

        private string _domain;
        private string _referrer;
        private string _cookie;
        private string _lastModified;
        private DocumentReadyState _readyState;

        private Location? location;

            // DOM TREE ACCESSORS //
        private string _title;
        private string _dir;
        private HTMLElement? _body;
        private HTMLHeadElement? _head;
        private List<Element> _images;
        private List<Element> _embeds;
        private List<Element> _plugins;
        private List<Element> _links;
        private List<Element> _forms;
        private List<Element> _scripts;
        private HTMLScriptElement? _currentScript;

        /*
            // USER INTERACTION //
        private readonly WindowProxy? _defaultView;
        private string _designMode;
        private bool _hidden;
        private DocumentVisibilityState _visibilityState;

            // EVENT HANDLER IDL ATTRS //
        private EventHandler _onreadystatechange;
        private EventHandler _onvisibilitychange;
        */

        // INTERNAL PROPERTIES
        internal string? BrowsingContext { get => _browsingContext; set => _browsingContext = value; }
        internal string? CurrentDocumentReadiness { get => _currentDocumentReadiness; set => _currentDocumentReadiness = value; }
        internal bool ActiveParser { get => _activeParser; }

        // PUBLIC PROPERTIES
        public string Referrer { get => _referrer; }
        public string Cookie { get => _cookie; } // TODO: add additional cookies
        public string LastModified { get => DateTime.Now.ToString(); } // TODO: keep track of updates
        public DocumentReadyState ReadyState { get => _readyState; }
        public HTMLHeadElement? Head { get => _head; }

        public string Title
        {
            get
            {
                Element? title = GetTitleElement();

                // 1
                // TODO: Element Typeing & Checking??

                // 2
                string value = (title is null) ? "" : title.TextContent;

                // 3
                value.StripAndCollapseASCIIWhitespace();

                // 4
                return value;
            }

            set
            {
                Element? title = GetTitleElement();
                Element? element = null;
                // 1
                // TODO: Element Typeing & Checking??

                // 1
                if (title is null && Head is null) { return; }

                // 2
                else if (title is not null) { element = title; }

                // 3
                else
                {
                    // 3.1
                    element = CreateElement(NodeDocument, _title, HTMLNamespace);
                    Head.Append(element);
                }

                element!.StringReplaceAll(value);
            }
        }

        public HTMLElement Body
        {
            get => _body;
            set
            {
                // 1
                if (value is HTMLBo )

                // 2
            }
        }

        // CONSTRUCTOR
        public Document()
        {

        }

        // HELPER METHODS
        internal Element CreateElement(Document document, string localName, string? nspace, string? prefix = null, string? options = null, bool syncCustomElements = false)
        {
            Element? result = null;

            // 4
            CustomElementDefinition? definition = CustomElementDefinition.LookUp(document, nspace, localName, options);

            if (definition is not null)
            {
                // 5
                if (definition.Name != definition.LocalName)
                {
                    // 5.1, 5.2
                    result = new Element(document, new NamedNodeMap(), HTMLNamespace, prefix, localName, Element.CustomElementState.Undefined, null, options);

                    // 5.3
                    if (syncCustomElements)
                    {
                        try
                        {
                            Element.Upgrade(definition);
                        }
                        catch (DOMException)
                        {
                            // TODO: Error Reporting
                            result.CEState = Element.CustomElementState.Failed;
                        }
                    }
                    else
                    {
                        Element.EnqueueCEUpgrade(result, definition);
                    }
                }
                // 6
                else
                {
                    // 6.1
                    if (syncCustomElements)
                    {
                        try
                        {
                            // 6.1.1
                            var constructor = definition.CustomElementConstructor;

                            // 6.1.2
                            result = constructor();

                            // 6.1.3
                            Debug.Assert((result.CEState != Element.CustomElementState.Failed && result.CEState != Element.CustomElementState.Undefined && result.CEDefinition is not null));

                            // 6.1.4
                            Debug.Assert(result.NamespaceURI == HTMLNamespace);

                            // 6.1.5, 6.1.6, 6.1.7, 6.1.8, 6.1.9
                            if ((result.Attributes is not null && result.Attributes.Count > 0) || result.HasChildNodes() || result.ParentNode is not null || result.OwnerDocument != document || result.LocalName != localName)
                            {
                                throw new DOMException(DOMError.NotSupported);
                            }

                            // 6.1.10
                            result.Prefix = prefix;

                            // 6.1.11
                            result.Is = null;
                        }
                        catch (DOMException)
                        {
                            // TODO: Error Reporting
                            result = new HTMLUnknownElement(document, new NamedNodeMap(), HTMLNamespace, prefix, localName, Element.CustomElementState.Failed, null, null);
                        }
                    }
                    // 6.2
                    else
                    {
                        result = new HTMLElement(document, new NamedNodeMap(), HTMLNamespace, prefix, localName, Element.CustomElementState.Undefined, null, null);
                        Element.EnqueueCEUpgrade(result, definition);
                    }
                }
            }
            // 7
            else
            {
                // 7.1 + 7.2
                result = new Element(document, new NamedNodeMap(), nspace, prefix, localName, Element.CustomElementState.Uncustomized, null, options);

                // 7.3
                if (nspace == HTMLNamespace && (Element.IsValidCustomElementName(localName) || options is not null))
                {
                    result.CEState = Element.CustomElementState.Undefined;
                }
            }

            return result!;
        }

        internal Element CreateElementByNameSpace(string nspace, string qualifiedName, string? options)
        {
            throw new NotImplementedException();
        }

        internal Element? GetTitleElement()
        {
            throw new NotImplementedException();
        }

        internal bool MatchesNameProduction(string name)
        {
            string nameStartChar = @"[:A-Z_a-z\xC0-\xD6\xD8-\xF6\xF8-\u02FF\u0370-\u037D\u037F-\u1FFF\u200C-\u200D\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD]|[\uD800-\uDB7F][\uDC00-\uDFFF]";
            string nameChar = @"[-.0-9\xB7\u0300-\u036F\u203F-\u2040]";
            string pattern = "^(?:" + nameStartChar + ")(?:" + nameStartChar + "|" + nameChar + ")*";

            if (Regex.IsMatch(name, pattern)) { return true; }

            return false;
        }

        internal void UpdateCurrentDocumentReadiness(Document document)
        {
            throw new NotImplementedException();
        }


        // PUBLIC METHODS
        /*

        public List<Element> GetElementsyTagName(string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public List<Element> GetElementsByTagNameNS(string nspace, string localName)
        {
            throw new NotImplementedException();
        }

        public List<Element> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        public List<Element> GetElementsByName(string elementName)
        {
            throw new NotImplementedExceptions();
        }

        */

        public Element CreateElementByName(string localName, string? options)
        {
            // 1
            if (!MatchesNameProduction(localName)) { throw new DOMException(DOMError.InvalidCharacter); }

            // 2
            if (Type == "html") { localName = localName.ToLower(); }

            // 5
            string? nspace = (Type == "html" || ContentType == "application/xhtml+xml") ? HTMLNamespace : null;

            return CreateElement(this, localName, nspace, null, options, true);
        }

        public Node ImportNode(Node node, bool deep=false)
        {
            throw new NotImplementedException();
        }

        public Node AdoptNode(Node node)
        {
            throw new NotImplementedException();
        }

        public List<Node> getElementsByName(string elementName)
        {
            throw new NotImplementedException();
        }

        /*
            // DYNAMIC MARKUP INSERTION //
        public Document Open()
        {
            throw new NotImplementedException();
        }

        public WindowProxy? Open(string url, string name, string features)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Write(string text)
        {
            throw new NotImplementedException();
        }

        public void Writeln(string text)
        {
            throw new NotImplementedException();
        }

            //  USER INTERACTION //
        public boolean HasFocus()
        {
            throw new NotImplementedException();
        }

        public boolean ExecCommand(string commandId, boolean showUI = false, string value = "")
        {
            throw new NotImplementedException();
        }

        public boolean QueryCommandEnabled(string commandID)
        {
            throw new NotImplementedException();
        }

        public boolean QueryCommandIndeterm(string commandID)
        {
            throw new NotImplementedException();
        }

        public boolean QueryCommandState(string commandID)
        {
            throw new NotImplementedException();
        }

        public boolean QueryCommandSupported(string commandID)
        {
            throw new NotImplementedException();
        }

        public boolean QueryCommandValue(string commandID)
        {
            throw new NotImplementedException();
        }
        */

        // ENUMS

        public enum DocumentReadyState
        {
            Loading,
            Interactive,
            Complete
        }

        public enum DocumentVisibilityState
        {
            Visible,
            Hidden
        }
    }

    public interface IDocumentOrShadowRoot {}

    public struct ElementCreationOptions
    {
        string is;
    }
}
