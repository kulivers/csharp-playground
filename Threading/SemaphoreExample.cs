using System;
using System.Threading;

namespace Threads
{
    public class SemaphoreExample
    {
        private static Thread[] _threads = new Thread[10];
        private static SemaphoreSlim sem = new SemaphoreSlim(1, 5);
        //1 = сколько сейчас свободно, 5 - вместимость 
        public static void LocalMain()
        {
            sem.Release();
            sem.Release();
            for (int i = 0; i < 6; i++)
            {
                _threads[i] = new Thread(Method);
                _threads[i].Name = "thread_" + i;
                _threads[i].Start();
            }
        }

        static void Method()
        {
            CwWaiting();
            sem.Wait();
            CwEnters();
            Thread.Sleep(500);
            CwLeaving();
            sem.Release();
        }

        private static void CwLeaving()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} is leaving", Thread.CurrentThread.Name);
        }

        private static void CwEnters()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("{0} enters the method", Thread.CurrentThread.Name);
        }

        static void CwWaiting()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("{0} is waiting in line", Thread.CurrentThread.Name);
        }
    }
}