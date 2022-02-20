namespace e_handelsystem.Models
{
    public class ProductUpdateModel
    {
        public ProductUpdateModel(int id, string barCode, string name, string description, decimal price, int stock, CategoriesCreateModel category)
        {
            Id = id;
            BarCode = barCode;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }        
        public int Stock { get; set; }
        public CategoriesCreateModel Category { get; set; }


    }
}
