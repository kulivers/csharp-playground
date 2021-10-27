using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace TAP.Exercises
{
    public class MySelfFillingDictionaryWithoutLocking
    {
        private Dictionary<int, int> _myDict;
        public MySelfFillingDictionaryWithoutLocking()
        {
            _myDict = new Dictionary<int, int>();
        }

        public void FillTheDictRandNumbers(int iterCount)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;
            for (int i = 0; i < iterCount; i++)
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

        public void FillTheDictFromFile(string path)
        {
            var json = JArray.Parse(File.ReadAllText(path));
            foreach (var token in json)
            {
                if (_myDict.ContainsKey(token.Value<int>()))
                {
                    _myDict[token.Value<int>()]++;
                }
                else
                {
                    _myDict.Add(token.Value<int>(), 1);
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