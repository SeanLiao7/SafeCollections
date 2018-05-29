using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public sealed class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly ReaderWriterLockSlim _locker;
        private readonly IDictionary<TKey, TValue> _table;

        public int Count
        {
            get
            {
                _locker.EnterReadLock();
                try
                {
                    return _table.Count;
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys => new SafeCollection<TKey>(_table.Keys, _locker);

        public ICollection<TValue> Values => new SafeCollection<TValue>(_table.Values, _locker);

        public SafeDictionary(IDictionary<TKey, TValue> table = null, ReaderWriterLockSlim locker = null)
        {
            _table = table ?? new Dictionary<TKey, TValue>();
            _locker = locker ?? new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        public TValue this[TKey key]
        {
            get
            {
                _locker.EnterReadLock();
                try
                {
                    return _table[key];
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            set
            {
                _locker.EnterWriteLock();
                try
                {
                    _table[key] = value;
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            _locker.EnterWriteLock();
            try
            {
                if (_table.ContainsKey(key) == false)
                    _table.Add(key, value);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            _locker.EnterWriteLock();
            try
            {
                if (_table.ContainsKey(item.Key) == false)
                    _table.Add(item);
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
                _table.Clear();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            _locker.EnterReadLock();
            try
            {
                return _table.Contains(item);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool ContainsKey(TKey key)
        {
            _locker.EnterReadLock();
            try
            {
                return _table.ContainsKey(key);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _locker.EnterReadLock();
            try
            {
                _table.CopyTo(array, arrayIndex);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => new SafeTableEnumerator<TKey, TValue>(_table, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(TKey key)
        {
            _locker.EnterWriteLock();
            try
            {
                return _table.Remove(key);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            _locker.EnterWriteLock();
            try
            {
                return _table.Remove(item);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {

            _locker.EnterReadLock();
            try
            {
                return _table.TryGetValue(key, out value);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }
    }
}