namespace Luxor.DOM.ServiceWorkers
{
    public class WorkerLocation
    {
        // PRIVATE FIELDS
        private string _href;
        private string _origin;
        private string _protocol;
        private string _host;
        private string _hostname;
        private string _port;
        private string _pathname;
        private string _search;
        private string _hash;

        // PUBLIC PROPERTIES
        public string Href { get => _href; }
        public string Origin { get => _origin; }
        public string Protocol { get => _protocol; }
        public string Host { get => _host; }
        public string Hostname { get => _hostname; }
        public string Port { get => _port; }
        public string Pathname { get => _pathname; }
        public string Search { get => _search; }
        public string Hash { get => _hash; }

        // CONSTRUCTOR
        public WorkerLocation()
        {
            _href = _origin = _protocol = _host = _hostname = _port = _pathname = _search = _hash = "";
        }
    }
}