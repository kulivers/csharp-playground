using System;
using System.Collections.Concurrent;
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


    public class MyConcurrentDictionary
    {
        public MyConcurrentDictionary()
        {
            Dict = new ConcurrentDictionary<int, int>();
        }


        public ConcurrentDictionary<int, int> Dict { get; set; }

        public void FillConcurrentDictionaryFromList(List<int> list, int threadsCount = 20)
        {
            if (list == null)
                throw new NullReferenceException("List is empty");
            try
            {
                Parallel.For((long)0, threadsCount, _ =>
                {
                    foreach (var i in list) Increment(i);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Increment(int key)
        {
            Dict.AddOrUpdate(key, 1, (k, v) => Interlocked.Increment(ref v));
        }

        public void FillTheDictRandNumbers(int count, int threadsCount = 20)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;
            Parallel.For(0, threadsCount, _ =>
            {
                if (Dict.Count >= count) return;
                var randKey = rand.Next(0, 10);
                Increment(randKey);
            });
        }

        public void FillTheDictFromJsonFile(string path, int threadsCount = 512)
        {
            if (threadsCount > 512 || threadsCount < 1)
            {
                throw new ArgumentOutOfRangeException("threadsCount is not less than 1 or greater than 512.");
            }

            var json = JArray.Parse(File.ReadAllText(path));

            json.Children().AsParallel().WithDegreeOfParallelism(threadsCount).ForAll(token =>
            {
                Increment(token.Value<int>());
            });
        }

        public void ShowSortedDict()
        {
            foreach (var pair in Dict.OrderBy(pair => pair.Key)) //no locking
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            var sum = Dict.Sum(pair => pair.Value);
            Console.WriteLine("values sum: {0}", sum);
        }
    }
}