using System;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    namespace Awaits
    {
        internal class AwaitPlayground
        {
            private static async void SayHelloWithLogsInConsole()
            {
                Console.WriteLine("SayHelloAfter5Sec() started and sleeping....");
                Thread.Sleep(1000);
                Console.WriteLine("SayHelloAfter5Sec() starts await task, thread: " +
                                  Thread.CurrentThread.ManagedThreadId);

                await Task.Run(() =>
                {
                    Console.WriteLine("thread inside await: " + Thread.CurrentThread.ManagedThreadId);

                    Thread.Sleep(4000);
                    Console.WriteLine("Hello!!!!!!!!!!!!!!!!");
                });
                Console.WriteLine("SayHelloAfter5Sec() ends, thread: " + Thread.CurrentThread.ManagedThreadId);
            }


            private static async void SayHelloAfter2Seconds()
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Hello After 5 Seconds");
                });
                Console.WriteLine("after 5");
            }

            private static void SayHelloAfter5Seconds()
            {
                Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Hello After 2 Seconds");
                });
                Console.WriteLine("after2");
            }

            public static void ShowWorkingThreads()
            {
                ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
                Console.WriteLine(workerThreads + " - " + completionPortThreads);
            }

            private static void ShowWorkingThreadsEveryXMs(int x)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
                        Console.WriteLine(workerThreads + " - " + completionPortThreads);
                        Thread.Sleep(x);
                    }
                }).Start();
            }

            public static void AsyncParallelDiff()
            {
                SayHelloAfter2Seconds();
                SayHelloAfter5Seconds();
                Console.WriteLine("continue Main thread");
            }


            public static void WatchThreadsWhileAwaiting()
            {
                //to be not main thread
                Task.Run(() =>
                {
                    ShowWorkingThreadsEveryXMs(100);
                    Thread.Sleep(100);
                    SayHelloWithLogsInConsole();
                });
            }
        }
    }
}