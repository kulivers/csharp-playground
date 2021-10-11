using System;
using System.Threading;

namespace Threads
{
    public class SimpleMutexClass
    {
        public static void LocalMain()
        {
            Mutex _mutex = new Mutex(false, "mutex_name");
            _mutex.WaitOne();
            Console.WriteLine("Press any key to realise mutex");
            Console.ReadLine();
            _mutex.ReleaseMutex();
        }
    }
}