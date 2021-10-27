using System;

namespace AddictionalFeatures
{
    internal class LazyExample
    {
        private class Reader
        {
            private Lazy<Library> library = new();

            public void ReadBook()
            {
                library.Value.GetBook();
                Console.WriteLine("Читаем бумажную книгу");
            }

            public void ReadEbook()
            {
                Console.WriteLine("Читаем книгу на компьютере");
            }
        }

        private class Library
        {
            private string[] _books = new string[99];

            public void GetBook()
            {
                Console.WriteLine("Выдаем книгу читателю");
            }
        }

        public static void LocalMain()
        {
            var reader = new Reader();
            reader.ReadBook();
        }
    }

    internal class SpanExample
    {
        public void LocalMain()
        {
            int[] temperatures = new int[]
            {
                10, 12, 13, 14, 15, 11, 13, 15, 16, 17,
                18, 16, 15, 16, 17, 14, 9, 8, 10, 11,
                12, 14, 15, 15, 16, 15, 13, 12, 12, 11
            };
            int[] firstDecade = new int[10]; // выделяем память для первой декады
            int[] lastDecade = new int[10]; // выделяем память для второй декады
            Array.Copy(temperatures, 0, firstDecade, 0, 10); // копируем данные в первый массив
            Array.Copy(temperatures, 20, lastDecade, 0, 10); // копируем данные во второй массив

            //------------------------------------------

            Span<int> temperaturesSpan = temperatures;
            var firstDecade2 = temperaturesSpan.Slice(0, 10); // нет выделения памяти под данные
            var lastDecade2 = temperaturesSpan.Slice(20, 10); // нет выделения памяти под данные
            temperaturesSpan[0] = 25; // меняем в temperatureSpan
            Console.WriteLine(firstDecade[0]); //25
        }
    }
}