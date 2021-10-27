using System;

namespace AddictionalFeatures
{
    public class ReferenceType
    {
        private static ref int Find(int[] arr, int value)
        {
            if (arr.Length > 0)
                for (var i = 0; i < arr.Length; i++)
                    if (arr[i] == value)
                        return ref arr[i];

            throw new IndexOutOfRangeException("число не найдено");
        }

        public static void LocalMain()
        {
            int[] arr = { 1, 2, 3, 45, 5 };
            ref var xref = ref Find(arr, 5);
            xref = 8;
            foreach (var i in arr) Console.WriteLine(i);
        }
    }
}