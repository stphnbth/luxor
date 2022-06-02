using Luxor.DOM.ServiceWorkers;

namespace Luxor.DOM.Events
{
    public abstract class EventTarget
    {
        // PRIVATE FIELDS
        private List<EventListener> _eventListenerList;
        private Func<EventTarget>? _getTheParent;
        private Func<Event, EventTarget>? _activationBehaviour;
        private Func<Event, EventTarget>? _legacyPreActivationBehaviour;
        private Func<Event, EventTarget>? _legacyCanceledActivationBehaviour;

        // PROTECTED PROPERTIES
        protected List<EventListener> EventListenerList { get => _eventListenerList; set => _eventListenerList = value; }
        protected Func<EventTarget>? GetTheParent { get => _getTheParent; set => _getTheParent = value; }
        protected Func<Event, EventTarget>? ActivationBehaviour { get => _activationBehaviour; set => _activationBehaviour = value; }
        protected Func<Event, EventTarget>? LegacyPreActivationBehaviour { get => _legacyPreActivationBehaviour; set => _legacyPreActivationBehaviour = value; }
        protected Func<Event, EventTarget>? LegacyCanceledActivationBehaviour { get => _legacyCanceledActivationBehaviour; set => _legacyCanceledActivationBehaviour = value; }

        // CONSTRUCTOR
        public EventTarget()
        {
            _eventListenerList = new List<EventListener>();
        }

        // PUBLIC METHODS
        public void AddEventListener(string type, EventHandler? callback)
            => AddEventListener(new EventListener(type, callback));
        public void AddEventListener(string type, EventHandler? callback, AddEventListenerOptions options)
            => AddEventListener(new EventListener(type, callback, options.capture, options.passive, options.once, options.signal));
        public void AddEventListener(string type, EventHandler? callback, bool useCapture)
            => AddEventListener(new EventListener(type, callback, useCapture));
        public void AddEventListener(EventListener listener)
        {
            // 1
            if (this is ServiceWorkerGlobalScope)
            {
                ServiceWorkerGlobalScope scope = (ServiceWorkerGlobalScope) this;
                if (scope.ServiceWorker.ScriptResource.hasEverBeenEvaluated
                    && ServiceWorkerGlobalScope.EventTypes.Contains(listener.type))
                {
                    Console.Console.ReportWarning("Unexpected Results");
                }
            }

            // 2 + 3
            if ((listener.signal is not null && listener.signal.Aborted)
                || (listener.callback is null))
            { return; }

            // 4
            if (!EventListenerList.Contains(listener)) { EventListenerList.Add(listener); }

            // 5
            if (listener.signal is not null)
            {
                // 5.1
                listener.signal.Add(() => this.RemoveEventListener(listener));
            }
        }

        public void RemoveEventListener(string type, EventHandler? callback)
            => RemoveEventListener(new EventListener(type, callback, false));
        public void RemoveEventListener(string type, EventHandler? callback, bool useCapture)
            => RemoveEventListener(new EventListener(type, callback, useCapture));
        public void RemoveEventListener(EventListener listener)
        {
            throw new NotImplementedException();
        }

        public bool DispatchEvent(Event toDispatch)
        {
            throw new NotImplementedException();
        }

        // HELPER METHODS

    }

    public struct EventListener
    {
        public string type;
        public EventHandler? callback;
        public bool capture, passive, once;
        public AbortSignal? signal = null;
        public bool removed = false;

        public EventListener(string type, EventHandler? callback)
        {
            this.type = type;
            this.callback = callback;
            this.capture = false;
            this.passive = false;
            this.once = false;
        }

        public EventListener(string type, EventHandler? callback, bool capture, bool passive, bool once, AbortSignal? signal)
        {
            this.type = type;
            this.callback = callback;
            this.capture = capture;
            this.passive = passive;
            this.once = once;
            this.signal = signal;
        }

        public EventListener(string type, EventHandler? callback, bool capture)
        {
            this.type = type;
            this.callback = callback;
            this.capture = capture;
            this.passive = false;
            this.once = false;
        }
    }

    public struct AddEventListenerOptions
    {
        public bool passive;
        public bool once;
        public bool capture;
        public AbortSignal? signal;
    }
}