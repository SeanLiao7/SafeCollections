using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SafeCollections
{
    public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly object _locker = new object();
        private readonly Dictionary<TKey, TValue> _table = new Dictionary<TKey, TValue>();

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

        public TValue this[TKey key]
        {
            get
            {
                lock (_locker)
                {
                    return _table[key];
                }
            }
            set
            {
                lock (_locker)
                {
                    _table[key] = value;
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (_locker)
            {
                if (_table.ContainsKey(key) == false)
                    (_table as IDictionary).Add(key, value);
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            lock (_locker)
            {
                if (_table.ContainsKey(item.Key) == false)
                    (_table as ICollection<KeyValuePair<TKey, TValue>>).Add(item);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _table.Clear();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            lock (_locker)
            {
                return _table.Contains(item);
            }
        }

        public bool ContainsKey(TKey key)
        {
            lock (_locker)
            {
                return _table.ContainsKey(key);
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (_locker)
            {
                (_table as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => new SafeTableEnumerator<TKey, TValue>(_table, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(TKey key)
        {
            lock (_locker)
            {
                return _table.Remove(key);
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (_locker)
            {
                return (_table as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_locker)
            {
                return _table.TryGetValue(key, out value);
            }
        }
       
    }
}