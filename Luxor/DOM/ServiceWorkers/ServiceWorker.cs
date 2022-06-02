using Luxor.DOM.Events;
using Luxor.DOM.HTML.Scripting;
using EventHandler = Luxor.DOM.Events.EventHandler;

namespace Luxor.DOM.ServiceWorkers
{
    public class ServiceWorker : EventTarget, IAbstractWorker, IMessagePort
    {
        // PRIVATE FIELDS
        private string _scriptURL = "";
        private ServiceWorkerState _state;

        private Script _scriptResource;

        internal Script ScriptResource { get => _scriptResource; set => _scriptResource = value; }

        public EventHandler? onstatechange;
        public EventHandler? onerror => null;


        public void postMessage(string message, List<object> transfer)
        {
            throw new NotImplementedException();
        }
    }

    public interface IAbstractWorker
    {
        public EventHandler? onerror { get; }
    }

    public enum ServiceWorkerState
    {
        parsed,
        installing,
        installed,
        activating,
        activated,
        redundant
    }
}