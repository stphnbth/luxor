using Luxor.DOM.Events;
using EventHandler = Luxor.DOM.Events.EventHandler;

namespace Luxor.DOM.ServiceWorkers
{
    public class ServiceWorkerRegistration : EventTarget
    {
        private ServiceWorker? _installing;
        private ServiceWorker? _waiting;
        private ServiceWorker? _active;
        private NaviagationPreloadManager _navigationPreload = new NaviagationPreloadManager();
        private string _scope = "";
        private ServiceWorkerUpdateViaCache _updateCache;

        // EVENT HANDLERS
        public EventHandler? onupdatefound;

        // PUBLIC METHODS
        public Task Update()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Unregister()
        {
            throw new NotImplementedException();
        }
    }

    public enum ServiceWorkerUpdateViaCache
    {
        imports,
        all,
        none
    }
}