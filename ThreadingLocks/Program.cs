using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    internal static class Program
    {
        private static void SemaphoreExample()
        {
            Threads.SemaphoreExample.LocalMain();
        }

        private static void MutexExample()
        {
            Console.WriteLine("now  dont press Enter and run \"Project for testing mutex\"");
            SimpleMutexClass.LocalMain();
        }

        static void CancelTaskExample()
        {
            var cts = new CancellationTokenSource();

            var ct = new CancellationToken();
            var cts2 = new CancellationTokenSource();
            var cts3 = new CancellationTokenSource();
            var cts4 = new CancellationTokenSource();

            var multiTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cts2.Token, cts3.Token, cts4.Token);

            CancellationToken token = cts.Token;

            var task = new Task(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) // =  token.ThrowIfCancellationRequested();  
                    {
                        Console.WriteLine("canceled");
                        throw new OperationCanceledException(token);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
            }, multiTokenSource.Token);

            Console.WriteLine("PRess enter to Stop");
            task.Start();
            Console.ReadLine();
            cts.Cancel();
        }

        static void SameAndDiffObjectsInLock()
        {
            LockExamples.SameAndDiffObjectsInLock();
        }

        static void LockedThreadsCountExample()
        {
            MonitorExamples.ExitAndLockedThreadsCount();
        }

        static void MonitorWaitExample()
        {
            MonitorExamples.WaitPulse();
        }

        static void BarrierExample()
        {
            GettingCoinsThreadsExample gettingCoinsThreadsExample = new GettingCoinsThreadsExample();
            gettingCoinsThreadsExample.GetSomeMoney();
        }

        static void RWTokenExample()
        {
            var rwLock = new RWLock();

            using (rwLock.ReadLock())
            {
            }
        }

        static void RWExample()
        {
            //there is a lot of methods, but here is just example
            var sync = new SynchronizedCache();
            sync.MultiAccessToUpgradeableReadLock();
        }

        private static void Main()
        {
            RWExample();
        }
    }
}