using System.Collections.Generic;

namespace SafeCollections
{
    public static class SafeExtensions
    {
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, object locker)
            => new SafeEnumerable<T>(enumerable, locker);

        public static SafeList<T> ToSafeList<T>(this List<T> list, object locker = null)
            => new SafeList<T>(list, locker);
    }
}