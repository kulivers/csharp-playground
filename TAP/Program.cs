using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    class MultiThreadList

    {
        private List<int> Ls { get; set; }

        public async Task FillEmptyList(int elementsCount, int threadCount)
        {
            var elsInOneArr = elementsCount / threadCount - 1;
            var elsInLastThread = elementsCount - elsInOneArr * threadCount;
            var locker = new object();
            Parallel.For(0, threadCount, i =>
            {
                var localList = new List<int>();
                // var localList = new ThreadLocal<List<int>>();
                var rand = new ThreadLocal<Random>(() => new Random()).Value;
                // localList.Value.Add(rand.Next(0, 20));
                localList.Add(rand.Next(0, 20));
                lock (locker)
                {
                    Ls.AddRange(localList);
                }
            });
        }

        public void Show()
        {
            Ls.ForEach(Console.WriteLine);
        }
    }

    class Program
    {
        static void Main()
        {
            

        }

        protected static long TimeTracker(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            var span = stopwatch.Elapsed;
            return span.Milliseconds;
        }
    }
}