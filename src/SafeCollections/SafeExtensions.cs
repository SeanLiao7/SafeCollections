using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public static class SafeExtensions
    {
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, ReaderWriterLockSlim locker)
            => new SafeEnumerable<T>(enumerable, locker);

        public static SafeCollection<T> ToSafeCollection<T>(this ICollection<T> collection, ReaderWriterLockSlim locker = null)
            => new SafeCollection<T>(collection, locker);

        public static SafeList<T> ToSafeList<T>(this List<T> list, ReaderWriterLockSlim locker = null)
            => new SafeList<T>(list, locker);

        public static SafeDictionary<TKey, TValue> ToSafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> table, ReaderWriterLockSlim locker = null)
            => new SafeDictionary<TKey, TValue>(table, locker);
    }
}