namespace Luxor.DOM.HTML
{
    public interface IForm
    {
        // TODO: implement subinterfaces??
        // TODO: implementation for default properties??
        public bool Disabled { get; set; }
        public HTMLFormElement? Form { get; }
        public string FormAction { get; set; }
        public string FormEnctype { get; set; }
        public string FormMethod { get; set; }
        public bool FormNoValidate { get; set; }
        public string FormTarget { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}