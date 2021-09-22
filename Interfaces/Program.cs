using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Collections;


namespace Interfaces
{
    interface IFigure
    {
        private const int minArea = 0;
        public static int maxArea = 60;


        static int NumOfSides { get; set; }
        int LenOfSide { get; set; }
        double GetArea();
    }

    class EquilateralTriangle : IFigure
    {
        public EquilateralTriangle(int lenOfSide)
        {
            LenOfSide = lenOfSide;
        }

        public int SomeAdditionalField { get; set; }

        public double GetArea()
        {
            return LenOfSide * LenOfSide * Math.Sqrt(3) / 4;
        }

        public double GetPerimeter()
        {
            return LenOfSide * 3;
        }

        public int LenOfSide { get; set; }


        public static void OutputLikeEnumerable()
        {
            IEnumerable<EquilateralTriangle> squares = new[]
                { new EquilateralTriangle(1), new EquilateralTriangle(2), new EquilateralTriangle(3) };

            var enumerator = squares.GetEnumerator();

            foreach (var item in squares)
            {
                Console.WriteLine(item.LenOfSide);
            }
        }

        public static void WorkingWithIEnumerable()
        {
            IEnumerable enumerable = new[]
                { new EquilateralTriangle(1), new EquilateralTriangle(1), new EquilateralTriangle(1) };
            enumerable.GetEnumerator().Reset();
        }

        static public void LocalMain()
        {
            // OutputLikeEnumerable();
            WorkingWithIEnumerable();
            IFigure triangle = new EquilateralTriangle(1);
            // var perimeter = triangle.GetPerimeter() //ERROR
        }
    }

    interface ISaveble
    {
        void ExplicitlySave();

        void UsualSave()
        {
            Console.WriteLine("Here is default implamentaion");
        }
    }

    class ExplicitInterfaceExample : ISaveble
    {
        void ISaveble.ExplicitlySave()
        {
            Console.WriteLine("this is explicitly Save, it is belong to interface");
        }

        public void ExplicitlySave()
        {
            Console.WriteLine("this is explicitly Save, it is belong to class");
        }

        public void UsualSave()
        {
            Console.WriteLine("We didnt it, cuz we dont want method with same name" +
                              "(that's all what we want from explitily implementation)");
        }

        public static void LocalMain()
        {
            var game = new ExplicitInterfaceExample();
            game.ExplicitlySave(); //usual   
            ((ISaveble)game).ExplicitlySave(); //explicitly
            Console.Read();
        }
    }

    class OddNumerator : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            int i = 1;
            yield return i;
            while (true)
            {
                yield return i += 2;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static void LocalMain()
        {
            var generator = new OddNumerator();
            foreach (var i in generator)
            {
                if (i > 50)
                    break;
                Console.WriteLine(i);
            }
        }
    }

    class TriangleComparerClass : IComparer<EquilateralTriangle>
    {
        public int Compare(EquilateralTriangle x, EquilateralTriangle y)
        {
            if (y != null && x != null && x.LenOfSide > y.LenOfSide)
            {
                return 1;
            }

            if (x != null && y != null && x.LenOfSide < y.LenOfSide)
            {
                return -1;
            }

            return 0;
        }

        static public void LocalMain()
        {
            EquilateralTriangle triangle1 = new EquilateralTriangle(1);
            EquilateralTriangle triangle2 = new EquilateralTriangle(2);
            EquilateralTriangle triangle3 = new EquilateralTriangle(3);
            var trianglesArr = new[] { triangle2, triangle1, triangle3 };
            Array.Sort(trianglesArr, new TriangleComparerClass());
            foreach (var triangle in trianglesArr)
            {
                Console.WriteLine(triangle.LenOfSide);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EquilateralTriangle.LocalMain();
            Dictionary<int, string> countries = new Dictionary<int, string>(5);
            countries.Add(1, null);
            countries.Add(3, "Great Britain");
            countries.Add(2, "USA");
            countries.Add(4, "France");
            countries.Add(5, "China");

            foreach (KeyValuePair<int, string> keyValue in countries)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }

            // получение элемента по ключу
            var keys = countries.Keys;
            // изменение объекта
            countries[4] = "Spain";
            // удаление по ключу
            countries.Remove(2);
        }
    }
}