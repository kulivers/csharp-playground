using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TAP.Exceptions;
using TAP.Exercises;
using TAP.Whens;

namespace TAP
{
    class Program
    {
        static void WatchThreadsWhileAwaiting()
        {
            Awaits.AwaitPlayground.WatchThreadsWhileAwaiting();
        }

        static void DiffWithAwaitAndWithout()
        {
            Awaits.AwaitPlayground.AsyncParallelDiff();
            Console.ReadLine();
        }


        static async void CallingTasks()
        {
            static async Task<int> SomeAsync()
            {
                for (int i = 1; i <= 99; i++)
                {
                    Console.Write(i + " ");
                    Thread.Sleep(1);
                }

                Console.WriteLine();
                return 1;
            }

            Task<int> task = SomeAsync();
            var a = await task;
            Console.WriteLine(a);
            //something with await
        }


        static async Task WhenAnyTesting()
        {
            await WhenAny.TryingToUseWhenAnyWithTwoTasks();
        }

        static void DefferedTask(int ms)
        {
            var t = new Task(() => { Console.WriteLine("task"); });

            TimerTasks.DeferredTask.RunTaskAfterNSec(t, ms);
            Console.ReadLine();
        }

        static void WaitAllTasksExample()
        {
            WaitAll.TryingToWaitTasks();
            Console.ReadLine();
        }

        static void WhenAnyExceptionExample()
        {
            WhenAny.WhenAnyExceptionExample();
            Console.ReadLine();
        }

        static async Task WhenAllExceptions2()
        {
            // WhenAllException.DoMultipleAsync();

            //??? почему ниже исключения не выводятся?
            try
            {
                await WhenAllException.DoMultipleAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadLine();
        }

        static async Task ReadingFileAsyncExample()
        {
            for (int i = 0; i < 1; i++)
            {
                await AsyncFileReadingExample.Do();
                Console.WriteLine("__________________________");
                Console.WriteLine();
            }
        }

        static async Task CatchingTaskWithContinuation()
        {
            var task = new Task(() =>
            {
                Console.WriteLine("sdad");
                var a = new int[1];
                a[4] = 1;
            });
            task.Start();
            task.ForgetSafelyExtention();
        }

        static async Task AggregateExceptionFromWhenAll()
        {
            await SeveralExceptionsInTask.WhenAllWithManyExceptions();
        }

        static void TimeTracker(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            var span = stopwatch.Elapsed;
            Console.WriteLine("ms: {0}", span.Milliseconds);
        }

        static void Main()
        {
            TimeTracker(DictionaryMultiAccessExample);
            TimeTracker(ConcurrentDictionaryAccessExample);
            TimeTracker(SingleThreadAccessDictionaryExample);
        }


        private static void SingleThreadAccessDictionaryExample()
        {
            try
            {
                var example = new MySelfFillingDictionaryWithoutLocking();
                example.FillTheDict();
                // example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ConcurrentDictionaryAccessExample()
        {
            try
            {
                var example = new MySelfFillingConcurrentDictionary();
                example.FillTheDict();
                // example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void DictionaryMultiAccessExample()
        {
            try
            {
                var example = new MySelfFillingDictionary();
                example.FillTheDict();

                // example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}