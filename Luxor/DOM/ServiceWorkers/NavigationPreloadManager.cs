namespace Luxor.DOM.ServiceWorkers
{
    public class NaviagationPreloadManager : INavigationPreloadManager
    {

    }

    public interface INavigationPreloadManager
    {
        public Task Enable()
        {
            throw new NotImplementedException();
        }

        public Task Disable()
        {
            throw new NotImplementedException();
        }
        
        public Task SetHeaderValue(byte[] value)
        {
            throw new NotImplementedException();
        }
        
        public Task<NavigationPreloadState> GetState()
        {
            throw new NotImplementedException();
        }
        
    }

    public struct NavigationPreloadState
    {
        bool enabled;
        byte[] headerValue;
    }
}