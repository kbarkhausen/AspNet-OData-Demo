using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleODataApiWithEf.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public ProductCategory Category { get; set; }

    }
}