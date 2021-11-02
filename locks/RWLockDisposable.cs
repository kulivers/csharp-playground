using System;
using System.Threading;

namespace ThreadLocks
{
    class RWLockDisposable : IDisposable
    {
        public struct WriteLockToken : IDisposable
        {
            private readonly ReaderWriterLockSlim _rwlock;

            public WriteLockToken(ReaderWriterLockSlim rwlock)
            {
                _rwlock = rwlock;
                rwlock.EnterWriteLock();
            }

            public void Dispose() => _rwlock.ExitWriteLock();
        }

        public struct ReadLockToken : IDisposable
        {
            private readonly ReaderWriterLockSlim _lock;

            public ReadLockToken(ReaderWriterLockSlim @lock)
            {
                _lock = @lock;
                @lock.EnterReadLock();
            }

            public void Dispose() => _lock.ExitReadLock();
        }

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public ReadLockToken ReadLock() => new ReadLockToken(_lock);
        public WriteLockToken WriteLock() => new WriteLockToken(_lock);

        public void Dispose() => _lock.Dispose();
    }
}