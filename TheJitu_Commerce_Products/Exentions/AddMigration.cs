﻿using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Products.Data;

namespace TheJitu_Commerce_Products.Exentions
{
    public static class AddMigration
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
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
