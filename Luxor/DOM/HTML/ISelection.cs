namespace Luxor.DOM.HTML
{
    public interface ISelection
    {
        // TODO: implementation for default properties??
        public ulong SelectionStart { get; set; }
        public ulong SelectionEnd { get; set; }
        public string SelectionDirection { get; set; }

        public void select()
        {
            throw new NotImplementedException();
        }

        public void setRangeText(string replacement)
        {
            throw new NotImplementedException();
        }

        public void setRangeText(string replacement, ulong start, ulong end, SelectionMode selectionMode = SelectionMode.Preserve)
        {
            throw new NotImplementedException();
        }

        public void setSelectionRange(ulong start, ulong end, string direction = "")
        {
            throw new NotImplementedException();
        }
    }

    public enum SelectionMode
    {
        Select,
        Start,
        End,
        Preserve
    }
}
