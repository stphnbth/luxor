using Luxor.DOM.XHR;

namespace Luxor.DOM.Events
{
    public class FormDataEvent : Event
    {
        // PRIVATE FIELDS
        private FormData _formData;

        // CONSTRUCTOR
        public FormDataEvent(string type, FormDataEventInit? eventInitDict = null) : base(type) {}
    }

    public struct FormDataEventInit
    {
        public FormData formData;
    }
}
