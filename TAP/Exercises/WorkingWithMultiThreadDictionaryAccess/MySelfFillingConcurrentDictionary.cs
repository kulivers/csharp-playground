using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

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

        public void FillTheDict()
        {
            var rand = new Random();
            Parallel.For(0, 20, _ =>
            {
                var randKey = rand.Next(0, 10);
                _myConcurrentDictionary.AddOrUpdate(randKey, 1, (k, v) => ++v);
            });
        }

        public void ShowSortedDict()
        {
            foreach (var pair in _myConcurrentDictionary.OrderBy(pair => pair.Key))//no locking
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            var sum = _myConcurrentDictionary.Sum(pair => pair.Value);
            Console.WriteLine("values sum: {0}", sum);
        }
    }
}