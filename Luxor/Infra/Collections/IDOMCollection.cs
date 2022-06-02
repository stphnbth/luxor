namespace Luxor.Infra.Collections
{
    public interface IDOMCollection<T>
    {
        // Size
        public int Size { get; }

        // Empty
        public void Empty();

        // Contains
        public bool Contains(T item);

        // IsEmpty
        public bool IsEmpty();
    }
}