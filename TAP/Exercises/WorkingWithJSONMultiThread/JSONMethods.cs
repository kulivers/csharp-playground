using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace TAP.Exercises.WorkingWithJSONMultiThread
{
    public static class JsonMethods
    {
        public static void WriteRandomArrayToFile(int elementsCount)
        {
            var rand = new ThreadLocal<Random>(() => new Random()).Value;
            var arr = new List<int>();
            for (var i = 0; i < elementsCount; i++) arr.Add(rand.Next(0, 9));

            var json = JsonConvert.SerializeObject(arr);
            File.WriteAllText(@"C:\dev\c#\playground\TAP\Exercises\WorkingWithJSONMultiThread\someDictionary.json",
                json);
        }
    }
}