using System.Collections.Generic;

namespace TAP.Exercises
{
    public class MySelfFillingDictionaryWithoutLocking
    {
        private Dictionary<int, int> _myDict;

        public MySelfFillingDictionaryWithoutLocking()
        {
            _myDict = new Dictionary<int, int>();
        }

        public void FillTheDict()
        {
            var rand = new ThreadSafeRandom();
            var randKey = rand.Next(1, 10);
            if (_myDict.ContainsKey(randKey))
            {
                _myDict[randKey]++;
            }
            else
            {
                _myDict.Add(randKey, 1);
            }
        }

        public void ShowSortedDict()
        {
        }
    }
}