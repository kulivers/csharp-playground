using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threading
{
    public class ParallelLockedDictionaryHistogram : IHistogram
    {
        private int _maxRange;
        private object _locker = new object();
        public ParallelLockedDictionaryHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
        }

        public string ImplementationName { get { return "Parallel Locked Dictionary Histogram"; } }

        public int[] Make(int[] data)
        {
            var dictionary = new Dictionary<int, int>();
            Parallel.ForEach(data, value =>
            {
                if (dictionary.ContainsKey(value))
                {
                    lock (_locker)
                        dictionary[value]++;
                }
                else
                {
                    lock (_locker)
                        if (dictionary.ContainsKey(value))
                            dictionary[value]++;
                        else
                            dictionary.Add(value, 1);
                }
            });

            return DictionaryToInts(dictionary);
        }

        private int[] DictionaryToInts(Dictionary<int, int> values)
        {
            var result = new int[_maxRange];

            for (int i = 0; i < _maxRange; i++)
                values.TryGetValue(i, out result[i]);

            return result;
        }
    }
}
