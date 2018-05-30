using System.Threading;

namespace SafeCollections
{
    public struct ReaderWriterLockTiny
    {
        // if lock is above this value then somebody has a write lock
        private const int WriterLock = 1000000;

        // lock state counter
        private int _lock;

        public void EnterReadLock()
        {
            var w = new SpinWait();
            var tmpLock = _lock;
            while (tmpLock >= WriterLock ||
                   tmpLock != Interlocked.CompareExchange(ref _lock, tmpLock + 1, tmpLock))
            {
                w.SpinOnce();
                tmpLock = _lock;
            }
        }

        public void EnterWriteLock()
        {
            var w = new SpinWait();

            while (0 != Interlocked.CompareExchange(ref _lock, WriterLock, 0))
                w.SpinOnce();
        }

        public void ExitReadLock()
        {
            Interlocked.Decrement(ref _lock);
        }

        public void ExitWriteLock()
        {
            _lock = 0;
        }
    }
}