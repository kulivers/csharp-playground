using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    class AsyncFileReadingExample
    {
        private static async Task<string> ReadTextFromTxtFileAsync(string path)
        {
            string text;
            try
            {
                using (var sr = new StreamReader(path))
                {
                    Console.WriteLine("inside before await: " + Thread.CurrentThread.ManagedThreadId);
                    text = await sr.ReadToEndAsync().ContinueWith(t =>
                    {
                        Console.WriteLine("system ReadToEndAsync() thread: " + Thread.CurrentThread.ManagedThreadId);
                        return "as";
                    });
                    Console.WriteLine("inside after await: " + Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return text;
        }

        public static async Task Do()
        {
            var fls = Directory.GetFiles(Directory.GetCurrentDirectory());
            string path;
            try
            {
                path = fls.First(fl => Path.GetFileName((string?)fl) == "NAME.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("Do before await : " + Thread.CurrentThread.ManagedThreadId);
            string txt = await ReadTextFromTxtFileAsync(path);
            Console.WriteLine("Do after  await: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}