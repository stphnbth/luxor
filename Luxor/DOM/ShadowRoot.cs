using System.Diagnostics;

namespace Luxor.DOM
{
    public class ShadowRoot : DocumentFragment
    {
        private bool _delegatesFocus = false;
        private bool _availableToInternals = false;
        
        public ShadowRootMode Mode { get; }
        public SlotAssignmentMode SlotAssignment { get; }
        
        public bool DelegatesFocus { get => _delegatesFocus; internal set => _delegatesFocus = value; }
        public bool AvailableToInternals { get => _availableToInternals; internal set => _availableToInternals = value; }

        public ShadowRoot(Document? ownerDocument, Element host, ShadowRootMode mode, SlotAssignmentMode slotAssignment) : base(ownerDocument, host)
        {
            Mode = mode;
            SlotAssignment = slotAssignment;
        }
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