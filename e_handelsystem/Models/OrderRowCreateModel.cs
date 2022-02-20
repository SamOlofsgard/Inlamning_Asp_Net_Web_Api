namespace e_handelsystem.Models
{
    public class OrderRowCreateModel
    {
        public OrderRowCreateModel(int orderId, int productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
