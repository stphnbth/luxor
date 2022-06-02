namespace Luxor.DOM.Events
{
    // https://dom.spec.whatwg.org/#customevent
    public class CustomEvent : Event
    {
        // PRIVATE FIELDS
        private object? _detail;

        // PUBLIC PROPERTIES
        public object? Detail { get => _detail; }


        // CONSTRUCTOR
        public CustomEvent(string type) : base (type) { _detail = null; }
        public CustomEvent(string type, CustomEventInit eventInit) : base(type, eventInit.init.bubbles, eventInit.init.cancelable)
        {
            _detail = eventInit.detail;
        }
    }

    public struct CustomEventInit
    {
        public EventInit init = new EventInit();
        public object? detail = null;
        public CustomEventInit() {}
    }
}