using System.Collections;
using System.Collections.Generic;

namespace SafeCollections
{
    public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly object _locker;
        private readonly IDictionary<TKey, TValue> _table;

        public int Count
        {
            get
            {
                lock (_locker)
                {
                    return _table.Count;
                }
            }
        }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys => new SafeCollection<TKey>(_table.Keys, _locker);

        public ICollection<TValue> Values => new SafeCollection<TValue>(_table.Values, _locker);

        public SafeDictionary(IDictionary<TKey, TValue> table = null, object locker = null)
        {
            _table = table ?? new Dictionary<TKey, TValue>();
            _locker = locker ?? new object();
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (_locker)
                    return _table[key];
            }
            set
            {
                lock (_locker)
                    _table[key] = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (_locker)
            {
                if (_table.ContainsKey(key) == false)
                    _table.Add(key, value);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            lock (_locker)
            {
                if (_table.ContainsKey(item.Key) == false)
                    _table.Add(item);
            }
        }

        public void Clear()
        {
            lock (_locker)
                _table.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            lock (_locker)
                return _table.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            lock (_locker)
                return _table.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (_locker)
                _table.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => new SafeTableEnumerator<TKey, TValue>(_table, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(TKey key)
        {
            lock (_locker)
                return _table.Remove(key);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (_locker)
                return _table.Remove(item);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_locker)
                return _table.TryGetValue(key, out value);
        }
    }
}