using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace threading
{
    public interface IHistogram
    {
        string ImplementationName { get; }
        /// <summary>
        /// Returns an array with counts of an entrance of the each value in the data
        /// </summary>
        int[] Make(int[] data);
    }

    class Program
    {
        private static int[] MakeData(int size = 20000, int maxRange = 10)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var result = new int[size];
            for (int i = 0; i < size; i++)
                result[i] = random.Next(maxRange);

            return result;
        }

        static void Main(string[] args)
        {
            var maxRange = 10;
            var data = MakeData(100000000, 10);

            var arrayStrategies = new IHistogram[] { 
                new SyncArrayHistogram(maxRange)
                , new ParallelArrayHistogram(maxRange)
                , new PlinqArrayAggregateHistogram(maxRange)
            };

            var dictionaryStrategies = new IHistogram[] { 
                new SyncDictionaryHistogram() 
                , new ConcurrentDictionaryHistogram(maxRange)
                , new ParallelLockedDictionaryHistogram(maxRange)
                , new PlinqDictionaryAggregateHistogram(maxRange)
            };

            var strategies = arrayStrategies.Concat(dictionaryStrategies);
                //dictionaryStrategies;

            foreach (var strategy in strategies)
            {
                Console.WriteLine("Strategy: " + strategy.ImplementationName);
                var timer = new Stopwatch();
                timer.Start();
                var histogramm = strategy.Make(data);
                timer.Stop();
                Console.WriteLine("Time: " + timer.ElapsedMilliseconds);
                //Console.WriteLine("Result: " + string.Join(", ", histogramm)); 
                Console.WriteLine();
            }
        }
    }
}
