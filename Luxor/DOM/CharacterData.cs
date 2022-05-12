using System.Diagnostics;

namespace Luxor.DOM
{
    public class CharacterData : Node 
    {
        // PRIVATE FIELDS
        private string _data;
        private UInt32 _length;

        // PUBLIC PROPERTIES
        public string Data { get => _data; set => Replace(this, 0, Length, value); }
        public int Length { get; }

        // PUBLIC OVERRIDES
        public override string? NodeValue {
            get => Data;
            set
            {
                if (value is null) { value = ""; }
                Replace(this, 0, Length, value);
            }
        }

        public override string? TextContent { get => NodeValue; set => NodeValue = value; }

        // CONSTRUCTOR
        public CharacterData(string data, Document nodeDocument) : base(nodeDocument)
        {
            _data = data;
        }

        // PUBLIC METHODS
        public void Append(string data) => Replace(this, Length, 0, Data);
        public void Insert(int offset, string data) => Replace(this, offset, 0, data);
        public void Delete(int offset, int count) => Replace(this, offset, count, "");

        // https://dom.spec.whatwg.org/#concept-cd-replace
        public void Replace(Node node, int offset, int count, string data)
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

        public string Substring(int offset, int count) => Data.Substring(offset, count);
        
    }
}