using Luxor.DOM.HTML;

namespace Luxor.DOM.Events
{
    public class SubmitEvent : Event
    {
        // PRIVATE FIELDS
        private HTMLElement? _submitter;

        // CONSTRUCTOR
        public SubmitEvent(string type, SubmitEventInit? eventInitDict = null) : base(type) {}
    }

    public struct SubmitEventInit
    {
        public HTMLElement? submitter = null;
        public SubmitEventInit() {}
    }
}
