using System;
using System.Threading.Tasks;

namespace TAP.Exceptions
{
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