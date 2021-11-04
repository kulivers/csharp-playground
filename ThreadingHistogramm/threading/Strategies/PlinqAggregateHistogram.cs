using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threading
{
    public class PlinqArrayAggregateHistogram : IHistogram
    {
        private int _maxRange;
        private object _locker = new object();
        public PlinqArrayAggregateHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
        }

        public string ImplementationName { get { return "Plinq Aggregate Array Histogram"; } }

        public int[] Make(int[] data)
        {
            return data.AsParallel().Aggregate(
                () => new int[_maxRange]
                , (local, value) => { local[value]++; return local; }
                , (final, local) => final.Zip(local, (a, b) => a + b).ToArray()
                , final => final);
        }
    }

    public class PlinqDictionaryAggregateHistogram : IHistogram
    {
        private int _maxRange;
        private object _locker = new object();
        public PlinqDictionaryAggregateHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
        }

        public string ImplementationName { get { return "Plinq Aggregate Dictionary Histogram"; } }

        public int[] Make(int[] data)
        {
            var result = data.AsParallel().Aggregate(
                () => new Dictionary<int, int>()
                , (local, value) =>
                {
                    int oldCount;
                    if (local.TryGetValue(value, out oldCount))
                        local[value] = oldCount + 1;
                    else
                        local.Add(value, 1);

                    return local;
                }
                , (final, local) =>
                    {
                        foreach (var pair in local)
                            final[pair.Key] += pair.Value;

                        return final;
                    }
                , final => final);

            return DictionaryToInts(result);
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
