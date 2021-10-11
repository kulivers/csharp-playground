using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project_for_testing_mutex
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex _mutex = new Mutex(false, "mutex_name");

            Parallel.For(0, 5, (i, state) =>
            {
                Thread.CurrentThread.Name = "thread_" + i;
                while (true)
                {
                    try
                    {
                        if (_mutex.WaitOne(102))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("working " + Thread.CurrentThread.Name);
                            _mutex.ReleaseMutex();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("sleeping " + Thread.CurrentThread.Name);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            });
        }
    }
}