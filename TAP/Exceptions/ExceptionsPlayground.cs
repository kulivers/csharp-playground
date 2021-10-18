using System;
using System.Threading.Tasks;

namespace TAP.Exceptions
{
    static class TaskExceptionExtention
    {
        private static void HandleException(Task task)
        {
            if (task.Exception == null) return;
            Console.WriteLine(task.Exception);
            throw task.Exception;
        }

        public static void ForgetSafelyExtention(this Task task)
        {
            task.ContinueWith(HandleException);
        }

        private static void HandleException<T>(Task<T> task)
        {
            if (task.Exception == null) return;
            Console.WriteLine(task.Exception);
            throw task.Exception;
        }

        public static void ForgetSafelyExtention<T>(this Task<T> task)
        {
            task.ContinueWith(HandleException);
        }
    }

    public class ExceptionsPlayground
    {
        static async Task HandledThrower()
        {
            Task.Delay(1000);
            throw new Exception("Thrower Handled Exception");
        }

        static async void UnhandledThrowerVoid()
        {
            Task.Delay(1000);
            int a = 0;
            int b = 1 / a;
        }

        static async Task UnhandledThrower()
        {
            Task.Delay(1000);
            int a = 0;
            int b = 1 / a;
        }

        public static async void RunUnhandleThrower()
        {
            Task task = UnhandledThrower();
            // await task;//no exception

            try
            {
                await task;
                // UnhandledThrower(); // - no exception without await
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async void RunHandleThrower()
        {
            try
            {
                await HandledThrower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}