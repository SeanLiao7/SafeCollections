using System.Collections;
using System.Collections.Generic;

namespace SafeCollections
{
    public sealed class SafeHashSet<T> : ISet<T>
    {
        private readonly ISet<T> _hashSet;
        private readonly object _locker;
        public int Count => _hashSet.Count;

        public bool IsReadOnly => false;

        public SafeHashSet(ISet<T> hashSet = null, object locker = null)
        {
            _hashSet = hashSet ?? new HashSet<T>();
            _locker = locker ?? new object();
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Add(T item)
        {
            lock (_locker)
                return _hashSet.Add(item);
        }

        public void Clear()
        {
            lock (_locker)
                _hashSet.Clear();
        }

        public bool Contains(T item)
        {
            lock (_locker)
                return _hashSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_locker)
                _hashSet.CopyTo(array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            lock (_locker)
                _hashSet.ExceptWith(other);
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_hashSet, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void IntersectWith(IEnumerable<T> other)
        {
            lock (_locker)
                _hashSet.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            lock (_locker)
                return _hashSet.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            lock (_locker)
                return _hashSet.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            lock (_locker)
                return _hashSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            lock (_locker)
                return _hashSet.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            lock (_locker)
                return _hashSet.Overlaps(other);
        }

        public bool Remove(T item)
        {
            lock (_locker)
                return _hashSet.Remove(item);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            lock (_locker)
                return _hashSet.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            lock (_locker)
                _hashSet.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            lock (_locker)
                _hashSet.UnionWith(other);
        }
    }
}