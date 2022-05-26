namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#attr
    public class Attr : Node
    {
        // PRIVATE FIELDS
        private string _namespace;
        private string _namespacePrefix;
        private string _localName;
        private string _qualifedName;
        private string _value;
        private Element _element;

        // PUBLIC PROPERTIES
        public string NamespaceURI { get => _namespace; }
        public string Prefix { get => _namespacePrefix; }
        public string LocalName { get => _localName; }
        public string Name { get => _qualifedName; }

        public string Value
        {
            get => _value;

            // https://dom.spec.whatwg.org/#set-an-existing-attribute-value
            set
            {
                // 1
                if (_element is null) { _value = value; }

                // TODO: change attribute to value
                // 2
            }
        }

        public Element OwnerElement { get => _element; }

        // PUBLIC OVERRIDES
        public override string NodeName { get => Name; }

        public override string? NodeValue {
            get => Value;
            set
            {
                if (value is null) { value = ""; }
                SetExistingValue(this, value);
            }
        }

        public override string? TextContent { get => NodeValue; set => NodeValue = value; }

        // CONSTRUCTOR
        public Attr(Document ownerDocument) : base(ownerDocument) {}

        // PUBLIC METHODS
        public void SetExistingValue(Attr attribute, string value)
        {
            throw new NotImplementedException();
        }

   }

}