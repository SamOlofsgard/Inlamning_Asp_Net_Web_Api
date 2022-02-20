using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_handelsystem.Models.Entities
{

    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {

        public UserEntity()
        {

        }

        public UserEntity(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public UserEntity(string firstName, string lastName, string email, string password, int addressId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            AddressId = addressId;
        }

        public UserEntity(int id, string firstName, string lastName, string email, string password, int addressId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            AddressId = addressId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }


        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; }
    }
}
