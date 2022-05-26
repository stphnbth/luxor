namespace Luxor.DOM.Events
{
    // https://dom.spec.whatwg.org/#customevent
    public class CustomEvent : Event
    {
        // PRIVATE FIELDS
        private object _detail;

        // PUBLIC PROPERTIES
        public object Detail { get => _detail; set => _detail = value; }


        // CONSTRUCTOR
        CustomEvent(string type, CustomEventInit? eventInit = null) : base(type) {}

    }

    public struct CustomEventInit
    {
        public object? detail = null;
        public CustomEventInit() {}
    }
}