namespace Luxor.DOM
{
    internal class HTMLTableCellElement : HTMLElement
    {
        protected Int32 ColSpan { get; set; }
        protected Int32 RowSpan { get; set; }
        protected string Headers { get; set; }
        protected Int32 CellIndex { get; }
        protected string Scope { get; set; }
        protected string Abbr { get; set; }

        public HTMLTableCellElement() {}   
    }
}