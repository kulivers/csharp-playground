using System;
using System.Threading;

namespace Threads
{
    public class GettingCoinsThreads
    {
        private int _bank = 0;
        private Barrier barrier;

        public GettingCoinsThreads()
        {
            barrier = new Barrier(3);
        }

        private void FirstThreadGettingMoney()
        {
            Console.WriteLine("first thread starts getting money 2 seconds");
            Thread.Sleep(2000);
            _bank++;
            Console.WriteLine("+1 coin from 1 thread");
            barrier.SignalAndWait();
        }

        private void SecondThreadGettingMoney()
        {
            Console.WriteLine("second thread starts getting money for 6 seconds");
            Thread.Sleep(6000);
            _bank++;
            Console.WriteLine("+1 coin from 2 thread");
            barrier.SignalAndWait();
        }

        public void GetSomeMoney()
        {
            new Thread(FirstThreadGettingMoney).Start();
            new Thread(SecondThreadGettingMoney).Start();
            barrier.SignalAndWait(); //from main thread
            Console.WriteLine("\nSo all threads is done and we got: {0} coins", _bank);
        }
    }
}