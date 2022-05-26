using System.Diagnostics;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#shadowroot
    public class ShadowRoot : DocumentFragment, IDocumentOrShadowRoot
    {
        // PRIVATE FIELDS
        private ShadowRootMode _mode;
        private bool _delegatesFocus;
        private SlotAssignmentMode _slotAssignment;
        private Element _host;

        private EventHandler onslotchange;

        private bool _availableToInternals;

        // PUBLIC PROPERTIES
        public ShadowRootMode Mode { get => _mode; }
        public bool DelegatesFocus { get => _delegatesFocus; }
        public SlotAssignmentMode SlotAssignment { get => _slotAssignment; }
        public Element Host { get => _host; }

        // INTERNAL PROPERTIES
        internal bool AvailableToInternals { get => _availableToInternals; set => _availableToInternals = value; }

        // CONSTRUCTOR
        private ShadowRoot(Document ownerDocument) : base(ownerDocument) {}
    }

    public enum ShadowRootMode
    {
        Open, 
        Closed
    }

    public enum SlotAssignmentMode
    {
        Manual,
        Named
    }

}