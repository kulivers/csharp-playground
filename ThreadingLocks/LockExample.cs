using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    static class LockExamples
    {
        public static void SameAndDiffObjectsInLock()
        {
            LockWithSameObject();
            // LockWithDifferentObjects();
            Console.ReadLine();
        }

        private static void LockWithSameObject()
        {
            object o = new object();

            void IncrementSource(ref int source)
            {
                lock (o)
                {
                    source++;
                    Console.WriteLine("source: {0}", source);
                    Thread.Sleep(1000);
                }
            }

            int source = 0;
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => IncrementSource(ref source));
            }
        }

        private static void LockWithDifferentObjects()
        {
            void IncrementSource(ref int source)
            {
                lock (new object())
                {
                    source++;
                    Console.WriteLine("source: {0}", source);
                    Thread.Sleep(1000);
                }
            }

            int source = 0;
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => IncrementSource(ref source));
            }
        }
    }
}