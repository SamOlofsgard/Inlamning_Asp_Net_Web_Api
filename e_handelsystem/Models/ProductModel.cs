using e_handelsystem.Models.Entities;

namespace e_handelsystem.Models
{
    public class ProductModel
    {
        

        public ProductModel(int id, string barCode, string name, string description, DateTime created, decimal price, string currency, int stock, int categoryId, CategoriesCreateModel category)
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

        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    
        public CategoriesCreateModel Category { get; set; }
    }
}
