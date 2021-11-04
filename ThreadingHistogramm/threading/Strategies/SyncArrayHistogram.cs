using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threading
{
    public class SyncArrayHistogram : IHistogram
    {
        private int _maxRange;
        public SyncArrayHistogram(int maxRange = 10)
        {
            _maxRange = maxRange;
        }

        public string ImplementationName { get { return "Sync Array Histogram"; } }

        public int[] Make(int[] data)
        {
            var result = new int[_maxRange];
            foreach (var value in data)
                result[value]++;

            return result;
        }
    }
}
