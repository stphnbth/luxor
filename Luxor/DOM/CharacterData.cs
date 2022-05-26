namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#characterdata
    public class CharacterData : Node, IChildNode, INonDocumentTypeChildNode
    {
        // PRIVATE FIELDS
        private string _data;
        private UInt32 _length;

        // PUBLIC PROPERTIES
        public string Data { get => _data; set => ReplaceData(this, 0, Length, value); }
        public int Length { get; }

        // PUBLIC OVERRIDES
        public override string? NodeValue {
            get => Data;
            set
            {
                if (value is null) { value = ""; }
                ReplaceData(this, 0, Length, value);
            }
        }

        public override string? TextContent { get => NodeValue; set => NodeValue = value; }

        // CONSTRUCTOR
        public CharacterData(string data, Document nodeDocument) : base(nodeDocument)
        {
            _data = data;
        }

        // PUBLIC METHODS
        public void AppendData(string data) => ReplaceData(this, Length, 0, Data);
        public void InsertData(int offset, string data) => ReplaceData(this, offset, 0, data);
        public void DeleteData(int offset, int count) => ReplaceData(this, offset, count, "");

        // https://dom.spec.whatwg.org/#concept-cd-replace
        public void ReplaceData(Node node, int offset, int count, string data)
        {
            // 1

            // 2

            // 3

            // 4

            // 5

            // 6

            // 6

            //

            // TODO: setup range errors
            if (offset > Length) {}

            if (offset + count > Length)
                count = Length - offset;

            // TODO: queue a mutation record

            Data.Insert(offset, data);
            Data.Remove(offset + data.Length, count);

            // TODO: FOREACH LIVE RANGES

            if (node.ParentNode is not null)
            {
                // TODO: CHILDREN CHANGED STEPS                
            }

        }

        public string SubstringData(int offset, int count) => Data.Substring(offset, count);
        
    }
}