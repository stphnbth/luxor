namespace Luxor.DOM
{
    internal class HTMLTableRowElement : HTMLElement
    {
        protected long RowIndex { get; }
        protected long SectionRowIndex { get; }
        protected List<Element> Cells { get; }
        
        public HTMLTableRowElement() {}
    
        internal HTMLTableCellElement insertCell(Int32? index = -1) { throw new NotImplementedException(); }
        internal void deleteCell(Int32 index) { throw new NotImplementedException(); }
        
    }
}