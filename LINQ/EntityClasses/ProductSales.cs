using System.Collections.Generic;

namespace LINQSamples
{
  public partial class ProductSales
  {
    public Product Product { get; set; }
    public IEnumerable<SalesOrderDetail> Sales { get; set; }
  }
}
