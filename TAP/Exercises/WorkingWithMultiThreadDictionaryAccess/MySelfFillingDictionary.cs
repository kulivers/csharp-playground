using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void FillTheDict()
        {
            var rand = new ThreadSafeRandom();
            Parallel.For(0, 20, _ =>
            {
                var randKey = rand.Next(1, 10);
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