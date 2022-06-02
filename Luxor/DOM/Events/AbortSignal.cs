namespace Luxor.DOM.Events
{
    public class AbortSignal : EventTarget
    {
        // PRIVATE FIELDS
        private bool _aborted;
        private string? _reason;

        private List<Action> _abortAlgorithms;

        // PUBLIC PROPERTIES
        public bool Aborted { get => _aborted; }
        public string? Reason { get => _reason; }

        // EVENT HANDLERS
        public EventHandler? _onabort;

        // CONSTRUCTOR
        internal AbortSignal()
        {
            _abortAlgorithms = new List<Action>();
        }

        // PUBLIC METHODS
        public AbortSignal abort(string reason)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfAborted()
        {
            throw new NotImplementedException();
        }

        // HELPER FUNCTIONS
        internal void Add(Action algorithm)
        {
            if (_aborted) { return; }
            _abortAlgorithms.Add(algorithm);
        }

    }
}