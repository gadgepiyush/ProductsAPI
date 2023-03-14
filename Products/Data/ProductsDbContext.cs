using Microsoft.EntityFrameworkCore;
using Products.Models.Domain;

namespace Products.Data
{
    public class ProductsDbContext : DbContext  
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seller> Sellers { get; set;}
    }
}
