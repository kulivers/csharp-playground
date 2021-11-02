using System.Collections;
using System.Collections.Generic;

namespace Interfaces
{
    internal class OddNumerator : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            var i = 1;
            yield return i;
            while (true) yield return i += 2;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}