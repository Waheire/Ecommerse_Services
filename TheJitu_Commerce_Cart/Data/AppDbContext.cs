using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Cart.Model;

namespace TheJitu_Commerce_Cart.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }      

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }  
    }
}
