using System;
using System.Linq;

namespace AddictionalFeatures
{
    public class ReferenceType
    {
        static ref int Find(int[] arr, int value)
        {
            if (arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == value)
                    {
                        return ref arr[i];
                    }
                }
            }

            throw new IndexOutOfRangeException("число не найдено");
        }

        public static void LocalMain()
        {
            int[] arr = { 1, 2, 3, 45, 5 };
            ref int xref = ref Find(arr, 5);
            xref = 8;
            foreach (var i in arr)
            {
                Console.WriteLine(i);
            }
        }
    }
}