using static Data.DataTables;

namespace Luxor.DOM
{
    public class CustomElementDefinition
    {
        private string _name;
        private string _localName;
        private CustomElementConstructor _customElementConstructor;
        private List<string> _observedAttributes;
        private Dictionary<string, LCCallback> _lifecycleCallbacks;
        private List<Element> _constructionStack;
        private bool _formAssociated;
        private bool _disableInternals;
        private bool _disableShadow;

        internal string Name { get => _name; }
        internal string LocalName { get => _localName; }
        internal CustomElementConstructor CustomElementConstructor { get => _customElementConstructor; set => _customElementConstructor = value; }

        public bool DisableShadow { get => _disableShadow; set => _disableShadow = value; }

        public static CustomElementDefinition? LookUp(Document document, string? nspace, string localName, string? isValue)
        {
            // 1, 2
            if ((nspace is not null && nspace != HTMLNamespace) || document.BrowsingContext is null) { return null; }

            // 3 TODO: empty CustomElementRegistry
            CustomElementRegistry registry = new CustomElementRegistry();

            // 4, 5
            foreach (CustomElementDefinition definition in registry.Definitions)
            {
                if (definition._localName == localName)
                {
                    if (definition.Name == localName || definition.Name  == isValue)
                    {
                        return definition;
                    }
                }
            }

            return null;
        }
    }

    public class CustomElementRegistry
    {
        private List<CustomElementDefinition> _definitions;
        private bool _definitionIsRunning;
        private Dictionary<string, Task> _whenDefinedMap;

        public List<CustomElementDefinition> Definitions { get => _definitions; set => _definitions = value; }
        public bool DefinitionIsRunning { get => _definitionIsRunning; set => _definitionIsRunning = value; }
        public Dictionary<string, Task> WhenDefinedMap { get => _whenDefinedMap; set => _whenDefinedMap = value; }

        public CustomElementRegistry() {}

        public void Define(string name, CustomElementConstructor constructor, string? extends) { throw new NotImplementedException(); }
        public CustomElementConstructor? Get(string name) { throw new NotImplementedException(); }
        public async Task<CustomElementConstructor> WhenDefine(string name) { throw new NotImplementedException(); }
        public void Upgrade(Node root) { throw new NotImplementedException(); }
    }

    public delegate void LCCallback(string callback);
    public delegate void CustomElementConstructor(HTMLElement el);
}