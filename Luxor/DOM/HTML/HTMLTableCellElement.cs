namespace Luxor.DOM.HTML
{
    // https://html.spec.whatwg.org/multipage/tables.html#htmltablecellelement
    public class HTMLTableCellElement : HTMLElement
    {
        // PRIVATE FIELDS
        private long _colSpan;
        private long _rowSpan;
        private string _headers;
        private long _cellIndex;
        private string _scope;
        private string _abbr;

        // PUBLIC PROPERTIES
        public long ColSpan { get => _colSpan; set => _colSpan = value; }
        public long RowSpan { get => _rowSpan; set => _rowSpan = value; }
        public string Headers { get => _headers; set => _headers = value; }
        public long CellIndex { get => _cellIndex; }
        public string Scope { get => _scope; set => _scope = value; }
        public string Abbr { get => _abbr; set => _abbr = value; }

        // CONSTRUCTOR
        public HTMLTableCellElement(Document nodeDocument) : base(nodeDocument) {}

    }
}