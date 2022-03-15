using System.Diagnostics;

namespace Luxor.DOM
{
    class DOMException : Exception
    {
        private static readonly Dictionary<DOMError, string> messages = new Dictionary<DOMError, string>{
            { DOMError.HierarchyRequest, "The operation would yield an incorrect node tree." },
            { DOMError.WrongDocument, "The object is in the wrong document." },
            { DOMError.InvalidCharacter, "The string contains invalid characters." },
            { DOMError.NoModificationAllowed, "The object can not be modified." },
            { DOMError.NotFound, "The object can not be found here." },
            { DOMError.NotSupported, "The operation is not supported." },
            { DOMError.InUseAttribute, "The attribute is in use." },
            { DOMError.InvalidState, "The object is in an invalid state." },
            { DOMError.Syntax, "The string did not match the expected pattern." },
            { DOMError.InvalidModification, "The object can not be modified in this way." },
            { DOMError.Namespace, "The operation is not allowed by Namespaces in XML." },
            { DOMError.Security, "The operation is insecure." },
            { DOMError.Network, "A network error occurred." },
            { DOMError.Abort, "The operation was aborted." },
            { DOMError.URLMismatch, "The given URL does not match another URL." },
            { DOMError.QuotaExceeded, "The quota has been exceeded." },
            { DOMError.Timeout, "The operation timed out." },
            { DOMError.InvalidNodeType, "The supplied node is incorrect or has an incorrect ancestor for this operation." },
            { DOMError.DataClone, "The object can not be cloned." },
            { DOMError.Encoding, "The encoding operation (either encoded or decoding) failed." },
            { DOMError.NotReadable, "The I/O read operation failed." },
            { DOMError.Unknown, "The operation failed for an unknown transient reason (e.g. out of memory)." },
            { DOMError.Constraint, "A mutation operation in a transaction failed because a constraint was not satisfied." },
            { DOMError.Data, "Provided data is inadequate." },
            { DOMError.TransactionInactive, "A request was placed against a transaction which is currently not active, or which is finished." },
            { DOMError.ReadOnly, "The mutating operation was attempted in a 'readonly' transaction." },
            { DOMError.Version, "An attempt was made to open a database using a lower version than the existing version." },
            { DOMError.Operation, "The operation failed for an operation-specific reason." },
            { DOMError.NotAllowed, "The request is not allowed by the user agent or the platform in the current context, possibly because the user denied permission." }
        };
        
        public DOMException() : base() {}
        public DOMException(DOMError error) : base(messages[error]) {}
        public DOMException(DOMError error, Exception inner) : base(messages[error], inner) {}
    }

    enum DOMError
    {
        HierarchyRequest,
        WrongDocument,
        InvalidCharacter,
        NoModificationAllowed,
        NotFound,
        NotSupported,
        InUseAttribute,
        InvalidState,
        Syntax,
        InvalidModification,
        Namespace,
        Security,
        Network,
        Abort,
        URLMismatch,
        QuotaExceeded,
        Timeout,
        InvalidNodeType,
        DataClone,
        Encoding,
        NotReadable,
        Unknown,
        Constraint,
        Data,
        TransactionInactive,
        ReadOnly,
        Version,
        Operation,
        NotAllowed
    }
} 