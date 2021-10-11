using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolExample
{
    internal class AwaitDifference
    {
        private static async void CwHelloAfter2Seconds()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Hello After 2 Seconds");
            });
            Console.WriteLine("after 2");
        }

        private static void CwHelloAfter5Seconds()
        {
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Hello After 5 Seconds");
            });
            Console.WriteLine("after5");
        }

        public static void AsyncParallelDiff()
        {
            CwHelloAfter2Seconds();
            CwHelloAfter5Seconds();
        }
    }
}