using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_handelsystem.Models.Entities
{
    public class OrderRowEntity
    {
        public OrderRowEntity(int productId, int quantity, decimal price)// kolla om denna behövs
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderRowEntity(int orderId, int productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderRowEntity(int id, int orderId, int productId, int quantity, decimal price)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        //public ICollection<OrdersEntity> Orders { get; set; }
    }
}
