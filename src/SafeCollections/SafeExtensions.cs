using System.Collections.Generic;

namespace SafeCollections
{
    public static class SafeExtensions
    {
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, ReaderWriterLockTiny locker)
            => new SafeEnumerable<T>(enumerable, locker);

        public static SafeCollection<T> ToSafeCollection<T>(this ICollection<T> collection, ReaderWriterLockTiny locker = null)
            => new SafeCollection<T>(collection, locker);

        public static SafeDictionary<TKey, TValue> ToSafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> table, ReaderWriterLockTiny locker = null)
            => new SafeDictionary<TKey, TValue>(table, locker);

        public static SafeList<T> ToSafeList<T>(this List<T> list, ReaderWriterLockTiny locker = null)
            => new SafeList<T>(list, locker);

        public static SafeHashSet<T> ToSafeHashSet<T>(this ISet<T> set, ReaderWriterLockTiny locker = null)
            => new SafeHashSet<T>(set, locker);
    }
}