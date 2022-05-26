using Luxor.DOM.HTML;

namespace Luxor.DOM.XHR
{
    public class FormData
    {
        // CONSTRUCTOR
        public FormData(HTMLFormElement form) {}

        // PUBLIC METHODS
        public void append(string name, string value)
        {
            throw new NotImplementedException();
        }
        
        public void append(string name, Span<Int64> blobValue, string filename = "")
        {
            throw new NotImplementedException();
        }
        
        public void delete(string name)
        {
            throw new NotImplementedException();
        }

        public FormDataEntryValue? get(string name)
        {
            throw new NotImplementedException();
        }

        public List<FormDataEntryValue> getAll(string name)
        {
            throw new NotImplementedException();
        }

        public bool has(string name)
        {
            throw new NotImplementedException();
        }

        public void set(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void set(string name, Span<Int64> blobValue, string filename = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<string, FormDataEntryValue>> values()
        {
            throw new NotImplementedException();
        }

    }

    public struct SubmitEventInit
    {
        public HTMLElement? submitter = null;
        public SubmitEventInit() {}
    }

    // TODO: determine how to return multiple types
    public abstract class FormDataEntryValue : Object {}

}
