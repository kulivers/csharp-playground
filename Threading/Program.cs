using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ThreadingDemos;

namespace Threads
{
    internal static class Program
    {
        private static void SemaphoreExample()
        {
            Threads.SemaphoreExample.LocalMain();
        }

        private static void MonitorLockExample()
        {
            var lockShell = new LockShell();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
        }

        private static void MutexExample()
        {
            Console.WriteLine("now  dont press Enter and run \"Project for testing mutex\"");
            SimpleMutexClass.LocalMain();
        }

        static void CancelTaskExample()
        {
            
            var cts = new CancellationTokenSource();
            
            var ct = new CancellationToken();//No ct.Cansel()

            var cts2 = new CancellationTokenSource();
            var cts3 = new CancellationTokenSource();
            var cts4 = new CancellationTokenSource();
            var ctss = CancellationTokenSource.CreateLinkedTokenSource(cts2.Token, cts3.Token, cts4.Token);
                        
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
            }, token);

            Console.WriteLine("PRess enter to Stop");
            task.Start();
            Console.ReadLine();
            cts.Cancel();
             
        }

        static void TaskResult()
        {
            
        }
        private static void Main()
        {
            
        }
    }
}