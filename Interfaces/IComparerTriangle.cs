using System.Collections.Generic;

namespace Interfaces
{
    internal class TriangleComparerClass : IComparer<EquilateralTriangle>
    {
        public int Compare(EquilateralTriangle x, EquilateralTriangle y)
        {
            if (y != null && x != null && x.LenOfSide > y.LenOfSide) return 1;

            if (x != null && y != null && x.LenOfSide < y.LenOfSide) return -1;

            return 0;
        }
    }
}