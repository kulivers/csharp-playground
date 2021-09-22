using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    public class SkipTakeSumDistinctDemos
    {
        private static List<Product> Products { get; set; }

        static SkipTakeSumDistinctDemos()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
        }

        static void ShowProductList(List<Product> list)
        {
            list.ForEach(product => { Console.WriteLine(product.ToString()); });
            Console.WriteLine("\nTotal: " + list.Count);
        }

        public static void RunAllMethods()
        {
            Sum();
            Take();
            TakeWhile();
            Skip();
            SkipWhile();
            Distinct();
            RemoveAll();
        }

        private static void RemoveAll()
        {
            Products.RemoveAll(product => product.Color == "Red");
            // ShowProductList(NoRedProducts.Where(product => product.Color=="Red").ToList());
            Products = ProductRepository.GetAll();
        }


        private static void Distinct()
        {
            var distinctByColors = Products.Select(product => product.Color).Distinct().ToList();
            // distinctByColors.ForEach(Console.WriteLine);
        }

        private static void SkipWhile() //идет сначала и пропускает элементы которые true в (),
                                        //после того как фолс берет ВСЕ оставшиеся
        {
            void Test()
            {
                List<int> ints = new List<int> { 1, 2, 4, 8, 4, 2, 1 };
                IEnumerable<int>
                    result = ints.SkipWhile(theInt => theInt < 5); 
                // result.ToList().ForEach(Console.WriteLine);
            }

            Test();

            var productsPriceLower45 = Products.SkipWhile(product => product.ListPrice > 45).ToList();

            var minTenPrices = Products.OrderBy(product => product.ListPrice).Select(p => p.ListPrice).Take(10);

            // ShowProductList(productsPriceLower45);
            // Console.WriteLine("10 min prices: ");
            // minTenPrices.ToList().ForEach(Console.WriteLine);
            // Console.WriteLine("priceLower45: ");
            // productsPriceLower45.ToList().ForEach(Console.WriteLine);
        }

        private static void Skip()
        {
            var skip5 = Products.Select((product, idx) => (product.ProductID, idx)).OrderBy(id => id.ProductID)
                .Skip(5).Take(10).ToList();

            var noSkip = Products.Select((product, idx) => (product.ProductID, idx)).OrderBy(id => id.ProductID)
                .ToList();
            // skip5.ForEach(tpl => { Console.WriteLine(tpl.idx + " - " + tpl.ProductID); });
        }

        private static void TakeWhile() //question
        {
            void Test()
            {
                List<int> ints = new List<int> { 1, 2, 4, 8, 4, 2, 1 };
                IEnumerable<int> result = ints.TakeWhile(theInt => theInt < 5); //not all els
                // result.ToList().ForEach(Console.WriteLine);
            }

            Test();
            var priceLower45 = Products.TakeWhile(product => product.ListPrice < 45);
            var minTenPrices = Products.OrderBy(product => product.ListPrice).Select(p => p.ListPrice).Take(10);

            // Console.WriteLine("10 min prices: ");
            // minTenPrices.ToList().ForEach(Console.WriteLine);
            // Console.WriteLine("priceLower45 ");
            // priceLower45.ToList().ForEach(Console.WriteLine);
        }

        private static void Take()
        {
            var fiveMinPriceProducts = Products.OrderBy(product => product.ListPrice).Take(5);
            var minTenPrices = Products.OrderBy(product => product.ListPrice).Select(p => p.ListPrice).Take(10);

            // Console.WriteLine("10 min prices: ");
            // minTenPrices.ToList().ForEach(Console.WriteLine);
            // Console.WriteLine("the cheapest products: ");
            // fiveMinPriceProducts.ToList().ForEach(Console.WriteLine);
        }

        private static void Sum()
        {
            var sum = Products.Select(product => product.ListPrice).Sum();
            var sum2 = Products.Sum(product => product.ListPrice);
            // Console.WriteLine(sum);
            // Console.WriteLine(sum2);
        }
    }
}