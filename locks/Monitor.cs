using System;
using System.Threading;

namespace ThreadLocks
{
    internal static class MonitorExamples
    {
        public static void WaitPulse() // it could be with pulseAll()
        {
            object phone = new object();

            void Work()
            {
                lock (phone) // Sort of "Turn the phone on while at work"
                {
                    while (true)
                    {
                        Console.WriteLine("worker wait(phone)");
                        Monitor.Wait(phone); // Wait for a signal from the boss
                        Console.WriteLine("worker doing something");
                        Console.WriteLine("worker pulseAll(phone)");
                        Monitor.Pulse(phone); // Signal boss we are done
                        Console.WriteLine("worker end");

                    }
                }
            }

            void BossNOTWork()
            {
                //preparework
                lock(phone) // Grab the phone when I have something ready for the worker
                {
                    Console.WriteLine("Boss PulseAll");
                    Monitor.Pulse(phone); // Signal worker there is work to do
                    Console.WriteLine("Boss wait");
                    Monitor.Wait(phone); // Wait for the work to be done
                    Console.WriteLine("BOSS EnD");
                }

            }

            
            new Thread(Work) { Name = "Worker Thread" }.Start();
            Thread.Sleep(1000);
            new Thread(BossNOTWork) { Name = "Boss Thread" }.Start();
        }


        public static void ExitAndLockedThreadsCount()
        {
            object o = new object();

            void M()
            {
                Monitor.Enter(o);
                Console.WriteLine("Doing smth in thread {0}", Thread.CurrentThread.Name);
                Thread.Sleep(3000);
                Monitor.Exit(o);
                Console.WriteLine(".......................{0} thread countinues after exit", Thread.CurrentThread.Name);
            }

            for (int i = 0; i < 5; i++)
            {
                new Thread(M) { Name = i.ToString() }.Start();
            }

            Thread.Sleep(1000);
            Console.WriteLine("there are {0} threads are waiting", Monitor.LockContentionCount);
            Console.ReadLine();
            Console.WriteLine(Monitor.LockContentionCount);
        }
    }
}
