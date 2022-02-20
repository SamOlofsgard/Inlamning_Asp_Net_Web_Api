using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_handelsystem.Models.Entities
{
    public class CategoriesEntity
    {
        public CategoriesEntity(string name)
        {
            Name = name;
        }

        public CategoriesEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
    }
}
