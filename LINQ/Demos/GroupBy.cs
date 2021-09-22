using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQSamples
{
    public class GroupingData
    {
        private static List<Product> Products { get; set; }
        private static List<SalesOrderDetail> Sales { get; set; }

        static GroupingData()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
            Sales = SalesOrderDetailRepository.GetAll();
        }

        static void ShowProductList(List<Product> list)
        {
            list.ForEach(product => { Console.WriteLine(product.ToString()); });
            Console.WriteLine("\nTotal: " + list.Count);
        }

        public static void RunAllMethods()
        {
            GroupBy();
            HavingEmulating();
            NewObjFromGroupBy();
        }

        private static void GroupBy()
        {
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            sizeGroup = from product in Products
                orderby product.Size
                group product by product.Size;

            // Display();
            void Display()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var group in sizeGroup)
                {
                    // The value in the 'Key' property is 
                    // whatever data you grouped upon
                    sb.AppendLine($"Size: {group.Key}  Count: {group.Count()}");

                    // Loop through the products in each size
                    foreach (var prod in group)
                    {
                        sb.Append($"  ProductID: {prod.ProductID}");
                        sb.Append($"  Name: {prod.Name}");
                        sb.AppendLine($"  Color: {prod.Color}");
                    }
                }

                Console.WriteLine(sb);
            }
        }

        private static void HavingEmulating()
        {
            var res = from prod in Products
                group prod by prod.Size
                into prodSizes
                where (prodSizes.Key != "M" && prodSizes.Key != "S")
                select prodSizes;

            // Display();
            void Display()
            {
                var sb = new StringBuilder();
                foreach (var group in res)
                {
                    // The value in the 'Key' property is 
                    // whatever data you grouped upon
                    sb.AppendLine($"Size: {group.Key}  Count: {group.Count()}");

                    // Loop through the products in each size
                    foreach (var prod in group)
                    {
                        sb.Append($"  ProductID: {prod.ProductID}");
                        sb.Append($"  Name: {prod.Name}");
                        sb.AppendLine($"  Color: {prod.Color}");
                    }
                }

                Console.WriteLine(sb);
            }
        }

        static void NewObjFromGroupBy()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<SaleProducts> salesGroup;

            // Get all products for a sales order id

            // Query syntax
            salesGroup = (from sale in Sales
                group sale by sale.SalesOrderID
                into sales
                select new SaleProducts
                {
                    SalesOrderID = sales.Key,
                    Products = (from prod in Products
                        join sale in Sales on prod.ProductID equals sale.ProductID
                        where sale.SalesOrderID == sales.Key
                        select prod).ToList()
                });

            // Display();
            void Display()
            {
                foreach (var sale in salesGroup)
                {
                    sb.AppendLine($"Sales ID: {sale.SalesOrderID}");

                    if (sale.Products.Count > 0)
                    {
                        // Loop through the products in each sale
                        foreach (var prod in sale.Products)
                        {
                            sb.Append($"  ProductID: {prod.ProductID}");
                            sb.Append($"  Name: {prod.Name}");
                            sb.AppendLine($"  Color: {prod.Color}");
                        }
                    }
                    else
                    {
                        sb.AppendLine("   Product ID not found for this sale.");
                    }
                }

                Console.WriteLine(sb);
            }
        }
    }
}