using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Products.Model;

namespace TheJitu_Commerce_Products.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
