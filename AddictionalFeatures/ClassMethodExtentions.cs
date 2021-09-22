using System;

namespace AddictionalFeatures
{
    public static class StringExtention
    {
        public static bool IsOdd(this int val)
        {
            return val / 2 * 2 != val;
        }

        static public int CharCounter(this string str, char c)
        {
            int sum = 0;
            foreach (var symbol in str)
            {
                if (symbol == c)
                {
                    sum++;
                }
            }

            return sum;
        }

        public static void LocalMain() // this method could be anywhere
        {
            var someString = "abcdefga";

            Console.WriteLine("\"asdasd\".CharCounter('a'): " + someString.CharCounter('a'));
            Console.WriteLine("IsOdd(5) | 5.IsOdd: " + 5.IsOdd());
        }
    }

    public class DisplayClass
    {
        void DisplayHello()
        {
            Console.WriteLine("Hello mf");
        }
    }

    public static class DisplayClassExtention
    {
        public static void DisplaySmile(this DisplayClass displayClass)
        {
            Console.WriteLine(":)");
        }

        
    }

    class MyClass
    {
        private DisplayClass DisplayClass = new DisplayClass();

        public MyClass()
        {
            DisplayClass.DisplaySmile();
        }
    }
}