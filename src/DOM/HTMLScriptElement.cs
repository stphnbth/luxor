namespace Luxor.DOM
{
    public class HTMLScriptElement : HTMLElement
    {
        private string src;
        private string type;
        private bool noModule;
        private bool async;
        private bool defer;
        private string? crossOrigin;
        private string text;
        private string integrity;
        private string referrerPolicy;
        private readonly List<Token> blocking;

        static bool supports(string type)
        {
            throw new NotImplementedException();
        }
    }
}
