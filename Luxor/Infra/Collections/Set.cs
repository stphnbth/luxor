using System.Collections;

namespace Luxor.Infra.Collections
{
    public interface IOrderedSet<T> : IEnumerable<T>, IEnumerable
    {
        // Append

        // Prepend

        // Replace

        // Subset

        // Superset

        // Intersection

        // Union

        // Range(Inclusive + Exclusive)
    }

    public class Set<T> : IOrderedSet<T>
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