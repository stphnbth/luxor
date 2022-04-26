namespace Luxor.DOM
{
    internal class HTMLTableElement : HTMLElement
    {
        protected HTMLTableCaptionElement? Caption { get; set; }
        protected HTMLTableSectionElement? THead { get; set; }
        protected HTMLTableSectionElement? TFoot { get; set; }
        protected List<Element> TBodies { get; }
        protected List<Element> Rows { get; }

        public HTMLTableElement() {}
        
        internal HTMLTableCaptionElement createCaption() { throw new NotImplementedException(); }
        internal void deleteCaption() { throw new NotImplementedException(); }
        internal HTMLTableSectionElement createTHead() { throw new NotImplementedException(); }
        internal void deleteTHead() { throw new NotImplementedException(); }
        internal HTMLTableSectionElement createTFoot() { throw new NotImplementedException(); }
        internal void deleteTFoot() { throw new NotImplementedException(); }
        internal HTMLTableSectionElement createTBody() { throw new NotImplementedException(); }
        internal HTMLTableRowElement insertRow(Int32? index = -1) { throw new NotImplementedException(); }
        internal void deleteRow(Int32 index) { throw new NotImplementedException(); }
    }
}