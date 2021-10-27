using System;
using TAP.Exercises;
using TAP.Exercises.WorkingWithJSONMultiThread;

namespace TAP
{
    internal class JsonToDictionaryExamples : Program
    {
        private static string JsonPath { get; set; }

        public JsonToDictionaryExamples()
        {
            JsonMethods.WriteRandomArrayToFile(20000);
            JsonPath = @"C:\dev\c#\playground\TAP\Exercises\WorkingWithJSONMultiThread\someDictionary.json";
        }

        public static void FillingDictFromFileDifferentWays()
        {
            Console.WriteLine("single thread: {0}", TimeTracker(FillingDictionaryFromFileSingleThread));
            Console.WriteLine("lock() : {0}", TimeTracker(FillingDictionaryFromFile));
            Console.WriteLine("concDict : {0}", TimeTracker(FillingConcurrentDictionaryFromFile));
            Console.WriteLine("_________");
        }

        private static void FillingDictionaryFromFileSingleThread()
        {
            try
            {
                var example = new MySelfFillingDictionaryWithoutLocking();
                example.FillTheDictFromFile(JsonPath);
                example.ShowSortedDict();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void FillingDictionaryFromFile()
        {
            try
            {
                var example = new MySelfFillingDictionary();
                example.FillTheDictFromFile(JsonPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void FillingConcurrentDictionaryFromFile()
        {
            try
            {
                var example = new MySelfFillingConcurrentDictionary();
                example.FillTheDictFromJsonFile(JsonPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}