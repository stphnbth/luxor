using System.Diagnostics;

namespace Luxor.DOM
{
    public class ShadowRoot : DocumentFragment
    {
        private bool _delgatesFocus = false;
        
        public ShadowRootMode Mode { get; }
        public SlotAssignmentMode SlotAssignment { get; }

        public ShadowRoot()
        {
            Mode = ShadowRootMode.Open;
            SlotAssignment = SlotAssignmentMode.Manual;
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