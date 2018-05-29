using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public struct SafeTableEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private readonly IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
        private readonly ReaderWriterLockSlim _locker;
        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current => _enumerator.Current;
        object IEnumerator.Current => _enumerator.Current;

        public SafeTableEnumerator(IDictionary<TKey, TValue> table, ReaderWriterLockSlim locker)
        {
            _locker = locker;
            _locker.EnterReadLock();
            _enumerator = table.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
            _locker.ExitReadLock();
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