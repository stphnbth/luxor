using System.Collections;

namespace Luxor.Infra.Collections
{
    public interface IOrderedMap<T> : IEnumerable<T>, IEnumerable
    {
        // Value (getter/setter)

        // Remove

        // Clear

        // Contains

        // Keys

        // Values

        // Size

        // IsEmpty

        // ForEach

        // Clone

        // SortAscending

        // SortDescending
    }

    public class Map<T> : IOrderedMap<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}