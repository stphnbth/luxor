namespace Luxor.Infra.Collections
{
    public interface IDOMQueue<T> : IDOMCollection<T>
    {
        // Enqueue
        public void Enqueue(T item);

        // Dequeue
        public T Dequeue();
    }

    public class DOMQueue<T> : IDOMQueue<T>
    {
        // PRIVATE FIELDS
        private T[] _items;
        private int _head;
        private int _tail;
        private int _size;
        private int _capacity;

        // PUBLIC PROPERTIES
        public int Size { get => _size; }

        // CONSTRUCTORS

        // PUBLIC METHODS
        public bool Contains(T item)
        {
            if (_size == 0) { return false; }

            if (_head < _tail) { return Array.IndexOf(_items, item, _head, _size) >= 0; }

            return
                Array.IndexOf(_items, item, _head, _items.Length - _head) >= 0 ||
                Array.IndexOf(_items, item, 0, _tail) >= 0;
        }

        public T Dequeue()
        {
            if (_size == 0) {}

            T toRemove = _items[_head];
            MoveNext(ref _head);

            _size--;
            _capacity++;

            return toRemove;
        }

        public void Empty()
        {
            Array.Clear(_items, _head, _size);
            _size = 0;
            _head = 0;
            _tail = 0;
        }

        public void Enqueue(T item)
        {
            if _size == _items.Length { Grow() }
        }

        public void IsEmpty()
        {
            throw new NotImplementedException();
        }
    }
}