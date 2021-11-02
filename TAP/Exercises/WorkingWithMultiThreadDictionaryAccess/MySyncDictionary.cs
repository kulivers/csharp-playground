using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace TAP.Exercises
{
    public class MySyncDictionary
    {
        private Dictionary<int, int> _myDict;
        private List<int> _list;

        public MySyncDictionary()
        {
            Dict = new Dictionary<int, int>();
        }

        public MySyncDictionary(List<int> list)
        {
            Dict = new Dictionary<int, int>();
            _list = list;
        }

        public Dictionary<int, int> Dict
        {
            get => _myDict;
            set => _myDict = value;
        }

        public void FillTheDictFromList()
        {
            if (_list== null)
                throw new NullReferenceException("List is empty");
            try
            {
                foreach (var i in _list)
                {
                    if (Dict.ContainsKey(i))
                        Dict[i]++;
                    else
                        Dict.Add(i, 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void FillTheDictRandNumbers(int count)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;
            for (var i = 0; i < count; i++)
            {
                var randKey = rand.Next(0, 10);
                if (Dict.ContainsKey(randKey))
                {
                    Dict[randKey]++;
                }
                else
                {
                    Dict.Add(randKey, 1);
                }
            }
        }

        public void FillTheDictFromFile(string path)
        {
            var json = JArray.Parse(File.ReadAllText(path));
            foreach (var token in json)
            {
                if (Dict.ContainsKey(token.Value<int>()))
                {
                    Dict[token.Value<int>()]++;
                }
                else
                {
                    Dict.Add(token.Value<int>(), 1);
                }
            }
        }

        public void ShowSortedDict()
        {
            foreach (var pair in Dict)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }
    }
}