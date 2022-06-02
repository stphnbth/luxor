using Luxor.DOM.Events;
using EventHandler = Luxor.DOM.Events.EventHandler;

namespace Luxor.DOM.ServiceWorkers
{
    public class WorkerGlobalScope : EventTarget
    {
        // PRIVATE FIELDS
        private WorkerGlobalScope _self;
        private WorkerLocation _location;
        private WorkerNavigator _navigator;

        // PUBLIC PROPERTIES
        public WorkerGlobalScope Self { get => _self; }
        public WorkerLocation Location { get => _location; }
        public WorkerNavigator Navigator { get => _navigator; }

        // EVENT HANDLERS
        public OnErrorEventHandler? onerror { get; }
        public EventHandler? onlanguagechange { get; }
        public EventHandler? onoffline { get; }
        public EventHandler? ononline { get; }
        public EventHandler? onrejectionhandled { get; }
        public EventHandler? onunhandledrejection { get; }

        // CONSTRUCTOR
        protected WorkerGlobalScope() : base()
        {
            _self = this;
            _location = new WorkerLocation();
            _navigator = new WorkerNavigator();
        }

        // PUBLIC METHODS
        public void ImportScripts(List<string> urls)
        {
            throw new NotImplementedException();
        }

    }
}