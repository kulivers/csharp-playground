using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LINQSamples
{
    public class CompareAndUnionTwoCollections
    {
        private static List<Product> Products { get; set; }
        private static List<SalesOrderDetail> Sales { get; set; }

        static CompareAndUnionTwoCollections()
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
            SequenceEqualInts();
            SequenceEqualProducts();
            Except();
            Intersect();
            Union();
            Concat();
            InnerJoin();
            InnerJoinWithTwoFields();
            GroupJoin();
            LeftOuterJoin();
        }

        private static void LeftOuterJoin()
        {
            StringBuilder sb = new StringBuilder(2048);
            var query = (from prod in Products
                join sale in Sales on prod.ProductID equals sale.ProductID
                    into productSales
                from sale in productSales.DefaultIfEmpty()
                select new
                {
                    prod.ProductID,
                    prod.Name,
                    prod.Color,
                    prod.StandardCost,
                    prod.ListPrice,
                    prod.Size,
                    sale?.SalesOrderID,
                    sale?.OrderQty,
                    sale?.UnitPrice,
                    sale?.LineTotal
                }).OrderBy(ps => ps.Name);

            foreach (var item in query)
            {
                sb.AppendLine($"Product Name: {item.Name} ({item.ProductID})");
                sb.AppendLine($"  Order ID: {item.SalesOrderID}");
                sb.AppendLine($"  Size: {item.Size}");
                sb.AppendLine($"  Order Qty: {item.OrderQty}");
                sb.AppendLine($"  Total: {item.LineTotal:c}");
            }

            // Console.WriteLine(sb);
        }

        private static void GroupJoin() //one to many
        {
            IEnumerable<ProductSales> productSales;
            productSales = from prod in Products
                join sale in Sales on prod.ProductID equals sale.ProductID // simmple inner join
                    into sales //we put this join into sales
                select new ProductSales { Product = prod, Sales = sales }; //so now create new obj with Sales field

            void Display()
            {
                foreach (var productSale in productSales)
                {
                    Console.Write(productSale.Product.Name + ": ");
                    if (productSale.Sales.Count() > 0)
                    {
                        foreach (var sale in productSale.Sales)
                        {
                            Console.Write($"     LineTotal: " + sale.LineTotal + " UnitPrice: " + sale.UnitPrice +
                                          '\n');
                        }

                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("No sales");
                        Console.WriteLine();
                    }
                }
            }
            // Display();
        }

        private static void InnerJoinWithTwoFields()
        {
            short qty = 6;
            var query = (from prod in Products
                join sale in Sales on
                    new { prod.ProductID, Qty = qty }
                    equals
                    new { sale.ProductID, Qty = sale.OrderQty }
                select new
                {
                    prod.ProductID,
                    SaleProdId = sale.ProductID,
                    prod.Name,
                    prod.Color,
                    prod.StandardCost,
                    prod.ListPrice,
                    prod.Size,
                    sale.SalesOrderID,
                    sale.OrderQty,
                    sale.UnitPrice,
                    sale.LineTotal
                });
            var res = (Products.Join(Sales, prod => new { prod.ProductID, Qty = qty },
                sale => new { sale.ProductID, Qty = sale.OrderQty },
                (prod, sale) => new
                {
                    prod.ProductID,
                    prod.Name,
                    prod.Color,
                    prod.StandardCost,
                    prod.ListPrice,
                    prod.Size,
                    sale.SalesOrderID,
                    sale.OrderQty,
                    sale.UnitPrice,
                    sale.LineTotal
                }));
            // res.ToList().ForEach(Console.WriteLine);
        }

        private static void InnerJoin()
        {
            var res = from product in Products
                join sale in Sales
                    on product.ProductID equals sale.ProductID
                select new
                {
                    PID = product.ProductID, SID = sale.ProductID, product.Name, sale.UnitPrice, sale?.SalesOrderID,
                };
            var query = Products.Join(Sales, product => product.ProductID, sale => sale.ProductID,
                (product, sale) => new { product.ProductID, product.Name, sale.UnitPrice });
            // query.ToList().ForEach(Console.WriteLine);
        }

        private static void Concat()
        {
            //Dubs
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6, 11 };
            List<int> concats = list1.Concat(list2).ToList();
            // concats.ForEach(Console.WriteLine); 
        }

        private static void Union()
        {
            //no Dubs
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6, 11 };
            List<int> concats = list1.Union(list2).ToList();
            // concats.ForEach(Console.WriteLine);
        }

        private static void Intersect()
        {
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6 };
            List<int> list3 = new List<int> { 11 };
            List<int> intersections = list1.Intersect(list2).ToList();
            List<int> intersections2 = list2.Intersect(list3).ToList();
            // intersections.ForEach(Console.WriteLine); 
            // intersections2.ForEach(Console.WriteLine); //NO EXCEPTION
        }

        private static void Except()
        {
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 4, 5, 6 };
            List<int> exceptions = list1.Except(list2).ToList();
            List<int> exceptions2 = list2.Except(list1).ToList();
            // exceptions.ForEach(Console.WriteLine); //1,2,3
            // exceptions2.ForEach(Console.WriteLine); //NO EXCEPTION
        }

        private static void SequenceEqualInts()
        {
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5, 6 };
            var val = list1.SequenceEqual(list2);
            // Console.WriteLine(val);
        }

        class ProductComparer : IEqualityComparer<Product>
        {
            public bool Equals(Product x, Product y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.ProductID == y.ProductID && x.Name == y.Name && x.Color == y.Color &&
                       x.StandardCost == y.StandardCost && x.ListPrice == y.ListPrice && x.Size == y.Size &&
                       x.NameLength == y.NameLength && x.TotalSales == y.TotalSales;
            }

            public int GetHashCode(Product obj)
            {
                return HashCode.Combine(obj.ProductID, obj.Name, obj.Color, obj.StandardCost, obj.ListPrice, obj.Size,
                    obj.NameLength, obj.TotalSales);
            }
        }

        static void SequenceEqualProducts()
        {
            var list1 = new List<Product>
            {
                new Product
                {
                    ProductID = 680,
                    Name = "HL Road Frame - Black, 58",
                    Color = "Black",
                    StandardCost = 1059.31M,
                    ListPrice = 1431.50M,
                    Size = "58",
                },
                new Product
                {
                    ProductID = 706,
                    Name = "HL Road Frame - Red, 58",
                    Color = "Red",
                    StandardCost = 1059.31M,
                    ListPrice = 1431.50M,
                    Size = "58",
                },
                new Product
                {
                    ProductID = 707,
                    Name = "Sport-100 Helmet, Red",
                    Color = "Red",
                    StandardCost = 13.08M,
                    ListPrice = 34.99M,
                    Size = null,
                }
            };
            var list2 = new List<Product>
            {
                new Product
                {
                    ProductID = 680,
                    Name = "HL Road Frame - Black, 58",
                    Color = "Black",
                    StandardCost = 1059.31M,
                    ListPrice = 1431.50M,
                    Size = "58",
                },
                new Product
                {
                    ProductID = 706,
                    Name = "HL Road Frame - Red, 58",
                    Color = "Red",
                    StandardCost = 1059.31M,
                    ListPrice = 1431.50M,
                    Size = "58",
                },
                new Product
                {
                    ProductID = 707,
                    Name = "Sport-100 Helmet, Red",
                    Color = "Red",
                    StandardCost = 13.08M,
                    ListPrice = 34.99M,
                    Size = null,
                }
            };
            var val = list1.SequenceEqual(list2, new ProductComparer());
            // Console.WriteLine(val);
        }
    }
}