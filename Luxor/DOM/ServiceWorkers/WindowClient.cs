namespace Luxor.DOM.ServiceWorkers
{
    public class WindowClient : Client
    {
        // PRIVATE FIELDS
        private VisibilityState _visibilityState;
        private bool _focused;
        private List<string> _ancestorOrigins;

        // PUBLIC PROPERTIES
        public VisibilityState VisibilityState { get => _visibilityState; }
        public bool Focused { get => _focused; }
        public List<string> AncestorOrigins { get => _ancestorOrigins; set => _ancestorOrigins = value; }

        // CONSTRUCTOR
        public WindowClient() : base()
        {
            _ancestorOrigins = new List<string>();
        }
    }

    public enum VisibilityState
    {
        hidden,
        visible
    }
}