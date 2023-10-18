using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Email.Model;

namespace TheJitu_Commerce_Email.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}
