using System.Collections.Generic;

namespace Workshop.Core.Helpers
{
    public static class HashsetExtensions
    {
        public static HashSet<T> ToHashSet<T>(
            this IEnumerable<T> source,
            IEqualityComparer<T> comparer = null)
        {
            return comparer is null ? new HashSet<T>(source) : new HashSet<T>(source, comparer);
        }
    }
}