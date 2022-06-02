namespace Luxor.DOM
{
    public class Navigator : NavigatorID, NavigatorLanguage, NavigatorOnline, NavigatorContentUtils, NavigatorCookies, NavigatorPlugins, NavigatorConcurrentHardware
    {
        // PRIVATE FIELDS
        private string _platform;
        private string _userAgent;
        private string _vendor;
        private string _language;
        private List<string> _languages;
        private bool _online;
        private bool _cookieEnabled;
        private ulong _hardwareConcurrency;

        // PUBLIC PROPERTIES
        public string Platform { get => _platform; }
        public string UserAgent { get => _userAgent; }
        public string Vendor { get => _vendor; }
        public string Language { get => _language; }
        public List<string> Languages { get => _languages; }
        public bool Online { get => _online; }
        public bool CookieEnabled { get => _cookieEnabled; }
        public ulong HardwareConcurrency { get => _hardwareConcurrency; }

        // CONSTRUCTOR

        // PUBLIC METHOD
        public void registerProtocolHandler(string scheme, string url)
        {
            throw new NotImplementedException();
        }
    }

    public interface NavigatorID
    {
        public string Platform { get; }
        public string UserAgent { get; }
        public string Vendor { get; }
    }

    public interface NavigatorLanguage
    {
        public string Language { get; }
        public List<string> Languages { get; }
    }
    
    public interface NavigatorOnline
    {
        public bool Online { get; }
    }

    public interface NavigatorContentUtils
    {
           public void registerProtocolHandler(string scheme, string url);
    }
    
    public interface NavigatorCookies 
    {
        public bool CookieEnabled { get; }
    }
    
    public interface NavigatorPlugins 
    {

    }

    public interface NavigatorConcurrentHardware
    {
        public ulong HardwareConcurrency { get; }
    }
    
}