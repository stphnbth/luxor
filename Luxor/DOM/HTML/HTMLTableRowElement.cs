namespace Luxor.DOM.HTML
{
    public class HTMLTableRowElement : HTMLElement
    {
        // PRIVATE FIELDS
        private long _rowIndex;
        private long _sectionRowIndex;
        private List<Element> _cells;

        // PUBLIC PROPERTIES
        public long RowIndex { get => _rowIndex; }
        public long SectionRowIndex { get => _sectionRowIndex; }
        public List<Element> Cells { get => _cells; }

        // CONSTRUCTOR
        public HTMLTableRowElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public HTMLTableCellElement insertCell(long? index = -1)
        {
            throw new NotImplementedException();
        }

        public void deleteCell(long index) 
        {
            throw new NotImplementedException();
        }
        
    }
}