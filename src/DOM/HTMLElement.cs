namespace Luxor.DOM
{
    public class HTMLElement : Element 
    {
        private string title;
        private string lang;
        private string dir;
        private string accessKey;
        private string accessKeyLabel;
        private string autocapitalize;
        private string innerText;
        private string outerText;
        
        private bool translate;
        private bool hidden;
        private bool inert;
        private bool draggable;
        private bool spellcheck;

        public ElementInternals attachInternals()
        {
            throw new NotImplementedException();
        }
    }
}
