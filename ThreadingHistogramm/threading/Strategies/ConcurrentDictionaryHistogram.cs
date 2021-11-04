using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace threading
{

    public class ConcurrentDictionaryHistogram : IHistogram
    {
        private int _maxRange;
        public ConcurrentDictionaryHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
        }

        public string ImplementationName { get { return "Concurrent Dictionary Histogram"; } }

        public int[] Make(int[] data)
        {
            var dictionary = new ConcurrentDictionary<int, int>();
            Parallel.ForEach(data
                , value => dictionary.AddOrUpdate(value, 1, (key, oldValue) => oldValue + 1));

            return DictionaryToInts(dictionary);
        }

        private int[] DictionaryToInts(ConcurrentDictionary<int, int> values)
        {
            var result = new int[_maxRange];

            for (int i = 0; i < _maxRange; i++)
                values.TryGetValue(i, out result[i]);

            return result;
        }
    }
}
