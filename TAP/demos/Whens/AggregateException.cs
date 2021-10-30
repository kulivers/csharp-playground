using System;
using System.Linq;
using System.Threading.Tasks;

namespace TAP
{
    public class SeveralExceptionsInTask
    {
        static async Task ArrayException()
        {
            int[] arr = new int[1];
            arr[4] = 1;
        }

        static async Task DivideZeroException()
        {
            int a = 0;
            int b = 1 / a;
        }

        static async Task LinqException()
        {
            int[] arr = new[] { 1, 21, 1, 1 };
            var a = arr.Single(el => el == 1);
        }

        public static async Task WhenAllWithManyExceptions()
        {
            var task = Task.WhenAll(ArrayException(), DivideZeroException(), LinqException());
            Console.WriteLine("task.Exception: "+task.Exception.GetType());
            Console.WriteLine();
            foreach (var ex in task.Exception.InnerExceptions)
            {
                Console.WriteLine("----------------"+ex.GetType());
            }
        }
    }
}