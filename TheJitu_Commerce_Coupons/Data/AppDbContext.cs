using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Coupons.Model;

namespace TheJitu_Commerce_Coupons.Data
{
    public class AppDbContext :DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
