using System;
using System.Collections;
using System.Collections.Generic;

namespace Interfaces
{

    internal interface IFigure
    {
        private const int minArea = 0;
        public static int maxArea = 60;


        static int NumOfSides { get; set; }
        int LenOfSide { get; set; }
        double GetArea();
    }
    
    internal class EquilateralTriangle : IFigure
    
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

            foreach (var item in squares) Console.WriteLine(item.LenOfSide);
        }

        public void WorkingWithIEnumerable()
        {
            IEnumerable enumerable = new[]
                { new EquilateralTriangle(1), new EquilateralTriangle(1), new EquilateralTriangle(1) };
            enumerable.GetEnumerator().Reset();
        }

    }
}