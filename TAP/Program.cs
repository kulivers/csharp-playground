using System;
using System.Threading;
using System.Threading.Tasks;

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


        static void WhenAnyTesting()
        {
            Whens.WhenAny.TryingToUseWhenAnyWithTwoTasks();
        }

        static void DefferedTask(int ms)
        {
            var t = new Task(() => { Console.WriteLine("task"); });
         
            TimerTasks.DeferredTask.RunTaskAfterNSec(t, ms);
            Console.ReadLine();
        }

        static void Main()
        {
            // WhenAnyTesting();
            DefferedTask(4000);
        }
    }
}