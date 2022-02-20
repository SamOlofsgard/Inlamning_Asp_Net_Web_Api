using e_handelsystem.Models.Entities;

namespace e_handelsystem.Models
{
    public class ProductCreateModel
    {
        public ProductCreateModel(string barCode, string name, string description, decimal price, string currency, int stock, CategoriesCreateModel category)
        {
            BarCode = barCode;
            Name = name;
            Description = description;
            Price = price;
            Currency = currency;
            Stock = stock;
            Category = category;
        }

        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Stock { get; set; }        
        public CategoriesCreateModel Category { get; set; }
    }
}
