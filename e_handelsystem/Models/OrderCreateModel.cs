namespace e_handelsystem.Models
{
    public class OrderCreateModel
    {
        public OrderCreateModel(int id, int customerId, string customerName, string customerAddress, DateTime orderDate, decimal totalPrice, string status)
        {
            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            Status = status;
        }

        public OrderCreateModel(int id, int customerId, string customerName, string customerAddress, DateTime orderDate, decimal totalPrice, string status, OrderRowCreateModel orderRow)
        {
            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            Status = status;
            OrderRow = orderRow;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public OrderRowCreateModel OrderRow { get; set; }
    }
}
