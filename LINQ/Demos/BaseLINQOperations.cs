using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    public partial class BaseLINQOperationsDemos
    {
        private static List<Product> Products { get; set; }

        static BaseLINQOperationsDemos()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
        }

        static void ShowProductList(List<Product> list)
        {
            list.ForEach(product => { Console.WriteLine(product.ToString()); });
            Console.WriteLine("Total: " + list.Count);
        }

        public static void RunAllMethods()
        {
            SelectAll();
            SelectName();
            OrderBy();
            OrderByDesc();
            OrderByTwoFields();
            WhereTwoFields();
            ByColorExtention();
            First();
            FirstOrDefalt();
            Single();
            SingleOrDefault();
            ForEach();
            FieldsAssignment();
        }

        #region Select Order Where + IEnumerable<Product> extention ByColor Examples

        /// Select a single column
        static void SelectAll()
        {
            var result = (from prod in Products
                select prod).ToList();
            var queryResult = Products.Select(prod => prod).ToList();
            // ShowProductList(result);
            // ShowProductList(queryResult);
        }

        /// Select a few specific properties from products and create new Product objects
        static void SelectName()
        {
            // Query Syntax
            var res = (from prod in Products
                select new Product
                {
                    ProductID = prod.ProductID,
                    Name = prod.Name,
                    Size = prod.Size
                }).ToList();
            var query = Products.Select(prod => new Product
            {
                ProductID = prod.ProductID,
                Name = prod.Name,
                Size = prod.Size
            }).ToList();
            // ShowProductList(res);
        }

        // Order products by Name
        static void OrderBy()
        {
            var res = (from product in Products orderby product.ProductID select product).ToList();
            var query = Products.Select(prod => prod).OrderBy(prod => prod.ProductID).ToList();
            // ShowProductList(query);
        }

        static void OrderByDesc()
        {
            var res = (from product in Products orderby product.ProductID descending select product).ToList();
            var query = Products.Select(prod => prod).OrderByDescending(prod => prod.ProductID).ToList();
            // ShowProductList(res);
            // ShowProductList(query);
        }

        static void OrderByTwoFields()
        {
            var res = (from product in Products orderby product.Name, product.ProductID select product).ToList();
            var query = Products.Select(prod => prod).OrderBy(prod => prod.Name).ThenBy(product => product.ProductID)
                .ToList();
            // ShowProductList(res);
            // ShowProductList(query);
        }

        static void WhereTwoFields()
        {
            var res = (from product in Products
                orderby product.Name, product.ProductID
                where product.ProductID % 2 == 0 && product.Color == "Red"
                select product).ToList();
            var query = Products.Where(prod => prod.ProductID % 2 == 0 && prod.Color == "Red")
                .OrderBy(prod => prod.Name)
                .ThenBy(product => product.ProductID)
                .ToList();
            // ShowProductList(res);
            // ShowProductList(query);
        }

        static void ByColorExtention()
        {
            var res = (from product in Products select product).ByColor("Black").ToList();
            var query = Products.ByColor("Red").ToList();
            // ShowProductList(query);
        }

        #endregion
    }

    public static partial class ProductHelper
    {
        public static IEnumerable<Product> ByColor(this IEnumerable<Product> query, string color)
        {
            return query.Where(prod => prod.Color == color);
        }
    }


    partial class BaseLINQOperationsDemos
    {
        #region FirstLastSingle

        static void First() // Last() = same
        {
            try
            {
                var res = (from product in Products
                    where product.Color == "Silvera"
                    select product).ToList().First();
                // Console.WriteLine(res.ToString());
            }
            catch
            {
                // Console.WriteLine("There are no elements as u want");
            }
        }

        static void FirstOrDefalt() //LastOrDefault() = same
        {
            var res = (from product in Products
                where product.Color == "Silvera" // no exception
                select product).ToList().FirstOrDefault();
            if (res == null)
            {
                // Console.WriteLine("there are no element");
            }
            else
            {
                // Console.WriteLine(res.ToString());
            }
        }

        // Операция Single возвращает единственный элемент последовательности или единственный элемент последовательности,
        // соответствующий предикату — в зависимости от используемого прототипа.
        // Эта операция имеет два прототипа, описанные ниже:
        static void Single()
        {
            try
            {
                var res = (from product in Products
                    select product).Single(product => product.ProductID > 706);
                var single = Products.Single(product => product.ProductID == 706);

                // Console.WriteLine(single.ToString());
            }
            catch (Exception e)
            {
                // if (e.InnerException == ThrowMoreThanOneMatchException)//как разделить на слишком много и вообще не верувшееся
            }
        }

        static void SingleOrDefault()
        {
            //if there is no value => null
            //if there are many values => exception
            var noValueQueryResult = Products.SingleOrDefault(product => product.ProductID == 1488); //null
            try
            {
                var manyValuesResult = Products.SingleOrDefault(product => product.ProductID > 1);
            }
            catch (Exception e)
            {
                // Console.WriteLine("there are many values");
            }
        }

        #endregion
    }

    partial class BaseLINQOperationsDemos
    {
        static void ForEach()
        {
            void Display(Product product)
            {
                Console.WriteLine(product.ToString());
            }

            Action<Product> action = (Product product) => { Console.WriteLine(product.ProductID); };

            // Products.ToList().ForEach(p => { Console.WriteLine(p.ProductID); });
            // Products.ToList().ForEach(action); //same
            // Products.ToList().ForEach(Display); //same
            // Products.ToList().ForEach(p => Display(p)); //same
        }

        static void FieldsAssignment()
        {
            Products = (from product in Products
                let nameLen = product.NameLength = product.Name.Length
                select product).ToList();
            Products.ForEach(product => { product.NameLength = product.Name.Length; });
            // Products.ForEach(p => { Console.WriteLine(p.ToString()); });
        }
    }
}