using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    class PLINQExamples
    {
        public static void Bufferisation()
        {
            void FullyBuffered()
            {
                IEnumerable<int> FullyBufferedResults = ParallelEnumerable.Range(0, 10)
                    .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                    .Select(i =>
                    {
                        Thread.Sleep(1000);
                        return i;
                    });

                Stopwatch sw = Stopwatch.StartNew();

                foreach (int i in FullyBufferedResults)
                {
                    Console.WriteLine("Значение: {0}, Время: {1}", i, sw.ElapsedMilliseconds);
                }
            }

            void NotBuffered()
            {
                IEnumerable<int> results = ParallelEnumerable.Range(0, 10)
                    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                    .Select(i =>
                    {
                        Thread.Sleep(1000);
                        return i;
                    });

                Stopwatch sw = Stopwatch.StartNew();

                foreach (int i in results)
                {
                    Console.WriteLine("Значение: {0}, Время: {1}", i, sw.ElapsedMilliseconds);
                }
            }

            void AutoBuff()
            {
                IEnumerable<int> results = ParallelEnumerable.Range(0, 10)
                    .WithMergeOptions(ParallelMergeOptions.AutoBuffered)
                    .Select(i =>
                    {
                        Thread.Sleep(1000);
                        return i;
                    });

                Stopwatch sw = Stopwatch.StartNew();

                foreach (int i in results)
                {
                    Console.WriteLine("Значение: {0}, Время: {1}", i, sw.ElapsedMilliseconds);
                }
            }

            Console.WriteLine("FullyBuffered();");
            FullyBuffered();
            Console.WriteLine("AutoBuff();");
            AutoBuff();
            Console.WriteLine("NotBuffered();");
            NotBuffered();
        }

        public static void OrderedVsUnOrdered() //syncron 
            // Операция AsSequential больше всего нужна, когда требуется включать или отключать параллельное выполнение
        {
            string[] cars =
            {
                "Nissan", "Aston Martin", "Chevrolet", "Alfa Romeo", "Chrysler", "Dodge", "BMW",
                "Ferrari", "Audi", "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru", "Жигули :)"
            };
            var auto = cars.AsParallel().AsOrdered().Take(5);
            var auto2 = cars.AsParallel().Take(5);
            Console.Write("ordered: ");
            foreach (var s in auto)
            {
                Console.Write(s + " ,");
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("not ordered(random): ");
            foreach (var s in auto2)
            {
                Console.Write(s + " ,");
            }
        }

        public static async Task ParallelAggregateExample()
        {
            // Create a data source for demonstration purposes.
            int[] source = new int[10];
            Random rand = new Random();
            for (int x = 0; x < source.Length; x++)
            {
                // Should result in a mean of approximately 15.0.
                source[x] = rand.Next(0, 5);
            }

            // Standard deviation calculation requires that we first
            // calculate the mean average. Average is a predefined
            // aggregation operator, along with Max, Min and Count.
            double mean = source.AsParallel().Average();

            // We use the overload that is unique to ParallelEnumerable. The
            // third Func parameter combines the results from each thread.
            double standardDev = source.AsParallel().Aggregate(
                // initialize subtotal. Use decimal point to tell
                // the compiler this is a type double. Can also use: 0d.
                0.0,

                // do this on each thread
                (subtotal, item) => subtotal + item,

                // aggregate results after all threads are done.
                (total, thisThread) => total + thisThread,

                // perform standard deviation calc on the aggregated result.
                (finalSum) => (finalSum / 2)
            );
            Console.WriteLine("Mean value is = {0}", mean);
            Console.WriteLine("Standard deviation is {0}", standardDev);
            Console.ReadLine();
        }

        public static void CalcAvgByAggregate()
        {
            int[] source = new int[10];
            Random rand = new Random();
            for (int x = 0; x < source.Length; x++)
            {
                // Should result in a mean of approximately 15.0.
                source[x] = rand.Next(0, 5);
            }


            var avgdefault = source.Average();
            var avgdefaultParallel = source.AsParallel().Average();

            var myAvg = source.AsParallel().Aggregate(
                (sum: 0d, count: 0d), //TAccumulate seed, 
                (local, itemInThread) =>
                {
                    local.sum += itemInThread;
                    local.count++;
                    return local;
                },
                (a, b) =>
                    (a.sum + b.sum, a.count + b.count),
                a => a.sum / a.count
            );
            Console.WriteLine(avgdefault);
            Console.WriteLine(avgdefaultParallel);
            Console.WriteLine(myAvg);
        }
    }
}