using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TAP.Exercises
{
    // всм есть пустой dict
    // у нас поток нарандомил число 5
    // тогда он записывает dict.Add(5,1)
    // потом если другой нарандомил 5 то уже изменяет, ы?
    // 1-Dict+lock 2-Concurent 3-noLocks

    //MemoryCache
    //MultiReadSingleWrite	
    // readmanywritesingle lock
    

    public class MySelfFillingDictionary
    {
        private Dictionary<int, int> _myDict;

        public MySelfFillingDictionary()
        {
            _locker = new object();
            _myDict = new Dictionary<int, int>();
        }


        private readonly object _locker;

        public void FillTheDictRandNumbers(int threadsCount = 20000)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;

            Parallel.For(0, threadsCount, _ =>
            {
                var randKey = rand.Next(0, 10);
                lock (_locker)
                {
                    if (_myDict.ContainsKey(randKey))
                    {
                        _myDict[randKey]++;
                    }
                    else
                    {
                        _myDict.Add(randKey, 1);
                    }
                }
            });
        }

        public void FillTheDictFromFile(string path, int threadsCount = 512)
        {
            if (threadsCount > 512 || threadsCount < 1)
            {
                throw new ArgumentOutOfRangeException("threadsCount is not less than 1 or greater than 512.");
            }

            var json = JArray.Parse(File.ReadAllText(path));

            json.Children().AsParallel().WithDegreeOfParallelism(threadsCount).ForAll(token =>
            {
                if (!_myDict.ContainsKey(token.Value<int>()))
                {
                    lock (_locker)
                    {
                        if (!_myDict.ContainsKey(token.Value<int>()))
                            _myDict.Add(token.Value<int>(), 1);
                    }
                }
                else
                {   
                    lock (_locker)
                    {
                        _myDict[token.Value<int>()]++;
                    }
                }
                // lock (_locker)
                // {
                //     if (_myDict.ContainsKey(token.Value<int>()))
                //     {
                //         _myDict[token.Value<int>()]++;
                //     }
                //     else
                //     {
                //         _myDict.Add(token.Value<int>(), 1);
                //     }
                // }
            });
        }

        public void ShowSortedDict()
        {
            lock (_locker)
            {
                int sum = 0;
                foreach (var pair in _myDict.OrderBy(pair => pair.Key))
                {
                    Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
                    sum++;
                }

                Console.WriteLine("values sum: {0}", sum);
            }
        }
    }
}