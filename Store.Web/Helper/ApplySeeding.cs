﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entity.IdentityEntity;
using Store.Repository;

namespace Store.Web.Helper
{
    public class ApplySeeding
    {
        public static async Task applySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<StoreDbContext>();
                    var UserManager = service.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                    await StoreIdentityContextSeed.SeedUserAsync(UserManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
