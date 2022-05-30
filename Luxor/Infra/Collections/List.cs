using System.Collections;

namespace Luxor.Infra.Collections
{
    public interface IList<T> : ICollection<T>, ICollection, IEnumerable<T>, IEnumerable
    {
        // Append - https://infra.spec.whatwg.org/#list-append
        void Append(T item) { Add(item); }

        // Extend - https://infra.spec.whatwg.org/#list-extend
        void Extend(List<T> list);

        // Prepend - https://infra.spec.whatwg.org/#list-prepend
        void Prepend(T item) { Insert(0, item); }

        // Replace - https://infra.spec.whatwg.org/#list-replace
        void Replace(Func<bool> condition, T item);

        // Insert - https://infra.spec.whatwg.org/#list-insert
        void Insert(int index, T item);

        // Remove - https://infra.spec.whatwg.org/#list-remove
        void Remove(Func<bool> condition);

        // Empty -  https://infra.spec.whatwg.org/#list-empty
        void Empty();

        // Contains - https://infra.spec.whatwg.org/#list-contain
        // ICollection<T>

        // Size - https://infra.spec.whatwg.org/#list-size
        int Size { get; }

        // IsEmpty - https://infra.spec.whatwg.org/#list-is-empty
        bool IsEmpty()
        {
            if (Size == 0)
                return true;

            return false;
        }

        // GetIndices - https://infra.spec.whatwg.org/#list-get-the-indices
        Set<int> GetIndices();

        // ForEach - https://infra.spec.whatwg.org/#list-iterate
        // IEnumerate<T>

        // Clone - https://infra.spec.whatwg.org/#list-clone
        List<T>  Clone();

        // SortAscending - https://infra.spec.whatwg.org/#list-sort-in-ascending-order
        void SortAscending();

        // SortDescending - https://infra.spec.whatwg.org/#list-sort-in-descending-order
        void SortDescending();
    }

    public class List<T> : IList<T>
    {
        // PRIVATE FIELDS
        private T[] _items;
        private int _size;
        private int _capacity;

        // PUBLIC PROPERTIES
        public int Size => Count;
        public int Count => _size;
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        // CONSTRUCTORS
        List() {}

        // PUBLIC METHODS
        public void Add(T item)
        {
            if (_size == _items.Length)
            {
                int newCapacity = (_size == 0) ? 4 : 2 * _size;

                if ((uint)newCapacity > Array.MaxLength) { newCapacity = Array.MaxLength; }

                _capacity = newCapacity;
            }

            _size++;
            _items[_size] = item;
        }

        public void Clear()
        {
            if (_size > 0) { Array.Clear(_items, 0, _size); }
            _size = 0;
        }

        public List<T> Clone()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item) => _size != 0 && IndexOf(item) >= 0;

        public void CopyTo(T[] array, int arrayIndex) => Array.Copy(_items, 0, array, arrayIndex, _size);

        public void CopyTo(Array array, int index)
        {
            if ((array != null) && (array.Rank != 1)) { throw; }

            try
            {
                Array.Copy(_items, 0, array!, index, _size);
            }
            catch (ArrayTypeMismatchException)
            {
                throw;
            }
        }

        public void Empty() => Clear();

        public void Extend(List<T> list)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
            => _items.GetEnumerator();

        public Set<int> GetIndices()
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            if ((uint)index > (uint)_size) { throw; }

            if (_size == _items.Length) {}

            if (index > _size) { Array.Copy(_items, index, _items, index + 1, _size - index); }

            _items[index] = item;
            _size++;
        }

        public void Remove(Func<bool> condition)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Replace(Func<bool> condition, T item)
        {
            throw new NotImplementedException();
        }

        public void SortAscending()
        {
            throw new NotImplementedException();
        }

        public void SortDescending()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}