using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    public class ContainingElementsFunctionsDemos
    {
        private static List<Product> Products { get; set; }

        static ContainingElementsFunctionsDemos()
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
            Any();
            All();
            ContainsBaseClass();
            ContainsMyClass();
        }

        class ProductIdComparer : IEqualityComparer<Product>
        {
            public bool Equals(Product x, Product y)
            {
                if (x.ProductID == y.ProductID)
                {
                    return true;
                }

                return false;
            }


            public int GetHashCode(Product obj)
            {
                return obj.GetHashCode();
            }
        }

        private static void ContainsMyClass()
        {
            bool badContains = Products.Contains(new Product //false because of different refs
            {
                ProductID = 680,
                Name = "HL Road Frame - Black, 58",
                Color = "Black",
                StandardCost = 1059.31M,
                ListPrice = 1431.50M,
                Size = "58",
            });

            bool goodContains = Products.Contains(new Product //true
            {
                ProductID = 680,
                Name = "HL Road Frame - Black, 58",
                Color = "Black",
                StandardCost = 1059.31M,
                ListPrice = 1431.50M,
                Size = "58",
            }, new ProductIdComparer());
            // Console.WriteLine(badContains);
            // Console.WriteLine(goodContains);
        }

        private static void ContainsBaseClass()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            // Console.WriteLine(list.Contains(11));
        }


        private static void Any()
        {
            bool value = Products.Any(product => product.Color.Contains("Red"));
            // Console.WriteLine(value);
        }

        private static void All()
        {
            bool value = Products.All(product => product.Color.Contains("Red"));
            // Console.WriteLine(value);
        }
    }
}