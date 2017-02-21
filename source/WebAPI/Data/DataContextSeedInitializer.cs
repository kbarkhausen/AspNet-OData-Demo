using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SimpleODataApiWithEf.Models;

namespace SimpleODataApiWithEf.Data
{
    public class DataContextSeedInitializer : DropCreateDatabaseAlways<ProductsContext>
    {
        private int itemCounter = 0;

        protected override void Seed(ProductsContext context)
        {   
            context.Products.AddRange(GetProduce());
            context.Products.AddRange(GetBakery());
            context.Products.AddRange(GetGrocery());            
            context.SaveChanges();
        }

        private List<Product> GetProduce()
        {
            var category = new ProductCategory { Id = 1, Name = "Fresh Produce" };

            var products = new List<Product>();

            products.Add(new Product { Id = itemCounter++, Name = "Apple", Price = 0.68m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Banana", Price = 0.28m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Lettuce", Price = 2.01m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Limes", Price = 0.28m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Orange", Price = 1.08m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Pears", Price = 0.68m, Category = category });

            return products;
        }

        private List<Product> GetBakery()
        {
            var category = new ProductCategory { Id = 2, Name = "Bakery" };

            var products = new List<Product>();

            products.Add(new Product { Id = itemCounter++, Name = "Bagels", Price = 1.98m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Sandwich bread", Price = 2.98m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "French bread", Price = 5.98m, Category = category });

            return products;
        }

        private List<Product> GetGrocery()
        {
            var category = new ProductCategory { Id = 3, Name = "Grocery" };

            var products = new List<Product>();

            products.Add(new Product { Id = itemCounter++, Name = "Tuna", Price = 3.15m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Pasta", Price = 2.38m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Tortillas", Price = 0.48m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Soup", Price = 2.88m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Cocktail sauce", Price = 4.38m, Category = category });
            products.Add(new Product { Id = itemCounter++, Name = "Beans", Price = 1.3m, Category = category });

            return products;
        }

    }
}