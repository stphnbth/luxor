namespace Luxor.DOM
{
    public class History
    {
        // PRIVATE FIELDS
        private ulong _length;
        private ScrollRestoration _scrollRestoration;
        private string? _state;

        // PUBLIC PROPERTIES
        public ulong Length { get => _length; }
        public ScrollRestoration ScrollRestoration { get => _scrollRestoration; set => _scrollRestoration = value; }
        public string? State { get => _state; set => _state = value; }

        // PUBLIC METHODS
        public void go(long delta = 0)
        {
            throw new NotImplementedException();
        }

        public void back()
        {
            throw new NotImplementedException();
        }
        
        public void forward()
        {
            throw new NotImplementedException();
        }
        
        public void pushState(string data, string unused, string? url = null)
        {
            throw new NotImplementedException();
        }
        
        public void replaceState(string data, string unused, string? url = null)
        {
            throw new NotImplementedException();
        }
    }

    public enum ScrollRestoration
    {
        auto,
        manual
    }
}