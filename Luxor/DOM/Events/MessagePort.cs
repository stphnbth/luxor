namespace Luxor.DOM.Events
{
    public class MessagePort : EventTarget, IMessagePort
    {
        public EventHandler? onmessage;
        public EventHandler? onmessageerror;

        public void start()
        {
            throw new NotImplementedException();
        }

        public void close()
        {
            throw new NotImplementedException();
        }

        public void postMessage(string message, List<object> transfer)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMessagePort
    {
        public void postMessage(string message, List<Object> transfer);
        public void postMessage(string message, StructuredSerializeOptions options)
            => postMessage(message, options.transfer);
    }

    public struct StructuredSerializeOptions
    {
        public List<Object> transfer = new List<object>();
        public StructuredSerializeOptions() {}
    }
}