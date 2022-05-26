using Luxor.DOM.HTML;

namespace Luxor.DOM
{
    public interface ISlottable
    {
        // PUBLIC PROPERTIES
        public HTMLSlotElement? AssignedSlot { get; }
    }
}