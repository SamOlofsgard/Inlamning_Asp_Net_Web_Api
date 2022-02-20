namespace e_handelsystem.Models
{
    public class OrderUpdateModel
    {
        public OrderUpdateModel(int id, decimal totalPrice, string status)
        {
            Id = id;
            TotalPrice = totalPrice;
            Status = status;
        }

        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
