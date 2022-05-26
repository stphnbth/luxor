namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#processinginstruction
    public class ProcessingInstruction : Text
    {        
        private string _target;

        public string Target { get => _target; }

        public override string NodeName { get => Target; }

        public ProcessingInstruction(Document nodeDocument) : base ("", nodeDocument) {}
    }
}


