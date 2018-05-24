using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public struct SafeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly object _locker;

        public T Current => _enumerator.Current;

        object IEnumerator.Current => Current;

        public SafeEnumerator(IEnumerable<T> list, object locker)
        {
            Monitor.Enter(locker);
            _enumerator = list.GetEnumerator();
            _locker = locker;
        }

        public void Dispose() => Monitor.Exit(_locker);

        public bool MoveNext() => _enumerator.MoveNext();

        public void Reset() => _enumerator.Reset();
    }
}