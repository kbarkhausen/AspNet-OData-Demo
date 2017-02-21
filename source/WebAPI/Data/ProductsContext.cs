using System.Data.Entity;
using SimpleODataApiWithEf.Models;

namespace SimpleODataApiWithEf.Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext()
                : base("name=ProductsContext")
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
    }

}