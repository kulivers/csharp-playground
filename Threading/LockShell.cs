using System;
using System.Threading;
using System.Threading.Channels;

namespace ThreadingDemos
{
    class Counter
    {
        private int _count = 0;

        public void Increment()
        {
            _count++;
        }

        public void Decrement()
        {
            _count--;
        }

        public int GetCounter()
        {
            return _count;
        }

        public void ShowCounter()
        {
            Console.WriteLine(_count);
        }
    }
    


    public class LockShell
    {
        private Counter _counter = new();

        private static int ThreadNumber { get; set; }

        public LockShell()
        {
            ThreadNumber = 0;
        }


        public void CreateThreadLockAndIncrementCounter()
        {
            Console.WriteLine();
            ThreadNumber++;
            lock (_counter)
            {
                var thread = new Thread(_counter.Increment)
                {
                    Name = "Thread №" + ThreadNumber
                };
                Console.WriteLine(thread.Name + " is working with counter, counter = " + _counter.GetCounter());
                _counter.Increment();
                Console.WriteLine("Sleeping..., counter: " + _counter.GetCounter());
                Thread.Sleep(3000);
            }
        }
    }
}