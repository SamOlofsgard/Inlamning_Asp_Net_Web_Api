using e_handelsystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_handelsystem
{
    public class SqlContext:DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<CategoriesEntity> Categories { get; set; }
        public virtual DbSet<ProductsEntity> Products { get; set; }        
        public virtual DbSet<OrderRowEntity> OrderRows { get; set; }
        public virtual DbSet<OrdersEntity> Orders { get; set; }


    }
}
