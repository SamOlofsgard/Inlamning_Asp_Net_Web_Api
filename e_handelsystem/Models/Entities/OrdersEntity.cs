using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_handelsystem.Models.Entities
{
    public class OrdersEntity
    {
        public OrdersEntity(int customerId, string customerName, string customerAddress, DateTime orderDate, decimal totalPrice, string status)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            Status = status;
        }

        public OrdersEntity(int id, int customerId, string customerName, string customerAddress, DateTime orderDate, decimal totalPrice, string status)
        {
            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            Status = status;
        }

        

        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CustomerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CustomerAddress { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime OrderDate { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }
        public OrderRowEntity OrderRow { get; set; }

    }
}
