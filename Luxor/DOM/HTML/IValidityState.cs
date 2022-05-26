namespace Luxor.DOM.HTML
{
    public interface IValidityState
    {
        // TODO: implementation for default properties??
        public bool WillValidate { get; }
        public ValidityState Validity { get; }
        public string ValidationMessage { get; }

        public bool checkValidity()
        {
            throw new NotImplementedException();
        }

        public bool reportValidity()
        {
            throw new NotImplementedException();
        }

        public void setCustomValidity(string error)
        {
            throw new NotImplementedException();
        }
    }

    public struct ValidityState
    {
        public bool valueMissing;
        public bool typeMismatch;
        public bool patternMismatch;
        public bool tooLong;
        public bool tooShort;
        public bool rangeUnderflow;
        public bool rangeOverflow;
        public bool stepMismatch;
        public bool badInput;
        public bool customError;
        public bool valid;
    }
}