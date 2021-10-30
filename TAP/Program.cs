using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TAP.Exercises;

namespace TAP
{
    class Program
    {
        static async Task Main()
        {
            // ShowTrackTimesOfFillers();
            var w = new MultiThreadList();
            await w.FillArrayRandomsByMakingNArrays(10, 20);
            var dict = w.GetDictionaryOfIntsCount();
            var sum = dict.Values.Sum();
            Console.WriteLine($"sum: {sum}");
        }

        static void ShowTrackTimesOfFillers()
        {
            var timeMultiThreadArray = TimeTracker(async () =>
            {
                var w = new MultiThreadList();
                await w.FillArrayRandomsByMakingNArrays(20001, 20);
                var dict = w.GetDictionaryOfIntsCount();
                var sum = dict.Values.Sum();
                Console.WriteLine($"sum: {sum}");
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

            var timeSynchronouslyLocking = TimeTracker(() =>
            {
                var multiThreadList = new MySelfFillingSyncDictionary();
                multiThreadList.FillTheDictRandNumbers(20);
            });
            Console.WriteLine(timeConcurrent);
            Console.WriteLine(timeDictionary);
            Console.WriteLine(timeSynchronouslyLocking);
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