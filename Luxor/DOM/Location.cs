namespace Luxor.DOM
{
    public class Location 
    {
        private string href;
        private string protocol;
        private string host;
        private string hostname;
        private string port;
        private string pathname;
        private string search;
        private string hash;

        readonly List<string> ancestorOrigins;
        readonly string origin;

        Node AdoptNode(Node node)
        {
            throw new NotImplementedException();
        }   

        void assign(string url)
        {
            throw new NotImplementedException();
        }

        void replace(string url)
        {
            throw new NotImplementedException();
        }
        
        void reload()
        {
            throw new NotImplementedException();
        }

    }
}
