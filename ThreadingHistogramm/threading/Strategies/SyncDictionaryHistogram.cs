using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threading
{
    public class SyncDictionaryHistogram : IHistogram
    {
        private int _maxRange;
        public SyncDictionaryHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
        }

        public string ImplementationName { get { return "Sync Dictionary Histogram"; } }

        public int[] Make(int[] data)
        {
            var dictionary = new Dictionary<int, int>();
            foreach (var value in data)
            {
                var count = 0;
                if (dictionary.TryGetValue(value, out count))
                    dictionary[value] = count + 1;
                else
                    dictionary.Add(value, 1);
            }

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
