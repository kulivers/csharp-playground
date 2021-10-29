using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    public class SynchronizedCache
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private Dictionary<int, string> innerCache = new Dictionary<int, string>();

        public int Count
        {
            get { return innerCache.Count; }
        }

        public string Read(int key)
        {
            cacheLock.EnterReadLock();
            try
            {
                return innerCache[key];
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void Add(int key, string value)
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Add(key, value);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public bool AddWithTimeout(int key, string value, int timeout)
        {
            if (cacheLock.TryEnterWriteLock(timeout))
            {
                try
                {
                    innerCache.Add(key, value);
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void MultiAccessToUpgradeableReadLock() //???
        {
            new Thread(() =>
            {
                cacheLock.EnterUpgradeableReadLock();
                // in other thread IsUpgradeableReadLockHeld = false
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("locking thread entered UpgradeableReadLock");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1011000);
                

                cacheLock.EnterWriteLock();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("locking thread updated thread to write lock");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(10000);
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("locking thread realised UpgradeableReadLock");
                Console.ForegroundColor = ConsoleColor.White;
                cacheLock.ExitUpgradeableReadLock();
            }).Start();
            
            new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("press key to new thread try get EnterUpgradeableReadLock(u), read(r), write(w)");
                    var c = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    switch (c)
                    {
                        case 'u':
                            Console.WriteLine(cacheLock.TryEnterUpgradeableReadLock(1000)
                                ? "new thread got .UpgradeableReadLock"
                                : "new thread didnt got UpgradeableReadLock");
                            break;
                        case 'r':
                            Console.WriteLine(cacheLock.TryEnterReadLock(1000)
                                ? "new thread got ReadLock"
                                : "new thread didnt got ReadLock");
                            break;
                        case 'w':
                            Console.WriteLine(cacheLock.TryEnterWriteLock(1000)
                                ? "new thread got WriteLock"
                                : "new thread didnt got WriteLock");
                            break;
                    }
                }
            }).Start();
        }

        public AddOrUpdateStatus AddOrUpdate(int key, string value)
        {
            cacheLock.EnterUpgradeableReadLock();
            try
            {
                string result = null;
                if (innerCache.TryGetValue(key, out result))
                {
                    if (result == value)
                    {
                        return AddOrUpdateStatus.Unchanged;
                    }
                    else
                    {
                        cacheLock.EnterWriteLock();
                        try
                        {
                            innerCache[key] = value;
                        }
                        finally
                        {
                            cacheLock.ExitWriteLock();
                        }

                        return AddOrUpdateStatus.Updated;
                    }
                }
                else
                {
                    cacheLock.EnterWriteLock();
                    try
                    {
                        innerCache.Add(key, value);
                    }
                    finally
                    {
                        cacheLock.ExitWriteLock();
                    }

                    return AddOrUpdateStatus.Added;
                }
            }
            finally
            {
                cacheLock.ExitUpgradeableReadLock();
            }
        }

        public void Delete(int key)
        {
            cacheLock.EnterWriteLock();
            try
            {
                innerCache.Remove(key);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public enum AddOrUpdateStatus
        {
            Added,
            Updated,
            Unchanged
        };

        ~SynchronizedCache()
        {
            if (cacheLock != null) cacheLock.Dispose();
        }
    }
}