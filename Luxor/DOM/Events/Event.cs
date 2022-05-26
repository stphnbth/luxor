namespace Luxor.DOM.Events
{
    public abstract class Event
    {
        // PUBLIC FIELDS
        private string _type;
        private EventTarget? _target;
        private EventTarget? _currentTarget;
        private ushort _eventPhase;
        private bool _bubbles;
        private bool _cancelable;
        private bool _defaultPrevented;
        private bool _composed;
        private bool _isTrusted;
        private double _timeStamp;

        // PUBLIC PROPERTIES
        public string Type { get => _type; }
        public EventTarget? Target { get => _target; }
        public EventTarget? CurrentTarget { get => _currentTarget; }
        public ushort EventPhase { get => _eventPhase; }
        public bool Bubbles { get => _bubbles; }
        public bool Cancelable { get => _cancelable; }
        public bool DefaultPrevented { get => _defaultPrevented; }
        public bool Composed { get => _composed; }
        public bool IsTrusted { get => _isTrusted; }
        public double TimeStamp { get => _timeStamp; }

        // CONSTRUCTOR
        public Event(string type, EventInit? eventInitDict = null) {}

        // PUBLIC METHODS
        public List<EventTarget> ComposedPath()
        {
            throw new NotImplementedException();
        }

        public void StopPropagation()
        {
            throw new NotImplementedException();
        }

        public void StopImmediatePropagation()
        {
            throw new NotImplementedException();
        }
        
        public void PreventDefault()
        {
            throw new NotImplementedException();
        }
        
    }

    public struct EventInit
    {
        public bool bubbles;
        public bool cancelable;
        public bool composed;
    }
}