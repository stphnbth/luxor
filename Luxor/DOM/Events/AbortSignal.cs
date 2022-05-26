namespace Luxor.DOM.Events
{
    public class AbortSignal : EventTarget
    {
        // PRIVATE FIELDS
        private bool _aborted;
        private string _reason;

        private EventHandler _onabort;

        // PUBLIC PROPERTIES
        public bool Aborted { get => _aborted; }
        public string Reason { get => _reason; }

        // CONSTRUCTOR
        public AbortSignal() {}

        // PUBLIC METHODS
        public AbortSignal abort(string reason)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfAborted()
        {
            throw new NotImplementedException();
        }
    }
}