namespace Luxor.DOM.HTML
{
    public class HTMLTableElement : HTMLElement
    {
        // PRIVATE FIELDS
        private HTMLTableCaptionElement? _caption;
        private HTMLTableSectionElement? _tHead;
        private HTMLTableSectionElement? _tFoot;
        private List<Element> _tBodies;
        private List<Element> _rows;

        // PUBLIC PROPERTIES
        public HTMLTableCaptionElement? Caption { get => _caption; set => _caption = value; }
        public HTMLTableSectionElement? THead { get => _tHead; set => _tHead = value; }
        public HTMLTableSectionElement? TFoot { get => _tFoot; set => _tFoot = value; }
        public List<Element> TBodies { get => _tBodies; }
        public List<Element> Rows { get => _rows; }


        // CONSTRUCTOR
        public HTMLTableElement(Document nodeDocument) : base(nodeDocument) {}

        // PUBLIC METHODS
        public HTMLTableCaptionElement createCaption() { throw new NotImplementedException(); }
        public void deleteCaption() { throw new NotImplementedException(); }
        public HTMLTableSectionElement createTHead() { throw new NotImplementedException(); }
        public void deleteTHead() { throw new NotImplementedException(); }
        public HTMLTableSectionElement createTFoot() { throw new NotImplementedException(); }
        public void deleteTFoot() { throw new NotImplementedException(); }
        public HTMLTableSectionElement createTBody() { throw new NotImplementedException(); }
        public HTMLTableRowElement insertRow(Int32? index = -1) { throw new NotImplementedException(); }
        public void deleteRow(Int32 index) { throw new NotImplementedException(); }
    }
}