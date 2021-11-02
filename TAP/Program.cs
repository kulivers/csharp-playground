using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TAP.Exercises;

namespace TAP
{
    class Program
    {
        static List<int> GetRandomArray(int count, int min = 0, int max = 20)
        {
            var rand = new Random();
            var list = new List<int>();
            for (var i = 0; i < count; i++)
            {
                list.Add(rand.Next(min, max));
            }

            return list;
        }


        static async Task Main()
        {
            ShowTrackTimesOfStatisticCounters();
        }

        static void ShowTrackTimesOfStatisticCounters()
        {
            var randomList = GetRandomArray(2000000);
            const int threadCount = 8;

            var plinqAggregateTime = TimeTracker(() =>
            {
                var myList = new MyList(randomList);
                myList.GetDictionaryOfIntsCount();
            });
            
            var ConcurrentTime = TimeTracker(() =>
            {
                var concurrentDictionary = new MyConcurrentDictionary();
                concurrentDictionary.FillConcurrentDictionaryFromList(randomList, threadCount);
            });
            
            var dictionaryWithLockTime = TimeTracker(() =>
            {
                var dictionary = new MyDictionary();
                dictionary.FillTheDictFromListWithLock(randomList, threadCount);
            });

            var synchronouslyLockingTime = TimeTracker(() =>
            {
                var syncDictionary = new MySyncDictionary(randomList);
                syncDictionary.FillTheDictFromList();
            });
            Console.WriteLine("Aggregate: " + plinqAggregateTime);
            Console.WriteLine("concurrent: " + ConcurrentTime);
            Console.WriteLine("with lock: " + dictionaryWithLockTime);
            Console.WriteLine("sync: " + synchronouslyLockingTime);
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