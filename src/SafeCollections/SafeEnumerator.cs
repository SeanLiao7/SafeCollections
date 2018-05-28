using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public struct SafeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly ReaderWriterLockSlim _locker;

        public T Current => _enumerator.Current;

        object IEnumerator.Current => Current;

        public SafeEnumerator(IEnumerable<T> list, ReaderWriterLockSlim locker)
        {
            locker.EnterReadLock();
            _enumerator = list.GetEnumerator();
            _locker = locker;
        }

        public void Dispose() => _locker.ExitReadLock();

        public bool MoveNext() => _enumerator.MoveNext();

        public void Reset() => _enumerator.Reset();
    }
}