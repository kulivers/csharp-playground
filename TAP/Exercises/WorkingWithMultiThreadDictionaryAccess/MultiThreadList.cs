using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    class MultiThreadList
    {
        private List<int> lsInts;


        private int _min = 0;
        private int _max = 10;

        public MultiThreadList(int min = 0, int max = 10)
        {
            lsInts = new List<int>();
            _min = min;
            _max = max;
        }


        Task<List<int>> GetRandomArrayTask(int elsCount, int min, int max)
        {
            return new Task<List<int>>(() =>
            {
                var randomArray = new List<int>();
                var rand = new ThreadLocal<Random>(() => new Random()).Value;
                for (var i = 0; i < elsCount; i++)
                {
                    randomArray.Add(rand.Next(min, max));
                }

                return randomArray;
            });
        }

        void AddRangeToList(IEnumerable<int> range)
        {
            lsInts.AddRange(range);
        }

        public Dictionary<int, int> GetDictionaryOfIntsCount()
        {
            var obj = new object();
            try
            {
                return lsInts.AsParallel().Aggregate(new Dictionary<int, int>(),
                    (seed, itemInThread) =>
                    {
                        lock (obj)
                        {
                            if (!seed.ContainsKey(itemInThread))
                            {
                                if (!seed.ContainsKey(itemInThread))
                                    seed.Add(itemInThread, 1);
                            }
                            else
                            {
                                lock (obj)
                                {
                                    seed[itemInThread]++;
                                }
                            }
                        }

                        return seed;
                    },
                    (a, b) =>
                    {   
                        var resDict = new Dictionary<int, int>();

                        lock (obj)
                        {
                            foreach (var (key, value) in a)
                            {
                                if (!resDict.ContainsKey(key))
                                {
                                    if (!resDict.ContainsKey(key))
                                        resDict.Add(key, value);
                                }
                                else
                                {
                                    resDict[key] += value;
                                }
                            }

                            foreach (var (key, value) in b)
                            {
                                if (!resDict.ContainsKey(key))
                                {
                                    lock (obj)
                                    {
                                        if (!resDict.ContainsKey(key))
                                            resDict.Add(key, value);
                                    }
                                }
                                else
                                {
                                    lock (obj)
                                    {
                                        resDict[key] += value;
                                    }
                                }
                            }
                        }


                        return resDict;
                    }, //from all 
                    res => res); //result
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task FillArrayRandomsByMakingNArrays(int count, int nThreadCount)
        {
            try
            {
                var obj = new object();
                var elsCountInOneArr = nThreadCount == 2 ? count / 2 : count / (nThreadCount - 1);
                var elsCountInLastThread = count - elsCountInOneArr * (nThreadCount - 1);
                var addingTasks = new List<Task>();
                for (var i = 0; i < nThreadCount; i++)
                {
                    if (i != nThreadCount - 1)
                    {
                        var createArrayTask = GetRandomArrayTask(elsCountInOneArr, _min, _max);
                        var addArrayToList = createArrayTask.ContinueWith(task =>
                        {
                            lock (obj)
                            {
                                AddRangeToList(task.Result);
                            }
                        });
                        addingTasks.Add(addArrayToList);
                        createArrayTask.Start();
                    }
                    else
                    {
                        var createArrayTask = GetRandomArrayTask(elsCountInLastThread, _min, _max);
                        var addArrayToList = createArrayTask.ContinueWith(task =>
                        {
                            lock (obj)
                            {
                                AddRangeToList(task.Result);
                            }
                        });
                        addingTasks.Add(addArrayToList);
                        createArrayTask.Start();
                    }
                }

                await Task.WhenAll(addingTasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // public void FillArrayRandoms(int threadCount, int min = 0, int max = 10)
        // {
        //     arr = new int[count];
        //     var elsInOneArr = threadCount == 2 ? count / 2 : count / (threadCount - 1);
        //     var elsInLastThread = count - elsInOneArr * (threadCount - 1);
        //     Parallel.For(0, threadCount, i =>
        //     {
        //         var firstI = i * elsInOneArr;
        //         var rand = new ThreadLocal<Random>(() => new Random()).Value;
        //
        //         if (i == threadCount - 1)
        //         {
        //             var lastI = i * elsInOneArr + elsInLastThread;
        //             for (int j = firstI; j < lastI; j++)
        //             {
        //                 arr[j] = rand.Next(min, max);
        //             }
        //         }
        //         else
        //         {
        //             var lastI = i * elsInOneArr + elsInOneArr;
        //
        //             for (int j = firstI; j < lastI; j++)
        //             {
        //                 arr[j] = rand.Next(min, max);
        //             }
        //         }
        //     });
        // }

        public void Show()
        {
            foreach (var o in lsInts)
            {
                Console.WriteLine(o);
            }
        }
    }
}