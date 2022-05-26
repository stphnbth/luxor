namespace Luxor.DOM.Events
{
    public class AbortController
    {
        // PRIVATE FIELDS
        private AbortSignal _signal;

        // PUBLIC PROPERTIES
        public AbortSignal Signal { get => _signal; }

        // CONSTRUCTOR
        public AbortController() {}

        // PUBLIC METHODS
        public void Abort(object? reason = null)
        {
            throw new NotImplementedException();
        }


    }
}