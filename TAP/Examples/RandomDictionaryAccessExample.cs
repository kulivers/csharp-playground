using System;
using TAP.Exercises;

namespace TAP
{
    internal class RandomDictionaryAccessExample : Program
    {
        public static void DifferentWaysToAccessDicts(int iterationsCount = 20000)
        {
            Console.WriteLine("threads and iterations count: {0}", iterationsCount);
            Console.WriteLine("single thread: {0}", TimeTracker(() => SingleThreadAccessDictionaryExample(iterationsCount)));
            Console.WriteLine("lock() : {0}", TimeTracker(() => DictionaryMultiAccessExample(iterationsCount)));
            Console.WriteLine("concDict : {0}", TimeTracker(() => ConcurrentDictionaryAccessExample(iterationsCount)));
            Console.WriteLine("_________");
        }

        private static void SingleThreadAccessDictionaryExample(int iterCount)
        {
            try
            {
                var example = new MySelfFillingSyncDictionary();
                example.FillTheDictRandNumbers(iterCount);
                // example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void DictionaryMultiAccessExample(int threadsCount)
        {
            try
            {
                var example = new MySelfFillingDictionary();
                example.FillTheDictRandNumbers(threadsCount);

                // example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ConcurrentDictionaryAccessExample(int threadsCount)
        {
            try
            {
                var example = new MySelfFillingConcurrentDictionary();
                example.FillTheDictRandNumbers(threadsCount);
                // example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}