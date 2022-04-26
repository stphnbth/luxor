namespace Luxor.DOM
{
    public class HTMLOptionElement : HTMLElement
    {
        protected bool Disabled { get; set; }
        protected HTMLFormElement? Form { get; }
        protected string Label { get; set; }
        protected bool DefaultSelected { get; set; }
        protected bool Selected { get; set; }
        protected string Value { get; set; }
        protected string Text { get; set; }
        protected Int32 Index { get; }

        public HTMLOptionElement() {}

        public void Option(string? value, string? text = "", bool? defaultSelected = false, bool? selected = false)
        {
            Text = text;
            Value = value;
            DefaultSelected = defaultSelected.HasValue ? defaultSelected.Value : true;
            Selected = selected.HasValue ? selected.Value : true;
        }
    }
}