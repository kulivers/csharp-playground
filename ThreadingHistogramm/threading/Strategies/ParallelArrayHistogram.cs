using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threading
{
    public class ParallelArrayHistogram : IHistogram
    {
        private int _maxRange;
        private volatile int[] _counts;

        public ParallelArrayHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
            _counts = new int[_maxRange];
        }

        public string ImplementationName { get { return "Parallel Array Histogram"; } }

        public int[] Make(int[] data)
        {
            var _counts = new int[_maxRange];
            Parallel.ForEach(data, value =>
            {
                Interlocked.Increment(ref _counts[value]);
            });

            return _counts;
        }
    }
}
