using System;
using System.Threading;
using System.Threading.Tasks;
using TAP.Exceptions;
using TAP.Whens;

namespace TAP
{
    public static class AwaitAndTaskExamples
    {
        public static void WatchThreadsWhileAwaiting()
        {
            Awaits.AwaitPlayground.WatchThreadsWhileAwaiting();
        }

        public static void DiffWithAwaitAndWithout()
        {
            Awaits.AwaitPlayground.AsyncParallelDiff();
            Console.ReadLine();
        }

        public static async void CallingTasks()
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

        public static async Task WhenAnyTesting()
        {
            await WhenAny.TryingToUseWhenAnyWithTwoTasks();
        }

        public static void DefferedTask(int ms)
        {
            var t = new Task(() => { Console.WriteLine("task"); });

            TimerTasks.DeferredTask.RunTaskAfterNSec(t, ms);
            Console.ReadLine();
        }

        public static void WaitAllTasksExample()
        {
            WaitAll.TryingToWaitTasks();
            Console.ReadLine();
        }

        public static void WhenAnyExceptionExample()
        {
            WhenAny.WhenAnyExceptionExample();
            Console.ReadLine();
        }

        public static async Task WhenAllExceptions2()
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

        public static async Task ReadingFileAsyncExample()
        {
            for (int i = 0; i < 1; i++)
            {
                await AsyncFileReadingExample.Do();
                Console.WriteLine("__________________________");
                Console.WriteLine();
            }
        }

        public static async Task CatchingTaskWithContinuation()
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

        public static async Task AggregateExceptionFromWhenAll()
        {
            await SeveralExceptionsInTask.WhenAllWithManyExceptions();
        }

    }
}