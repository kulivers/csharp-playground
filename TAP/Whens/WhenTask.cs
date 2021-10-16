using System;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    namespace Whens
    {
        public class WaitAll
        {
            static Task FiveSec()
            {
                return new Task(() =>
                {
                    Console.WriteLine("Task FiveSec() starts");
                    Thread.Sleep(5000);
                    Console.WriteLine("Task FiveSec() ends");
                });
            }

            static Task TwoSec()
            {
                return new Task(() =>
                {
                    Console.WriteLine("Task TwoSec() starts");
                    Thread.Sleep(2000);
                    Console.WriteLine("Task TwoSec() ends");
                });
            }


            //??? question
            //waitAll returns true if all of the Task instances completed execution within the allotted time; otherwise, false.
            //why it returns false??
            public static async void TryingToWaitTasks()
            {
                var tasks = new Task[2] { TwoSec(), FiveSec() };
                var res = Task.WaitAll(tasks, 6000);
                Console.WriteLine(res);
            }
        }


        public class WhenAny
        {
            private static async Task<T> WithTimeout<T>(Task<T> task, int time)
            {
                Task delayTask = Task.Delay(time);
                Task firstToFinish = await Task.WhenAny(task, delayTask);
                if (firstToFinish == delayTask)
                {
                    // Первой закончилась задача задержки – разобраться с исключениями
                    task.ContinueWith((task) =>
                    {
                        Console.WriteLine("continue");
                        Console.WriteLine(task.Status);
                    });
                    throw new TimeoutException();
                }

                // Если мы дошли до этого места, исходная задача уже завершилась
                return await task;
            }

            static async Task FiveSec()
            {
                Console.WriteLine("Task FiveSec() starts");
                Thread.Sleep(5000);
                Console.WriteLine("Task FiveSec() ends");
            }

            static async Task TwoSec()
            {
                Console.WriteLine("Task TwoSec() starts");
                Thread.Sleep(2000);
                Console.WriteLine("Task TwoSec() ends");
            }

            static async Task FiveSecWithNullException()
            {
                Console.WriteLine("Task FiveSecWithException() starts");
                Thread.Sleep(5000);
                int a = 0;
                int b = 1 / a;
                Console.WriteLine("Task FiveSec() ends");
            }

            static async Task TwoSecWithArrayException()
            {
                Console.WriteLine("Task TwoSecWithException() starts");
                Thread.Sleep(2000);
                Console.WriteLine("Task TwoSec() ends");
                int[] arr = new int[1];
                arr[123] = 121;
            }

            public static async void TryingToUseWhenAnyWithTwoTasks()
            {
                //???
                //Why it runs TwoSec 2 times?? 
                //And why it doesn't run on parallel?

                //The returned task will always end in the RanToCompletion state with its Result set to the first task to complete.
                //This is true even if the first task to complete ended in the Canceled or Faulted state.
                //почему она RanToCompletion если первая Cancel or Faulted? в чем логика

                Task delayTask = Task.Delay(1000);
                Task first = await Task.WhenAny(TwoSec(), FiveSec());
                Console.WriteLine(first == TwoSec());
            }

            public static async void WhenAnyExceptionExample()
            {
                //??? why no exception, how to catch or get status, etc...
                await TwoSecWithArrayException();
                
                // Console.WriteLine(TwoSecWithArrayException().Exception);
                // Console.WriteLine(TwoSecWithArrayException().Status);
                // Console.WriteLine(TwoSecWithArrayException().IsCompleted);
                // Console.WriteLine(TwoSecWithArrayException().IsFaulted);
                // Console.WriteLine(TwoSecWithArrayException().IsCompletedSuccessfully);

                
                //     var tasks = new Task[4] { FiveSec(), FiveSecWithNullException(), TwoSec(), TwoSecWithArrayException() };
                //     var waiter = Task.WhenAny(tasks);
                //     Debugger.Break();
            }
        }
    }
}