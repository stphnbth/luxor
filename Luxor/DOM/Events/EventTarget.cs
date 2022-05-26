namespace Luxor.DOM.Events
{
    public abstract class EventTarget
    {
        // PRIVATE FIELDS

        // CONSTRUCTOR
        public EventTarget() {}

        // PUBLIC METHODS
        // TODO: FUCKING CALLBACK FUNCTIONS
        public void AddEventListener(string type, EventListener? callback, AddEventListenerOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveEventListener(string type, EventListener? callback, EventListenerOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public bool DispatchEvent(Event toDispatch)
        {
            throw new NotImplementedException();
        }
    }

    public struct EventListenerOptions
    {
        public bool capture;
    }

    public struct AddEventListenerOptions
    {
        public bool capture;
        public bool passive;
        public bool once;
        public AbortSignal signal;
    }
}