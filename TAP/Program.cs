using System;

namespace ThreadPoolExample
{
    class Program
    {
        static void DiffWithAwaitAndWithout()
        {
            AwaitDifference.AsyncParallelDiff();
            Console.ReadLine();
        }

        static void Main()
        {
        }
    }
}