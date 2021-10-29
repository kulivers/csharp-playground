using System;
using System.Diagnostics;
using TAP.Exercises;

namespace TAP
{
    class Program
    {
        static void Main()
        {
         
        }

        static void ShowTrackTimesOfFillers()
        {
            var timeMultiThreadArray = TimeTracker(() =>
            {
                var multiThreadList = new MultiThreadList(20001);
                multiThreadList.FillArrayRandoms(20);
            });
            var timeConcurrent = TimeTracker(() =>
            {
                var multiThreadList = new MySelfFillingConcurrentDictionary();
                multiThreadList.FillTheDictRandNumbers(20);
            });
            var timeDictionary = TimeTracker(() =>
            {
                var multiThreadList = new MySelfFillingDictionary();
                multiThreadList.FillTheDictRandNumbers(20);
            });

            var timeSynchronislyLocking = TimeTracker(() =>
            {
                var multiThreadList = new MySelfFillingDictionaryWithoutLocking();
                multiThreadList.FillTheDictRandNumbers(20);
            });
            Console.WriteLine(timeConcurrent);
            Console.WriteLine(timeDictionary);
            Console.WriteLine(timeSynchronislyLocking);
            Console.WriteLine(timeMultiThreadArray);
        }

        protected static long TimeTracker(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            var span = stopwatch.Elapsed;
            return span.Ticks;
        }
    }
}