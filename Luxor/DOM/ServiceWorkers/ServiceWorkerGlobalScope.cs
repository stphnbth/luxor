namespace Luxor.DOM.ServiceWorkers
{
    public class ServiceWorkerGlobalScope : WorkerGlobalScope
    {
        // STATICS
        private static readonly List<string> _eventTypes = new List<string>()
        {
            "install",
            "activate",
            "fetch",
            "message",
            "messageerror"
        };

        public static List<string> EventTypes => _eventTypes;

        // PRIVATE FIELDS
        private Clients _clients;
        private ServiceWorkerRegistration _registration;
        private ServiceWorker _serviceWorker;

        // PUBLIC PROPERTIES
        public Clients Clients { get => _clients; }
        public ServiceWorkerRegistration Registration { get => _registration; }
        public ServiceWorker ServiceWorker { get => _serviceWorker; }

        // EVENT HANDLERS
        public EventHandler? oninstall { get; }
        public EventHandler? onactivate { get; }
        public EventHandler? onfetch { get; }
        public EventHandler? onmessage { get; }
        public EventHandler? onmessageerror { get; }

        // CONSTRUCTOR
        protected ServiceWorkerGlobalScope()
        {
            _clients = new Clients();
            _registration = new ServiceWorkerRegistration();
            _serviceWorker = new ServiceWorker();
        }

        // PUBLIC METHODS
        public Task SkipWaiting()
        {
            throw new NotImplementedException();
        }
    }
}