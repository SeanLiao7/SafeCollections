using System.Collections.Generic;

namespace SafeCollections
{
    public static class SafeExtensions
    {
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, object locker)
            => new SafeEnumerable<T>(enumerable, locker);

        public static SafeCollection<T> ToSafeCollection<T>(this ICollection<T> collection, object locker = null)
            => new SafeCollection<T>(collection, locker);

        public static SafeList<T> ToSafeList<T>(this List<T> list, object locker = null)
            => new SafeList<T>(list, locker);

        public static SafeDictionary<TKey, TValue> ToSafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> table, object locker = null)
            => new SafeDictionary<TKey, TValue>(table, locker);
    }
}