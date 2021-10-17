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
            //MSDN: waitAll returns true if all of the Task instances completed execution within the allotted time; otherwise, false.
            //why it returns false??
            public static void TryingToWaitTasks()
            {
                var tasks = new Task[] { TwoSec(), FiveSec() };
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

                Task.Run(() =>
                {
                    int a = 0;
                    int b = 1 / a;
                });
                int[] arr = new int[1];
                arr[123] = 121;
                
            }

            public static async void TryingToUseWhenAnyWithTwoTasks()
            {
                //???
                //Why it runs TwoSec 2 times?? 
                //And why it doesn't run on parallel?

                //MSDN: The returned task will always end in the RanToCompletion state with its Result set to the first task to complete.
                //This is true even if the first task to complete ended in the Canceled or Faulted state.
                //почему она RanToCompletion если первая Cancel or Faulted? в чем логика

                //почему без await выводится false? он же заканчивает все равно, там выводится что NSec() ends

                Task delayTask = Task.Delay(1000);
                Task first = await Task.WhenAny(TwoSec(), FiveSec());
                Console.WriteLine(first.Status);
                Console.WriteLine(first == TwoSec());
            }

            public static async void WhenAnyExceptionExample()
            {
                //??? how to catch inside task exception

                try
                {
                    await TwoSecWithArrayException();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

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

        public class WhenAllException
        {
            public static void Factorial(int n)
            {
                if (n < 1)
                    throw new Exception($"{n} : число не должно быть меньше 1");
                int result = 1;
                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }

                Console.WriteLine($"Факториал числа {n} равен {result}");
            }

            public static async void FactorialAsync(int n)
            {
                try
                {
                    await Task.Run(() => Factorial(n));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public static async Task DoMultipleAsync()
            {
                Task allTasks = null;
                try
                {
                    Task t1 = Task.Run(() => Factorial(-3));
                    Task t2 = Task.Run(() => Factorial(-5));
                    Task t3 = Task.Run(() => Factorial(-10));

                    allTasks = Task.WhenAll(t1, t2, t3);
                    await allTasks;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Исключение: " + ex.Message);
                    Console.WriteLine("IsFaulted: " + allTasks.IsFaulted);
                    foreach (var inx in allTasks.Exception.InnerExceptions)
                    {
                        Console.WriteLine("Внутреннее исключение: " + inx.Message);
                    }
                }
            }
        }
    }
}