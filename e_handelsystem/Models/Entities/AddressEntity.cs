using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_handelsystem.Models.Entities
{
    public class AddressEntity
    {
        public AddressEntity(string addressLine, string postalCode, string city)
        {
            AddressLine = addressLine;
            PostalCode = postalCode;
            City = city;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string AddressLine { get; set; }

        [Required]
        [Column(TypeName = "char(5)")]
        public string PostalCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }


        
    }
}
