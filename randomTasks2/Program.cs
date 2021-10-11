using System;

namespace randomTasks2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int x = 0;
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}