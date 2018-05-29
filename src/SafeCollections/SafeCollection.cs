using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SafeCollections
{
    public sealed class SafeCollection<T> : ICollection<T>
    {
        private readonly ICollection<T> _collection;
        private readonly ReaderWriterLockSlim _locker;

        public int Count
        {
            get
            {
                lock (_locker)
                    return _collection.Count;
            }
        }

        public bool IsReadOnly => false;

        public SafeCollection(ICollection<T> collection = null, ReaderWriterLockSlim locker = null)
        {
            _collection = collection ?? new Collection<T>();
            _locker = locker ?? new ReaderWriterLockSlim();
        }

        public void Add(T item)
        {
            lock (_locker)
                _collection.Add(item);
        }

        public void Clear()
        {
            lock (_locker)
                _collection.Clear();
        }

        public bool Contains(T item)
        {
            lock (_locker)
                return _collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_locker)
                _collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_collection, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(T item)
        {
            lock (_locker)
                return _collection.Remove(item);
        }
    }
}