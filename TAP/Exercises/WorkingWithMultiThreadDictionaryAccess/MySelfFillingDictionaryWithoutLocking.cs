using System;
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
            var rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                var randKey = rand.Next(0, 10);
                if (_myDict.ContainsKey(randKey))
                {
                    _myDict[randKey]++;
                }
                else
                {
                    _myDict.Add(randKey, 1);
                }
            }
        }

        public void ShowSortedDict()
        {
            foreach (var pair in _myDict)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }
    }
}