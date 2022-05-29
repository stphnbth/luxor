using System.Collections;

namespace Luxor.Infra.Collections
{
    public interface IBaseList<T> : IEnumerable<T>, IEnumerable
    {
        // Append

        // Extend

        // Prepend

        // Replace

        // Insert

        // Remove

        // Empty

        // Contains

        // Size

        // IsEmpty

        // GetIndices

        // ForEach

        // Clone

        // SortAscending

        // SortDescending
    }

    public class List<T> : IBaseList<T>
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