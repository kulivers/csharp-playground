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


    public class MyDictionary
    {
        private Dictionary<int, int> _myDict;


        private readonly object _locker;

        public Dictionary<int, int> Dict
        {
            get => _myDict;
            set => _myDict = value;
        }

        public MyDictionary()
        {
            _locker = new object();
            Dict = new Dictionary<int, int>();
        }
        
        public void FillTheDictFromListWithLock(List<int> list, int threadsCount = 20)
        {
            if (list == null)
            {
                throw new NullReferenceException("List is empty");
            }

            try
            {
                Parallel.For((long)0, threadsCount, _ =>
                {
                    foreach (var i in list)
                    {
                        lock (_locker)
                        {
                            if (Dict.ContainsKey(i))
                            {
                                Dict[i]++;
                            }
                            else
                            {
                                Dict.Add(i, 1);
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void FillTheDictRandNumbersWithLock(int count, int threadsCount = 20)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;

            Parallel.For(0, threadsCount, _ =>
            {
                var randKey = rand.Next(0, 10);
                lock (_locker)
                {
                    if (Dict.Count >= count) return;
                    if (Dict.ContainsKey(randKey))
                    {
                        Dict[randKey]++;
                    }
                    else
                    {
                        Dict.Add(randKey, 1);
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
                if (!Dict.ContainsKey(token.Value<int>()))
                {
                    lock (_locker)
                    {
                        if (!Dict.ContainsKey(token.Value<int>()))
                            Dict.Add(token.Value<int>(), 1);
                    }
                }
                else
                {
                    lock (_locker)
                    {
                        Dict[token.Value<int>()]++;
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
                foreach (var pair in Dict.OrderBy(pair => pair.Key))
                {
                    Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
                    sum++;
                }

                Console.WriteLine("values sum: {0}", sum);
            }
        }
    }
}