namespace Luxor.DOM.ServiceWorkers
{
    public class WorkerNavigator : NavigatorID, NavigatorLanguage, NavigatorOnline, NavigatorConcurrentHardware
    {
        // PRIVATE FIELDS
        private string _platform;
        private string _userAgent;
        private string _vendor;
        private string _language;
        private List<string> _languages;
        private bool _online;
        private ulong _hardwareConcurrency;

        // PUBLIC PROPERTIES
        public string Platform { get => _platform; }
        public string UserAgent { get => _userAgent; }
        public string Vendor { get => _vendor; }
        public string Language { get => _language; }
        public List<string> Languages { get => _languages; }
        public bool Online { get => _online; }
        public ulong HardwareConcurrency { get => _hardwareConcurrency; }

        public WorkerNavigator()
        {
            _platform = _userAgent = _vendor = _language = "";
            _languages = new List<string>();
        }
    }

}