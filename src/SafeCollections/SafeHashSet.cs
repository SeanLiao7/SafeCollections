using System.Collections;
using System.Collections.Generic;

namespace SafeCollections
{
    public sealed class SafeHashSet<T> : ISet<T>
    {
        private readonly ISet<T> _hashSet;
        private readonly ReaderWriterLockTiny _locker;
        public int Count => _hashSet.Count;
        public bool IsReadOnly => false;

        public SafeHashSet(ISet<T> hashSet = null, ReaderWriterLockTiny locker = null)
        {
            _hashSet = hashSet ?? new HashSet<T>();
            _locker = locker ?? new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Add(T item)
        {
            _locker.EnterWriteLock();
            try
            {
                return _hashSet.Add(item);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void Clear()
        {
            _locker.EnterWriteLock();
            try
            {
                _hashSet.Clear();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool Contains(T item)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.Contains(item);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _locker.EnterReadLock();
            try
            {
                _hashSet.CopyTo(array, arrayIndex);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            _locker.EnterWriteLock();
            try
            {
                _hashSet.ExceptWith(other);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_hashSet, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void IntersectWith(IEnumerable<T> other)
        {
            _locker.EnterWriteLock();
            try
            {
                _hashSet.IntersectWith(other);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.IsProperSubsetOf(other);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.IsProperSupersetOf(other);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.IsSubsetOf(other);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.IsSupersetOf(other);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.Overlaps(other);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool Remove(T item)
        {
            _locker.EnterWriteLock();
            try
            {
                return _hashSet.Remove(item);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            _locker.EnterReadLock();
            try
            {
                return _hashSet.SetEquals(other);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _locker.EnterWriteLock();
            try
            {
                _hashSet.SymmetricExceptWith(other);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            _locker.EnterWriteLock();
            try
            {
                _hashSet.UnionWith(other);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }
    }
}