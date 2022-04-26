namespace Luxor.DOM
{
    internal class HTMLTableSectionElement : HTMLElement
    {
        protected List<Element> Rows { get; }

        public HTMLTableSectionElement() {}
        
        internal HTMLTableRowElement insertRow(Int32? index = -1) { throw new NotImplementedException(); }
        internal void deleteRow(Int32 index) { throw new NotImplementedException(); }
    }
}