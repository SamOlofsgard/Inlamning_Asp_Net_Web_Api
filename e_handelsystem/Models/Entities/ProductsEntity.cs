using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_handelsystem.Models.Entities
{
    [Index(nameof(BarCode), IsUnique = true)]
    public class ProductsEntity
    {
        public ProductsEntity()
        {

        }

        public ProductsEntity(string barCode, string name, string description, DateTime created, decimal price, string currency, int stock)
        {
            BarCode = barCode;
            Name = name;
            Description = description;
            Created = created;
            Price = price;
            Currency = currency;
            Stock = stock;
        }

        public ProductsEntity(string barCode, string name, string description, DateTime created, decimal price, string currency, int stock, int categoryId)
        {
            BarCode = barCode;
            Name = name;
            Description = description;
            Created = created;
            Price = price;
            Currency = currency;
            Stock = stock;
            CategoryId = categoryId;
        }

        public ProductsEntity(int id, string barCode, string name, string description, DateTime created, decimal price, string currency, int stock, int categoryId, CategoriesEntity category)
        {
            Id = id;
            BarCode = barCode;
            Name = name;
            Description = description;
            Created = created;
            Price = price;
            Currency = currency;
            Stock = stock;
            CategoryId = categoryId;
            Category = category;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string BarCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Currency { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }        

        public CategoriesEntity Category { get; set; }
    }
}
