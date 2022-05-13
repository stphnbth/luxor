namespace Luxor.DOM.HTML
{
    public class HTMLTableSectionElement : HTMLElement
    {
        // PRIVATE FIELDS
        private List<Element> _rows;

        // PUBLIC PROPERTIES
        public List<Element> Rows { get => _rows; }

        // CONSTRUCTOR
        public HTMLTableSectionElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        internal HTMLTableRowElement insertRow(Int32? index = -1) { throw new NotImplementedException(); }
        internal void deleteRow(Int32 index) { throw new NotImplementedException(); }
    }
}