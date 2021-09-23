using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    public static class DefferedAndImmediateTypes
    {
        static DefferedAndImmediateTypes()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
        }

        private static List<Product> Products { get; set; }

        const string COLOR = "Red";

        public static void RunAllMethods()
        {
            DeferredExecutionExample();
            FilterUsingSimpleGenericFilterTake();
            FilterUsingWhereTake();
            FilterUsingSimpleGenericFilter();
            UsingYield();
            UsingOrderBy();
            UsingYieldAndTake();
        }

        /// Illustrate the concept of LINQ deferred execution
        private static void DeferredExecutionExample()
        {
            IEnumerable<Product> query;

            System.Diagnostics.Debugger.Break();

            // Create LINQ query
            // Add .ToList() to see immediate execution
            query = Products.Where(prod => prod.Color == COLOR);


            // Query is executed here
            // foreach (Product product in query)
            // {
            //     Console.WriteLine(product);
            // }

            // The following code is equivalent to the foreach()
            //IEnumerator<Product> enumerator = query.GetEnumerator();
            //while (enumerator.MoveNext()) {
            //  Console.WriteLine(enumerator.Current);
            //}
        }

        /// Create a generic filter to illustrate how NOT to write a filter
        private static void FilterUsingSimpleGenericFilter()
        {
            IEnumerable<Product> query;

            System.Diagnostics.Debugger.Break();

            // Create LINQ query
            query = Products.BadSimpleFilter(prod => prod.Color == COLOR);

            foreach (Product product in query)
            {
                // Console.WriteLine(product);
            }
        }

        /// Use the Where() expression with Take(1) and you see very different results from using the simple generic filter
        private static void FilterUsingWhereTake()
        {
            IEnumerable<Product> query;

            System.Diagnostics.Debugger.Break();

            // Create LINQ query
            query = Products.Where(prod => prod.Color == COLOR).Take(1);

            foreach (Product product in query)
            {
                // Console.WriteLine(product);
            }
        }

        /// Add Take(1) to simple generic filter and compare to using Where() and Take(1)
        private static void FilterUsingSimpleGenericFilterTake()
        {
            IEnumerable<Product> query;

            // System.Diagnostics.Debugger.Break();

            // Create LINQ query
            query = Products.BadSimpleFilter(prod => prod.Color == COLOR).Take(1);

            foreach (Product product in query)
            {
                // Console.WriteLine(product);
            }
        }

        /// The correct way to write a generic filter is to use the 'yield' keyword
        private static void UsingYield()
        {
            IEnumerable<Product> query;

            System.Diagnostics.Debugger.Break();

            // Create LINQ query
            query = Products.Filter(prod => prod.Color == COLOR);

            foreach (Product product in query)
            {
                // Console.WriteLine(product);
            }
        }

        /// Apply the Take(1) expression to your custom filter using 'yield'
        private static void UsingYieldAndTake()
        {
            IEnumerable<Product> query;

            System.Diagnostics.Debugger.Break();

            // Create LINQ query
            query = Products.Filter(prod => prod.Color == COLOR).Take(1);

            foreach (Product product in query)
            {
                // Console.WriteLine(product);
            }
        }

        /// Adding the OrderBy() forces one complete iteration through collection to sort the data before the filtering can happen
        private static void UsingOrderBy()
        {
            IEnumerable<Product> query;

            // Create LINQ query
            query = Products.Filter(prod => prod.Color == COLOR)
                .OrderBy(prod => prod.Name);

            // Using .Where() produces the same result
            // query = Products.Where(prod => prod.Color == search)
            //                 .OrderBy(prod => prod.Name);

            foreach (Product product in query)
            {
                // Console.WriteLine(product);
            }
        }
    }
}