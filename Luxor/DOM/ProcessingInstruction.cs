using System.Diagnostics;

namespace Luxor.DOM
{
    public class ProcessingInstruction : Text
    {        
        private string _target;

        public string Target { get => _target; }

        public override string NodeName { get => Target; }

        public ProcessingInstruction() : base () {}

    }
}


