using System;
using System.Collections.Concurrent;
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


    public class MySelfFillingConcurrentDictionary
    {
        private ConcurrentDictionary<int, int> _myConcurrentDictionary;

        public MySelfFillingConcurrentDictionary()
        {
            _myConcurrentDictionary = new ConcurrentDictionary<int, int>();
        }

        private void Increment(int key)
        {
            _myConcurrentDictionary.AddOrUpdate(key, 1, (k, v) => Interlocked.Increment(ref v));
        }

        public void FillTheDictRandNumbers(int threadsCount = 20000)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;
            Parallel.For(0, threadsCount, _ =>
            {
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
            foreach (var pair in _myConcurrentDictionary.OrderBy(pair => pair.Key)) //no locking
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            var sum = _myConcurrentDictionary.Sum(pair => pair.Value);
            Console.WriteLine("values sum: {0}", sum);
        }
    }
}