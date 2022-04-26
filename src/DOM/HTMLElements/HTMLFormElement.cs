namespace Luxor.DOM
{
    public class HTMLFormElement : HTMLElement
    {
        protected string AcceptCharset { get; set; }
        protected string Action { get; set; }
        protected string Autocomplete { get; set; }
        protected string Enctype { get; set; }
        protected string Encoding { get; set; }
        protected string Method { get; set; }
        protected string Name { get; set; }
        protected bool NoValidate { get; set; }
        protected string Target { get; set; }
        protected string Rel { get; set; }
        protected List<Token> RelList { get; }
        protected List<Element> Elements { get; }
        protected UInt32 Length { get; }
        
        public HTMLFormElement() {}

        public void submit() { throw new NotImplementedException(); }
        public void requestSubmit(HTMLElement? submitter = null) { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }
        public bool checkValidity() { throw new NotImplementedException(); }
        public bool reportValidity() { throw new NotImplementedException(); }
    }
}