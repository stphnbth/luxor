using System.Diagnostics;
using System.Text.RegularExpressions;

using static Data.DataTables;

namespace Luxor.DOM
{
    public class Document : Node
    {
        private string _encoding = "utf-8";
        private string _contentType = "application/xml";
        private string? _origin = null;
        private string _type = "html";
        private string _mode = "no-quirks";
        private string _url = "about:blank";

        private string? _baseURL;
        private string? _browsingContext = null;

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


        /*
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
        */

        internal string? BaseURL { get => _baseURL; set => _baseURL = value; }
        internal string? BrowsingContext { get => _browsingContext; set => _browsingContext = value; }
        internal string Type { get => _type; set => _type = value; }

        public DOMImplementation Implementation { get; }
        public string URL { get => _url; }
        public string DocumentURI { get => _url; }
        public string CompatMode { get => _mode == "quirks" ? "BackCompat" : "CSS1Compat"; }
        public string CharacterSet { get => _encoding; }
        public string ContentType { get => _contentType; }
        public DocumentType? Doctype { get; }
        public Element? DocumentElement { get; }

        public override string NodeName { get => "#document"; }

        public Document()
        {

        }



        /*

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

        internal Element CreateElementByNameSpace(string nspace, string qualifiedName, string? options)

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

        private bool MatchesNameProduction(string name)
        {
            string nameStartChar = @"[:A-Z_a-z\xC0-\xD6\xD8-\xF6\xF8-\u02FF\u0370-\u037D\u037F-\u1FFF\u200C-\u200D\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD]|[\uD800-\uDB7F][\uDC00-\uDFFF]";
            string nameChar = @"[-.0-9\xB7\u0300-\u036F\u203F-\u2040]";
            string pattern = "^(?:" + nameStartChar + ")(?:" + nameStartChar + "|" + nameChar + ")*";

            if (Regex.IsMatch(name, pattern)) { return true; }

            return false;
        }

        private Element CreateElement(Document document, string localName, string? nspace, string? prefix, string? options, bool syncCustomElements)
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


        // WindowProxy? open(string url, string name, string features)
    }
}
