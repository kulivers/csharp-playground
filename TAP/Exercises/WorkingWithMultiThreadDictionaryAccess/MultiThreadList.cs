using System;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    class MultiThreadList
    {
        private int[] arr;
        private int count;

        public MultiThreadList(int count)
        {
            this.count = count;
            arr = new int[count];
            
        }

        public void FillArrayRandoms(int threadCount, int min = 0, int max = 10)
        {
            var elsInOneArr = threadCount == 2 ? count / 2 : count / (threadCount - 1);
            var elsInLastThread = count - elsInOneArr * (threadCount - 1);
            Parallel.For(0, threadCount, i =>
            {
                var firstI = i * elsInOneArr;
                var rand = new ThreadLocal<Random>(() => new Random()).Value;

                if (i == threadCount - 1)
                {
                    var lastI = i * elsInOneArr + elsInLastThread;
                    for (int j = firstI; j < lastI; j++)
                    {
                        arr[j] = rand.Next(min, max);
                    }
                }
                else
                {
                    var lastI = i * elsInOneArr + elsInOneArr;

                    for (int j = firstI; j < lastI; j++)
                    {
                        arr[j] = rand.Next(min, max);
                    }
                }
            });
        }

        public void Show()
        {
            foreach (var o in arr)
            {
                Console.WriteLine(o);
            }
        }
    }
}