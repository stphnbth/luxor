namespace Luxor.DOM.ServiceWorkers
{
    public class Clients : IClients {}

    public interface IClients
    {
        public Task<Clients> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Clients>> MatchAll() => MatchAll(new ClientQueryOptions());
        public Task<List<Clients>> MatchAll(ClientQueryOptions options)
            => MatchAll(options.includeUncontrolled, options.type);
        public Task<List<Clients>> MatchAll(bool includeUncontrolled, ClientType type)
        {
            throw new NotImplementedException();
        }

        public Task<WindowClient?> OpenWindow(string url)
        {
            throw new NotImplementedException();
        }

        public Task Claim()
        {
            throw new NotImplementedException();
        }
    }

    public struct ClientQueryOptions
    {
        public bool includeUncontrolled = false;
        public ClientType type = ClientType.window;
        public ClientQueryOptions() {}
    }

    public enum ClientType
    {
        window,
        worker,
        sharedworker,
        all
    }
}
