using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQSamples
{
    public static class AggregateFunctions
    {
        public static void RunAllMethods()
        {
            Count();
            CountFiltered();
            Minimum();
            Maximum();
            Average();
            Sum();
            AggregateSum();
            AggregateCustom();
            AggregateUsingGrouping();
        }

        #region Constructor

        static AggregateFunctions()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
            // Load all Sales Data
            Sales = SalesOrderDetailRepository.GetAll();
        }

        #endregion

        #region Properties

        private static bool UseQuerySyntax { get; set; } = true;
        private static List<Product> Products { get; set; }
        private static List<SalesOrderDetail> Sales { get; set; }
        private static string ResultText { get; set; }

        #endregion

        #region Count

        /// <summary>
        /// Gets a total number of products in a collection
        /// </summary>
        private static void Count()
        {
            int value;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                    select prod).Count();
            }
            else
            {
                // Method Syntax
                value = Products.Count();
            }

            ResultText = $"Total Products = {value}";
        }

        #endregion

        #region CountFiltered

        /// <summary>
        /// You can apply a where clause, or a predicate in Count()
        /// </summary>
        private static void CountFiltered()
        {
            string search = "Red";
            int value;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                        select prod)
                    .Count(prod => prod.Color == search);

                //value = (from prod in Products
                //         where prod.Color == search
                //         select prod)
                //        .Count();
            }
            else
            {
                // Method Syntax
                value = Products.Count(prod => prod.Color == search);

                // Alternate Syntax
                //value = Products.Where(prod => prod.Color == search).Count();
            }

            ResultText = $"Total Products with a color of 'Red' = {value}";
        }

        #endregion

        #region Minimum

        /// <summary>
        /// Get the minimum value in a collection
        /// </summary>
        private static void Minimum()
        {
            decimal? value;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                    select prod.ListPrice).Min();

                // Alternate Syntax
                //value = (from prod in Products
                //         select prod)
                //        .Min(prod => prod.ListPrice);
            }
            else
            {
                // Method Syntax
                value = Products.Min(prod => prod.ListPrice);
            }

            ResultText = $"Minimum List Price = {value.Value:c}";
        }

        #endregion

        #region Maximum

        /// <summary>
        /// Get the maximum value in a collection
        /// </summary>
        private static void Maximum()
        {
            decimal? value;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                    select prod.ListPrice).Max();

                // Alternate Syntax
                //value = (from prod in Products
                //         select prod)
                //        .Max(prod => prod.ListPrice);
            }
            else
            {
                // Method Syntax
                value = Products.Max(prod => prod.ListPrice);
            }

            if (value.HasValue)
            {
                ResultText = $"Maximum List Price = {value.Value:c}";
            }
            else
            {
                ResultText = "No List Prices Exist.";
            }
        }

        #endregion

        #region Average

        /// <summary>
        /// Get the average value in a collection
        /// </summary>
        private static void Average()
        {
            decimal? value;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                    select prod.ListPrice).Average();

                // Alternate Syntax
                //value = (from prod in Products
                //         select prod)
                //        .Average(prod => prod.ListPrice);
            }
            else
            {
                // Method Syntax
                value = Products.Average(prod => prod.ListPrice);
            }

            if (value.HasValue)
            {
                ResultText = $"Average List Price = {value.Value:c}";
            }
            else
            {
                ResultText = "No List Prices Exist.";
            }
        }

        #endregion

        #region Sum

        /// <summary>
        /// Get a total value from a numeric property
        /// </summary>
        private static void Sum()
        {
            decimal? value;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                    select prod.ListPrice).Sum();

                // Alternate Syntax
                //value = (from prod in Products
                //         select prod)
                //        .Sum(prod => prod.ListPrice);
            }
            else
            {
                // Method Syntax
                value = Products.Sum(prod => prod.ListPrice);
            }

            if (value.HasValue)
            {
                ResultText = $"Total of all List Prices = {value.Value:c}";
            }
            else
            {
                ResultText = "No List Prices Exist.";
            }
        }

        #endregion

        #region AggregateSum

        /// <summary>
        /// Simulate Sum() using the Aggregate method
        /// </summary>
        private static void AggregateSum()
        {
            decimal? value = 0;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from prod in Products
                        select prod)
                    .Aggregate(0M, (sum, prod) =>
                        sum += prod.ListPrice);
            }
            else
            {
                // Method Syntax
                value = Products.Aggregate(0M,
                    (sum, prod) => sum += prod.ListPrice);
            }

            if (value.HasValue)
            {
                ResultText = $"Total of all List Prices = {value.Value:c}";
            }
            else
            {
                ResultText = "No List Prices Exist.";
            }
        }

        #endregion

        #region AggregateCustom

        /// <summary>
        /// Simulate Sum() using the Aggregate method
        /// </summary>
        private static void AggregateCustom()
        {
            decimal? value = 0;

            if (UseQuerySyntax)
            {
                // Query Syntax
                value = (from sale in Sales
                        select sale)
                    .Aggregate(0M,
                        (sum, sale) =>
                            sum += (sale.OrderQty * sale.UnitPrice));
            }
            else
            {
                // Method Syntax
                value = Sales.Aggregate(0M,
                    (sum, sale) => sum += (sale.OrderQty * sale.UnitPrice));
            }

            if (value.HasValue)
            {
                ResultText = $"Total of all List Prices = {value.Value:c}";
            }
            else
            {
                ResultText = "No List Prices Exist.";
            }
        }

        #endregion

        #region AggregateUsingGrouping

        /// <summary>
        /// Group products by Size property and calculate min/max/average prices
        /// </summary>
        private static void AggregateUsingGrouping()
        {
            StringBuilder sb = new StringBuilder(2048);

            if (UseQuerySyntax)
            {
                // Query syntax
                var stats = (from prod in Products
                    group prod by prod.Size
                    into sizeGroup
                    where sizeGroup.Count() > 0
                    select new
                    {
                        Size = sizeGroup.Key,
                        TotalProducts = sizeGroup.Count(),
                        Max = sizeGroup.Max(s => s.ListPrice),
                        Min = sizeGroup.Min(s => s.ListPrice),
                        Average = sizeGroup.Average(s => s.ListPrice)
                    }
                    into result
                    orderby result.Size
                    select result);

                // Loop through each product statistic
                foreach (var stat in stats)
                {
                    sb.AppendLine($"Size: {stat.Size}  Count: {stat.TotalProducts}");
                    sb.AppendLine($"  Min: {stat.Min:c}");
                    sb.AppendLine($"  Max: {stat.Max:c}");
                    sb.AppendLine($"  Average: {stat.Average:c}");
                }
            }
            else
            {
                // Method syntax
                var stats = Products.GroupBy(sale => sale.Size)
                    .Where(sizeGroup => sizeGroup.Count() > 0)
                    .Select(sizeGroup => new
                    {
                        Size = sizeGroup.Key,
                        TotalProducts = sizeGroup.Count(),
                        Max = sizeGroup.Max(s => s.ListPrice),
                        Min = sizeGroup.Min(s => s.ListPrice),
                        Average = sizeGroup.Average(s => s.ListPrice)
                    })
                    .OrderBy(result => result.Size)
                    .Select(result => result);

                // Loop through each product statistic
                foreach (var stat in stats)
                {
                    sb.AppendLine($"Size: {stat.Size}  Count: {stat.TotalProducts}");
                    sb.AppendLine($"  Min: {stat.Min:c}");
                    sb.AppendLine($"  Max: {stat.Max:c}");
                    sb.AppendLine($"  Average: {stat.Average:c}");
                }
            }

            ResultText = sb.ToString();
        }

        #endregion
    }
}