using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public struct SafeTableEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private readonly IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
        private readonly object _locker;
        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current => _enumerator.Current;
        object IEnumerator.Current => _enumerator.Current;

        public SafeTableEnumerator(Dictionary<TKey, TValue> table, object locker)
        {
            Monitor.Enter(locker);
            _enumerator = table.GetEnumerator();
            _locker = locker;
        }

        void IDisposable.Dispose()
        {
            Monitor.Exit(_locker);
        }

        bool IEnumerator.MoveNext()
        {
            return _enumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            _enumerator.Reset();
        }
    }
}
