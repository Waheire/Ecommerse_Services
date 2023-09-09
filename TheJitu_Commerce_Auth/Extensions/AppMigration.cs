using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TheJitu_Commerce_Auth.Data;

namespace TheJitu_Commerce_Auth.Extensions
{
    public static class AppMigration
    {
        public static IApplicationBuilder  UseMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            return app;
        }
    }
}
