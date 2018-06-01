using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SafeCollections
{
    public sealed class SafeCollection<T> : ICollection<T>
    {
        private readonly ICollection<T> _collection;
        private readonly ReaderWriterLockTiny _locker;
        public int Count => _collection.Count;
        public bool IsReadOnly => false;

        public SafeCollection(ICollection<T> collection = null, ReaderWriterLockTiny locker = null)
        {
            _collection = collection ?? new Collection<T>();
            _locker = locker ?? new ReaderWriterLockTiny();
        }

        public void Add(T item)
        {
            _locker.EnterWriteLock();
            try
            {
                _collection.Add(item);
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
                _collection.Clear();
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
                return _collection.Contains(item);
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
                _collection.CopyTo(array, arrayIndex);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_collection, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(T item)
        {
            _locker.EnterWriteLock();
            try
            {
                return _collection.Remove(item);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }
    }
}