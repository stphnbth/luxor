using System.Collections;

namespace Luxor.DOM
{
    // https://dom.spec.whatwg.org/#domtokenlist
    public class DOMTokenList : IList
    {
        // PRIVATE FIELDS
        private string[] _tokens;
        private ulong _length;

        // PUBLIC PROPERTIES
        public ulong Length { get => _length; }

        // IList Implementation
        public object? this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFixedSize => throw new NotImplementedException();
        public bool IsReadOnly => throw new NotImplementedException();
        public int Count => throw new NotImplementedException();
        public bool IsSynchronized => throw new NotImplementedException();
        public object SyncRoot => throw new NotImplementedException();

        public int Add(object? value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object? value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object? value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object? value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object? value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        // PUBLIC METHODS
        public string? Item(ulong index)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string token)
        {
            throw new NotImplementedException();
        }
        
        public void Add(string[] tokens)
        {
            throw new NotImplementedException();
        }
        
        public void Remove(string[] tokens)
        {
            throw new NotImplementedException();
        }
        
        public bool Toggle(string token, bool force = false)
        {
            throw new NotImplementedException();
        }
        
        public bool Replace(string token, string newToken)
        {
            throw new NotImplementedException();
        }
        
        public bool Supports(string token)
        {
            throw new NotImplementedException();
        }
    }
}