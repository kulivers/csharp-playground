using System;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    class Program
    {
        static void WatchThreadsWhileAwaiting()
        {
            AwaitPlayground.WatchThreadsWhileAwaiting();
        }

        static void DiffWithAwaitAndWithout()
        {
            AwaitPlayground.AsyncParallelDiff();
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


        
        static void Main()
        {
            WhenAnyTaskPlayground.TestWhenAny();
        }
    }
}