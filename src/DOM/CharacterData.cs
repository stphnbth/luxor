using System.Diagnostics;

namespace Luxor.DOM
{
    public interface CharacterData : Node 
    {
        public string Data { get; set; }
        public int Length { get; }

        public void Append(string data) => Replace(Length, 0, Data);
        public void Insert(int offset, string data) => Replace(offset, 0, data);
        public void Delete(int offset, int count) => Replace(offset, count, "");

        public void  Replace(int offset, int count, string data)
        {
            // TODO: setup range errors
            if (offset > Length) {}

            if (offset + count > Length)
                count = Length - offset;

            // TODO: queue a mutation record

            Data.Insert(offset, data);
            Data.Remove(offset + data.Length, count);

            // TODO: FOREACH LIVE RANGES

            if (ParentNode is not null)
            {
                // TODO: CHILDREN CHANGED STEPS                
            }

        }

        public string Substring(int offset, int count) => Data.Substring(offset, count);
        
    }
}