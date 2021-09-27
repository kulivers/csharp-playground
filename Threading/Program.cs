using System;
using System.Diagnostics;
using System.IO;
using ThreadingDemos;

namespace Threads
{
    internal static class Program
    {
        private static void SemaphoreExample()
        {
            Threads.SemaphoreExample.LocalMain();
        }

        private static void MonitorLockExample()
        {
            var lockShell = new LockShell();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
            lockShell.CreateThreadLockAndIncrementCounter();
        }

        private static void MutexExample()
        {
            Console.WriteLine("now run dont press key and\"Project for testing mutex\"");
            SimpleMutexClass.LocalMain();
        }

        private static void Main()
        {
            MutexExample();
        }
    }
}