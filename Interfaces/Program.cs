using System;

namespace Interfaces
{
    internal class Program
    {
        static void IEnumerableExample()
        {
            var peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon")
            };

            var peopleList = new People(peopleArray);
            foreach (var p in peopleArray)
                Console.WriteLine(p.firstName + " " + p.lastName);
        }

        static void IEnumerableIntExample()
        {
            var generator = new OddNumerator();
            foreach (var i in generator)
            {
                if (i > 50)
                    break;
                Console.WriteLine(i);
            }
        }

        static void CustomInterfaceExample()
        {
            var eqTriangle = new EquilateralTriangle(1);
            // OutputLikeEnumerable();
            eqTriangle.WorkingWithIEnumerable();
            IFigure triangle = new EquilateralTriangle(1);
            // var perimeter = triangle.GetPerimeter() //ERROR
        }

        static void DefaultInterfaceMethodExample()
        {
            var game = new ExplicitInterfaceExample();
            game.ExplicitlySave(); //usual   
            ((ISaveble)game).ExplicitlySave(); //explicitly
            Console.WriteLine("_____________________");
            game.UsualSave(); //same as above
            ((ISaveble)game).UsualSave();

            Console.Read();
        }

        static void IComparerExample()
        {
            var triangle1 = new EquilateralTriangle(1);
            var sameTriangle1 = new EquilateralTriangle(1);
            var triangle2 = new EquilateralTriangle(2);
            var triangle3 = new EquilateralTriangle(3);
            var trianglesArr = new[] { triangle2, triangle1, triangle3 };
            Array.Sort(trianglesArr, new TriangleComparerClass()); // first variant
            foreach (var triangle in trianglesArr) Console.WriteLine(triangle.LenOfSide);
            Console.WriteLine("_______________");
        }


        private static void Main(string[] args)
        {
        }

        public class SomeClass : IDisposable
        {
            private bool disposed = false;

            // реализация интерфейса IDisposable.
            public void Dispose()
            {
                Dispose(true);
                // подавляем финализацию
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        // Освобождаем управляемые ресурсы
                    }
                    disposed = true;
                }
            }
            ~SomeClass()
            {
                Dispose(false);
            }
        }
    }
}