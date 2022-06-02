using Luxor.DOM.Events;

namespace Luxor.DOM.ServiceWorkers
{
    public class Client : IMessagePort
    {
        // PRIVATE FIELDS
        private string _url;
        private FrameType _frameType;
        private string _id;
        private ClientType type;

        // PUBLIC PROPERTIES
        public string Url { get => _url; set => _url = value; }
        public FrameType FrameType { get => _frameType; set => _frameType = value; }
        public string Id { get => _id; set => _id = value; }
        public ClientType Type { get => type; set => type = value; }

        // CONSTRUCTOR
        public Client() { _url = _id = ""; }

        // PUBLIC METHODS
        public void postMessage(string message, List<object> transfer)
        {
            throw new NotImplementedException();
        }
    }

    public enum FrameType
    {
        auxiliary,
        topLevel,
        nested,
        none
    }
}