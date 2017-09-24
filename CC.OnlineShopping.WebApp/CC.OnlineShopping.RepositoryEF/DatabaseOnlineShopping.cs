using CC.OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.OnlineShopping.RepositoryEF
{
    public class DatabaseOnlineShopping : DbContext
    {
        public DatabaseOnlineShopping() : base ("TeamOnlineShopping")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductsToCart> ProductToCarts { get; set; }
    }
}
