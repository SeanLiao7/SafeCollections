using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SafeCollections
{
    public sealed class SafeEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerator;
        private readonly ReaderWriterLockSlim _locker;

        public SafeEnumerable(IEnumerable<T> enumerator, ReaderWriterLockSlim locker)
        {
            _locker = locker;
            _enumerator = enumerator;
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_enumerator, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}